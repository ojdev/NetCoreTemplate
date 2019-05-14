using Core.Template.BackgroundTasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Template
{
    /// <summary>
    /// 
    /// </summary>
    public static class BackgroundTaskModuleExtension
    {
        /// <summary>
        /// 曲线救国的解决程序启动时无法注入BackgroundTasks项目的问题
        /// </summary>
        /// <param name="services"></param>
        public static void AddBackgroundTaskModule(this IServiceCollection services)
        {
            services.TryAddSingleton<BackgroundTaskModule>();
        }
    }
}
