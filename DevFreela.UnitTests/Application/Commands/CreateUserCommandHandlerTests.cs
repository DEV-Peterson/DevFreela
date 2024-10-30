using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Execute_ReturnsUserId()
        {
            // Arrange

            var userRepositoryMock = Substitute.For<IUserRepository>();
            var authRepositoryMock = Substitute.For<IAuthService>();
            var createUserCommand = new CreateUserCommand
            {
                FullName = "Usuario Teste",
                Email = "teste@email.com",
                Password = "Teste123@",
                BirthDate = new DateTime(2002,6,3),
                Role = "Client"
            };

            var createUserCommandHandler = new CreateUserCommandHandler(userRepositoryMock, authRepositoryMock);


            // Acct

            var id = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());


            // Assert

            Assert.True(id >= 0);

            await userRepositoryMock.Received(1).AddUserAsync(Arg.Any<User>());
        }
    }
}
