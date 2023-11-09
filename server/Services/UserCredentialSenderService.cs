using Grpc.Core;

namespace server.Services;

public class UserCredentialSenderService : UserCredentialSender.UserCredentialSenderBase
{
    private readonly ILogger<UserCredentialSenderService> _logger;
    public UserCredentialSenderService(ILogger<UserCredentialSenderService> logger)
    {
        _logger = logger;
    }

    public override Task<Reply> SendUserCredentials(UserCredentials request, ServerCallContext context)
    {
        Console.WriteLine(request.Username);
        Console.WriteLine(request.Password);
        Console.WriteLine(request.Action);
        //Console.WriteLine(BCrypt.Net.BCrypt.EnhancedVerify("password", BCrypt.Net.BCrypt.EnhancedHashPassword("password", 13)));
        return Task.FromResult(new Reply
        {
            Message = true
        });
    }
}
