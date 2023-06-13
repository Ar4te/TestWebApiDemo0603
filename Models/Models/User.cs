using Extensions.SnowFlake;
using Models.ViewModels;
using SqlSugar;

namespace Models.Models;

[SugarTable("User")]
public class User
{
    public User()
    {

    }
    public User(UserVM uvm)
    {
        UserName = uvm.uName;
        UserAge = uvm.uAge;
        Password = uvm.Password;
    }

    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long UserId { get; set; }
    public string UserName { get; set; }
    public int UserAge { get; set; }
    public string Password { get; set; }
}
