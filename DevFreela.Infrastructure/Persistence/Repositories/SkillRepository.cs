using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _ConnectionString;
        public SkillRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _ConnectionString = configuration.GetConnectionString("DevFreelaCs") ?? string.Empty;
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            using (var sqlConnection = new SqlConnection(_ConnectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Description FROM Skills";

                var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

                return skills.ToList();
            }
        }
    }
}
