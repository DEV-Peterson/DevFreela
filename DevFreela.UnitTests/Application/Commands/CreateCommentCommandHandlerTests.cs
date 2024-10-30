using DevFreela.Application.Commands.CreateComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOK_Execute_ShouldAddComment()
        {
            // Arrange

            var projectRepositoryMock = Substitute.For<IProjectRepository>();
            var createCommentCommand = new CreateCommentCommand
            {
                Content = "Comentário de teste",
                IdProject = 1,
                IdUser = 1
            };

            var createCommentCommandHandler = new CreateCommentCommandHandler(projectRepositoryMock);

            // Acct

            await createCommentCommandHandler.Handle(createCommentCommand, new CancellationToken());


            // Assertt

            await projectRepositoryMock.Received(1).AddCommentAsync(Arg.Any<ProjectComment>());
        }
    }
}
