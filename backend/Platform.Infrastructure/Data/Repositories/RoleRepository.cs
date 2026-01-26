using Platform.Core.Domain.Entities;
using Platform.Infrastructure.Data;

namespace Platform.Infrastructure.Data.Repositories;

public class RoleRepository : GenericRepository<Role>
{
    public RoleRepository(OracleDbContext context) : base(context, "ROLES")
    {
    }
}
