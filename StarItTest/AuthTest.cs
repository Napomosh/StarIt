using System.Transactions;
using Microsoft.AspNetCore.Http;
using StarIt.Models;
using StarIt.Tools;
using StarItTest.Common;

namespace StarItTest;

public class AuthTest : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task BaseRegistrationTest()
    {
        using TransactionScope scope = TestHelper.CreateTransactionScope();
        
        string email = Guid.NewGuid() + "@test.com";
        Guid userGuid = await authBl.RegisterUser(new UserModel
        {
            Email = email,
            Password = "password",
        });
        Assert.That(Guid.Empty, Is.Not.EqualTo(userGuid));
        
        userGuid = await authBl.RegisterUser(new UserModel
        {
            Email = email,
            Password = "password",
        });
        Assert.That(Guid.Empty, Is.EqualTo(userGuid));
    }
    
    [Test]
    public async Task AuthLoginTest()
    {
        using TransactionScope transaction = TestHelper.CreateTransactionScope();
        string email = Guid.NewGuid() + "@test.com";
        
        Assert.That(authBl.Login(email, "password").GetAwaiter().GetResult(), Is.EqualTo(false));
        
        Guid userGuid = await authBl.RegisterUser(new UserModel
        {
            Email = email,
            Password = "password",
        });
        Assert.That(userGuid, Is.Not.EqualTo(Guid.Empty));
        
        Assert.That(authBl.Login(email, "pass", false).GetAwaiter().GetResult(), Is.EqualTo(false));
        Assert.That(authBl.Login("wrong_email", "password", false).GetAwaiter().GetResult(), Is.EqualTo(false));
        Assert.That(authBl.Login(email, "password", false).GetAwaiter().GetResult(), Is.EqualTo(true));
    }

    [Test]
    public async Task RememberMeTest()
    {
        using TransactionScope transaction = TestHelper.CreateTransactionScope();
        string email = Guid.NewGuid() + "@test.com";
        
        Guid userGuid = await authBl.RegisterUser(new UserModel
        {
            Email = email,
            Password = "password",
        });
        bool loginResult = await authBl.Login(email, "password", true);
        Assert.IsTrue(loginResult);
        Assert.IsNotEmpty(webCookie.Get(AuthConstants.AUTH_REMEMBER_ME_COOKIE));
        await authBl.Logout();
        Assert.IsEmpty(webCookie.Get(AuthConstants.AUTH_REMEMBER_ME_COOKIE));
        
        loginResult = await authBl.Login(email, "password", false);
        Assert.IsTrue(loginResult);
        Assert.IsEmpty(webCookie.Get(AuthConstants.AUTH_REMEMBER_ME_COOKIE));
    }
}