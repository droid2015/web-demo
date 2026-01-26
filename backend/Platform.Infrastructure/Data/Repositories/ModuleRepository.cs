using Platform.Core.Domain.Entities;
using Platform.Infrastructure.Data;

namespace Platform.Infrastructure.Data.Repositories;

public class ModuleRepository : GenericRepository<Module>
{
    public ModuleRepository(OracleDbContext context) : base(context, "MODULES")
    {
    }
}
