using MediatR;
using WeConnectAPI.Models;

namespace WeConnectAPI.Command.GigModelCommand
{
    public class DeleteGigModelCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}