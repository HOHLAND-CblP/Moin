using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;
using MoinBackend.Domain.Services;
using MoinBackend.Domain.Settings;
using Moq;

namespace Tests.ServiceTests;

public class AccountServiceTest
{
    [Fact]
    public async Task CreateAccountTest_Success()
    {
        var account = new Account
        {
            UserId = 1,
            Value = 0,
            CurrencyId = 1
        };
        
        var accountRepositoryMock = new Mock<IAccountRepository>();
        accountRepositoryMock
            .Setup(rep => rep.Create(It.IsAny<Account>(), It.IsAny<CancellationToken>()).Result)
            .Returns(1);

        var service = new AccountService(accountRepositoryMock.Object);

        var result = await service.Create(account, CancellationToken.None);
        
        Assert.Equal(result, 1);
    }

    [Fact]
    public async Task GetAccountTest_Success()
    {
        var accountId = 1;
        var account = new Account
        {
            Id = 1,
            UserId = 1,
            Value = 0,
            CurrencyId = 1,
            CreationDate = DateTime.UtcNow
        };
        
        var accountRepositoryMock = new Mock<IAccountRepository>();
        accountRepositoryMock
            .Setup(rep => rep.GetAccount(It.IsAny<long>(), It.IsAny<CancellationToken>()).Result)
            .Returns(account);
        
        var service = new AccountService(accountRepositoryMock.Object);
        
        var result = await service.Get(accountId, CancellationToken.None);
        
        Assert.Equal(result, account);
    }
}