using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using EasyNetQ;
using GameHall.SharedKernel.Core.Commands;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GameHall.FrontEnd.WebApi.Endpoints.User
{
    public class CreateUserEndpoint: BaseAsyncEndpoint
        .WithRequest<CreateUserRequest>
        .WithResponse<CreateUserResponse>
    {
        private readonly IBus _bus;

        public CreateUserEndpoint(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("/users")]
        [SwaggerOperation(
            Summary = "Create user",
            Description = "Creates a user",
            OperationId = "User.Create",
            Tags = new[] { "UserEndpoint" })
        ]
        public override async Task<ActionResult<CreateUserResponse>> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken = new CancellationToken())
        {
            await _bus.PubSub.PublishAsync(new CreateUser(Guid.NewGuid(), "aju"));
            return  Ok(new CreateUserResponse());
        }
    }

    public class CreateUserResponse
    {
    }

    public class CreateUserRequest
    {
    }
}
