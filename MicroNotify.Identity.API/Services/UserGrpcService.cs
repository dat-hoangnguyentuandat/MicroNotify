using Grpc.Core;
using MicroNotify.Identity.API.Protos;
using MicroNotify.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MicroNotify.Identity.API.Services
{
    public class UserGrpcService : UserService.UserServiceBase
    {
        private readonly IdentityDbContext _context;
        private readonly ILogger<UserGrpcService> _logger;

        public UserGrpcService(IdentityDbContext context, ILogger<UserGrpcService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task<UserResponse> GetUserById(GetUserRequest request, ServerCallContext context)
        {
            _logger.LogInformation("gRPC GetUserById called with userId: {UserId}", request.UserId);

            if (!Guid.TryParse(request.UserId, out var userId))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid user ID format"));
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            return new UserResponse
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive
            };
        }
    }
}
