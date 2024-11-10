using System.Transactions;
using StarIt.Bl.Auth;
using StarIt.Bl.Game;
using StarIt.Models;
using StarItTest.Common;

namespace StarItTest;

public class GameTest : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public async Task TestGameCreating()
    {
        using TransactionScope scope = TestHelper.CreateTransactionScope();

        GameModel model = new GameModel
        {
            Title = "TestGame",
            Description = "TestGameCreating",
        };
        
        bool result = await gameBl.CreateGame(model);
        
        Assert.IsTrue(result);
    }
}