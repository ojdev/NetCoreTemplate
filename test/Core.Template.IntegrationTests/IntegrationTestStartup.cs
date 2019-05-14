using Core.Template.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Template.IntegrationTests
{
    class IntegrationTestStartup : Startup
    {
        public IntegrationTestStartup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(configuration, hostingEnvironment)
        {
        }
        public override IServiceCollection ConfigureEntityFramework(IServiceCollection services)
        {
            var connection = new SqliteConnection(new SqliteConnectionStringBuilder() { DataSource = ":memory:" }.ConnectionString);
            connection.Open();
            services.AddDbContext<TemplateContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlite(connection);
            });
            return services;
        }

        public override void DataSeederConfigure(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<TemplateContext>();
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
        }
    }
}
