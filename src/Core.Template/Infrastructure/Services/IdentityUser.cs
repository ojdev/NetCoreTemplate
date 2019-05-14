using System.Runtime.Serialization;

namespace Core.Template.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class IdentityUser
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Phone { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string Name { set; get; }
        /// <summary>
        /// 公司Id
        /// </summary>
        [DataMember]
        public string CompanyId { set; get; }
        /// <summary>
        /// 经纪公司
        /// </summary>
        [DataMember]
        public string Company { set; get; }
        /// <summary>
        /// 部门Id
        /// </summary>
        [DataMember]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [DataMember]
        public string Department { get; set; }
        /// <summary>
        /// 大区Id
        /// </summary>
        [DataMember]
        public string BigRegionId { get; set; }
        /// <summary>
        /// 大区
        /// </summary>
        [DataMember]
        public string BigRegion { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        [DataMember]
        public string RegionId { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [DataMember]
        public string Region { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [DataMember]
        public string StoreId { set; get; }
        /// <summary>
        /// 门店
        /// </summary>
        [DataMember]
        public string Store { set; get; }
        /// <summary>
        /// 店组Id
        /// </summary>
        [DataMember]
        public string GroupId { get; set; }
        /// <summary>
        /// 店组
        /// </summary>
        [DataMember]
        public string Group { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IdentityUser()
        {
            CompanyId = string.Empty;
            Company = string.Empty;
            DepartmentId = string.Empty;
            Department = string.Empty;
            BigRegionId = string.Empty;
            BigRegion = string.Empty;
            RegionId = string.Empty;
            Region = string.Empty;
            StoreId = string.Empty;
            Store = string.Empty;
            GroupId = string.Empty;
            Group = string.Empty;
            Id = string.Empty;
            Name = string.Empty;
        }
    }
}
