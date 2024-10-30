using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnsProjectId()
        {
            // Arrange

            var projectRepositoryMock = Substitute.For<IProjectRepository>();
            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Teste 1",
                Description = "Teste 1",
                IdCliente = 1,
                IdFreelancer = 2,
                TotalCost = 10000
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepositoryMock);

            // Act

            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            // Assert

            Assert.True(id >= 0);

            await  projectRepositoryMock.Received(1).AddAsync(Arg.Any<Project>());
        }
    }
}
