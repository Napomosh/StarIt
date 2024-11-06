using System.Transactions;
using Microsoft.AspNetCore.Http;
using StarIt.Bl.Auth;
using StarIt.Models;
using StarIt.Tools;
using StarItTest.Common;

namespace StarItTest;

public class CurrentUserTest : BaseTest
{
    [Test]
    public async Task BaseCheckLoginTest()
    {
        using TransactionScope scope = TestHelper.CreateTransactionScope();
        
        string email = Guid.NewGuid() + "@test.com";
        Guid userGuid = await authBl.RegisterUser(new UserModel
        {
            Email = email,
            Password = "password",
        });
        bool loginResult = await authBl.Login(email, "password", true);
        Assert.IsTrue(loginResult);

        Guid checkUserGuid = await currentUser.GetUserIdByToken();
        Assert.That(checkUserGuid, Is.EqualTo(userGuid));
    }
}