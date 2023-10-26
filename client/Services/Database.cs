using System.Collections.Generic;
using client.Models;

namespace client.Services
{
  public class Database
  {
    List<Account> accountDatabase = new();
    public Database()
    {
      accountDatabase.Add(new Account("admin", "password"));
    }

    public void RegisterNewAccount(string username, string password){
      accountDatabase.Add(new Account(username, password));
    }

    public bool VerifyAccount(Account acc){
      return accountDatabase.Contains(acc);
    }
  }
}