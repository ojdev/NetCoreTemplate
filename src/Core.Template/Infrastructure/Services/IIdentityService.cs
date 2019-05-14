namespace Core.Template.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// 
        /// </summary>
        IdentityUser CurrentUser { get; }
    }
}
