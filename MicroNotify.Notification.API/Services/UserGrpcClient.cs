using Grpc.Net.Client;
using MicroNotify.Identity.API.Protos;

namespace MicroNotify.Notification.API.Services
{
    public class UserGrpcClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserGrpcClient> _logger;

        public UserGrpcClient(IConfiguration configuration, ILogger<UserGrpcClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<UserResponse?> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var identityServiceUrl = _configuration["GrpcServices:IdentityService"];
                
                using var channel = GrpcChannel.ForAddress(identityServiceUrl!);
                var client = new UserService.UserServiceClient(channel);

                var request = new GetUserRequest { UserId = userId.ToString() };
                var response = await client.GetUserByIdAsync(request);

                _logger.LogInformation("Successfully retrieved user {UserId} via gRPC", userId);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling gRPC service for user {UserId}", userId);
                return null;
            }
        }
    }
}
