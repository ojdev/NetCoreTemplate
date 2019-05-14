using MediatR;

namespace Core.Template.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class AddUserDemoCommand : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }
    }
}
