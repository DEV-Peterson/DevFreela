using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Execute_returnsThreeProjectsViewModels()
        {
            // Arrange

            var projects = new List<Project>
            {
                new Project("Teste 1", "Teste 1", 1, 2, 10000),
                new Project("Teste 2", "Teste 2", 1, 2, 20000),
                new Project("Teste 3", "Teste 3", 1, 2, 30000),
            };

            var projectRepositoryMock = Substitute.For<IProjectRepository>();
            projectRepositoryMock.GetAllAsync().Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock);
            

            // Act

            var projectsViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // Assert

            Assert.NotNull(projectsViewModelList);
            Assert.NotEmpty(projectsViewModelList);
            Assert.Equal(projects.Count(), projectsViewModelList.Count());

            await projectRepositoryMock.Received(1).GetAllAsync();
        }
    }
}
