namespace Core.Template.Domain.SeedWork
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
