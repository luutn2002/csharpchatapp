namespace client.Models
{
  public class Account
  {
    private string Username {get; set;} = string.Empty;
    private string Password {get; set;} = string.Empty;

    public Account(string username, string password){
      Username = username;
      Password = password;
    }
  }
}