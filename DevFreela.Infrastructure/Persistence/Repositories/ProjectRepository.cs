using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _ConnectionString;
        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _ConnectionString = configuration.GetConnectionString("DevFreelaCs") ?? string.Empty;
        }
        
        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            using (var sqlConnection = new SqlConnection(_ConnectionString))
            {
                sqlConnection.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedAt = project.StartedAt, project.Id });
            }
        }

        public async Task UpdateAsync(Project project)
        {
            _dbContext.Entry(project).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCommentAsync(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
