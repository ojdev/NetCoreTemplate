using Consul;
using Core.Template.Infrastructure;
using Core.Template.Infrastructure.Filters;
using Core.Template.Infrastructure.Idempotency;
using Core.Template.Infrastructure.Services;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Template
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiResponseFilterAttribute));
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new DigitsFormatConvert());
            })
            .AddControllersAsServices()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #region 健康检查
            services.AddHealthChecks();
            #endregion
            #region 缓存
            services.AddResponseCaching(); 
            #endregion
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddTransient<IIdentityService, IdentityService>();
            #region 多版本Api
            services.AddApiVersioning(options =>
               {
                   options.AssumeDefaultVersionWhenUnspecified = true;
                   options.ApiVersionReader = new UrlSegmentApiVersionReader();
               }).AddVersionedApiExplorer(options =>
               {
                   options.GroupNameFormat = "VVV";
                   options.SubstituteApiVersionInUrl = true;
               });
            #endregion
            #region swagger
            if (Configuration.GetValue<bool>("App:EnableSwagger"))
            {
                var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
                services.AddSwaggerDocument(configure =>
                        {
                            configure.DocumentName = "v0";
                            configure.Description = "这个是演示用的版本";
                            configure.ApiGroupNames = new[] { "0" };
                            configure.Title = assemblyName;
                        })
                        .AddSwaggerDocument(configure =>
                        {
                            configure.DocumentName = "v1";
                            configure.Description = "正式使用环境下的Api版本";
                            configure.ApiGroupNames = new[] { "1" };
                            configure.Title = assemblyName;
                        })
                        .AddSwaggerDocument(configure =>
                         {
                             configure.DocumentName = "v99";
                             configure.Description = "运维使用";
                             configure.ApiGroupNames = new[] { "99" };
                             configure.Title = assemblyName;
                         });
            }
            #endregion
            services.AddBackgroundTaskModule();
            ConfigureFluentValidation(services);
            ConfigureMediatR(services);
            ConfigureConsul(services);
            ConfigureEventBus(services);
            ConfigureEntityFramework(services);
            ConfigureEventStore(services);
            return services.BuildServiceProvider();
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                #region swagger
                app.UseSwagger().UseSwaggerUi3();
                #endregion
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            #region 健康检查
            app.UseHealthChecks("/hc", new HealthCheckOptions
            {
                ResultStatusCodes =
                {
                    [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            }); 
            #endregion
            DataSeederConfigure(app);
            #region Cache
            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(30)
                };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] = new string[] { "Accept-Encoding" };
                await next();
            });
            #endregion
            app.RabbitMQEventBusAutoSubscribe();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
        #region 中间件
        /// <summary>
        /// 配置FluentValidation
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection ConfigureFluentValidation(IServiceCollection services)
        {
            //services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));// 不起作用
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                              .Values
                              .SelectMany(x => x.Errors.Select(p => p.ErrorMessage))
                              .ToList();
                    return new BadRequestObjectResult(new ApiResponse<object>(new ErrorInfo(0, string.Join(';', errors))));
                };
            });
            return services;
        }
        /// <summary>
        /// 配置MediatR
        /// 配合集成测试，该方法可重写
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<ServiceFactory>(p => p.GetService);
            return services;
        }

        /// <summary>
        /// 配置Consul
        /// 配合集成测试，该方法可重写
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection ConfigureConsul(IServiceCollection services)
        {
            ConsulExtension.Consul = new ConsulClient(option => option.Address = new Uri(Configuration["App:ConsulUrl"]));
            services.AddScoped<IConsulClient>(x =>
            {
                return ConsulExtension.Consul;
            });
            return services;
        }
        /// <summary>
        /// 配置EventBus，RabbitMQ
        /// 配合集成测试，该方法可重写
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection ConfigureEventBus(IServiceCollection services)
        {
            services.AddRabbitMQEventBus(connectionAction: () =>
            {
                string endpoint = "192.168.0.251:5672";
                if (!HostingEnvironment.IsDevelopment())
                {
                    using (IConsulClient consul = new ConsulClient(option => option.Address = new Uri(Configuration["App:ConsulUrl"])))
                    {
                        endpoint = consul.GetService(Configuration["App:EventBus:ServiceName"]).Result;
                    }
                }
                var connectionString = $"amqp://{Configuration["App:EventBus:UserName"]}:{Configuration["App:EventBus:Password"]}@{endpoint}/";
                return connectionString;
            }, eBusOption =>
            {
                eBusOption.ClientProvidedAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                eBusOption.EnableRetryOnFailure(true, 5000, TimeSpan.FromSeconds(30));
            });
            return services;
        }
        /// <summary>
        /// 配置EntityFramework
        /// 配合集成测试，该方法可重写
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<TemplateContext>(options =>
                {
                    options.UseLazyLoadingProxies()
                        .UseSqlServer(Configuration["ConnectionStrings:Default"],
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            });
                },
                ServiceLifetime.Transient);
            //services.TryAddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
            //services.TryAddScoped(typeof(IQueries), typeof(Queries));
            return services;
        }
        /// <summary>
        /// 配置EventStore
        /// 配合集成测试，该方法可重写
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection ConfigureEventStore(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<EventSourceContext>(options =>
                {
                    options.UseLazyLoadingProxies()
                        .UseSqlServer(Configuration["ConnectionStrings:Event"],
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            });
                },
                ServiceLifetime.Transient);
            services.TryAddScoped<IRequestManager, RequestManager>();
            return services;
        }

        /// <summary>
        /// 数据初始化
        /// 配合集成测试，该方法可重写
        /// </summary>
        /// <param name="app"></param>
        public virtual void DataSeederConfigure(IApplicationBuilder app)
        {
            app.MigrateDbContext<EventSourceContext>((context, services) => { })
               .MigrateDbContext<TemplateContext>((context, services) =>
               {
                   var logger = services.GetService<ILogger<TemplateContextSeed>>();
                   new TemplateContextSeed().SeedAsync(context, logger).Wait();
               });
        }
        #endregion
    }

}
