using Microsoft.Extensions.Options;
using Moq;
using MoinBackend.Domain;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;
using MoinBackend.Domain.Services;
using MoinBackend.Domain.Settings;

namespace Tests.ServiceTests;

public class UserServiceTest
{
    readonly AuthSettings authSettings = new AuthSettings
    {
        Issuer = "Issuer",
        Audience = "Audience",
        SecretKey = "aaaaaaabbbbbbbbccccccc1234567890",
        TokenLifeTimeInSeconds = 604800
    };
    
    [Fact]
    public async Task SignUpTest_Success()
    {
       var user = new User
        {
            Username = "hohland_cblp",
            Name = "Ismail",
            Email = "ismail987654321lkjhgfdas@gmail.com",
            Password = "1234"
        };

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(rep => rep.Create(It.IsAny<User>(), It.IsAny<CancellationToken>()).Result)
            .Returns(1);

        var service = new UserService(userRepositoryMock.Object, new OptionsWrapper<AuthSettings>(authSettings));

        var result = await service.SignUp(user, CancellationToken.None);
        
        Assert.Equal(result.User.Id, 1);
        Assert.False(string.IsNullOrEmpty(result.Token));
    }

    [Fact]
    public async Task LoginTest_Success()
    {
        string testUsername = "hohland_cblp";
        string testPassword = "1234";
        
        var user = new User
        {
            Id = 1,
            Username = "hohland_cblp",
            Name = "Ismail",
            Email = "ismail987654321lkjhgfdas@gmail.com",
            //Hash for '1234' password
            Password = "+mkvxUzmjJ9qTstPVHz3/RBjiMfaqd7Zdu0lLDF+opHukyA/ptGnrheIXMDbpgl9hDsy8+ePRK9+yVWvTCJORvTGliBfP8O0JmdGWUHDEVS0dVYLwlro8FChOgx94OTTXkrxxG4e8foe7E7JtWoa2A8q21yb1cdy8iJ3qKcazuY="
        };
        
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(rep => rep.GetUserByUsername(It.IsAny<string>(), It.IsAny<CancellationToken>()).Result)
            .Returns(user);
        
        var service = new UserService(userRepositoryMock.Object, new OptionsWrapper<AuthSettings>(authSettings));

        var result = await service.Login(testUsername, testPassword,CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.Equal(result.User.Username, testUsername);
    }
    
    [Fact]
    public async Task LoginTest_Fail()
    {
        string testUsername = "hohladn_cblp";
        string testPassword = "12345";
        
        var user = new User
        {
            Id = 1,
            Username = "hohland_cblp",
            Name = "Ismail",
            Email = "ismail987654321lkjhgfdas@gmail.com",
            //Hash for '1234' password
            Password = "NVyy7c7RZc2hKRDyQ0vE9RUmvzxhZcb9WuyDUyG2QgiLx7vJzL4Ad1IdseZn2Feh6NsAR9f9Ddrd2YtP1w4Vh4eiR3TWskMmgYS5sGejzAQEzXCqGOQ7w2Su/mFoOnr9qQB7dEz6YS1s6XiEg+IeAjcaa364IUwKePd2ay819NA="
        };
        
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(rep => rep.GetUserByUsername(It.IsAny<string>(), It.IsAny<CancellationToken>()).Result)
            .Returns(user);
        
        var service = new UserService(userRepositoryMock.Object, new OptionsWrapper<AuthSettings>(authSettings));

        var result = await service.Login(testUsername, testPassword,CancellationToken.None);
        
        Assert.Null(result);
    }
    
    
}