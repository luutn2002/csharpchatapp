using Grpc.Core;
using server.Processor;

namespace server.Services;

public class UserCredentialSenderService : UserCredentialSender.UserCredentialSenderBase
{
    private readonly ILogger<UserCredentialSenderService> _logger;
    readonly UserAccountDatabaseProcessor LoginAndRegisterProcessor = new(DevelopmentMode:true);
    public UserCredentialSenderService(ILogger<UserCredentialSenderService> logger)
    {
        _logger = logger;
    }

    bool LoginAndRegisterActionHandler(UserCredentials request){
        switch (request.Action) 
        {
            case "register":
                return LoginAndRegisterProcessor.AddNewUserToDatabase(request.Username, request.Password);
                //break;
            case "login":
                return LoginAndRegisterProcessor.CheckIfUserValid(request.Username, request.Password);
                //break;
            default: return false;
        }
    }

    public override Task<Reply> SendUserCredentials(UserCredentials request, ServerCallContext context)
    {
        return Task.FromResult(new Reply
        {
            Message = LoginAndRegisterActionHandler(request)
        });
    }
}
