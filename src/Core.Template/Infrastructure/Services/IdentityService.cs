using Microsoft.AspNetCore.Http;
using System;

namespace Core.Template.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// 
        /// </summary>
        public IdentityUser CurrentUser
        {
            get
            {
                var _user = new IdentityUser();
                if (_context.HttpContext.Request.Headers.TryGetValue("CityID", out var city))
                {
                    _user.City = city;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("BrokerID", out var userId))
                {
                    _user.Id = userId;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("BrokerName", out var user))
                {
                    _user.Name = System.Net.WebUtility.UrlDecode(user);
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("BrokerPhone", out var phone))
                {
                    _user.Phone = phone;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("CompanyID", out var companyId))
                {
                    _user.CompanyId = companyId;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("CompanyName", out var company))
                {
                    _user.Company = System.Net.WebUtility.UrlDecode(company);
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("DepartmentId", out var departmentId))
                {
                    _user.DepartmentId = departmentId;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("DepartmentName", out var department))
                {
                    _user.Department = System.Net.WebUtility.UrlDecode(department);
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("BigRegionId", out var bigRegionId))
                {
                    _user.BigRegionId = bigRegionId;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("BigRegionName", out var bigRegion))
                {
                    _user.BigRegion = System.Net.WebUtility.UrlDecode(bigRegion);
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("RegionId", out var regionId))
                {
                    _user.RegionId = regionId;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("RegionName", out var region))
                {
                    _user.Region = System.Net.WebUtility.UrlDecode(region);
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("StoreID", out var storeId))
                {
                    _user.StoreId = storeId;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("StoreName", out var store))
                {
                    _user.Store = System.Net.WebUtility.UrlDecode(store);
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("GroupId", out var groupId))
                {
                    _user.GroupId = groupId;
                }
                if (_context.HttpContext.Request.Headers.TryGetValue("GroupName", out var group))
                {
                    _user.Group = System.Net.WebUtility.UrlDecode(group);
                }
                return _user;
            }
        }
    }
}
