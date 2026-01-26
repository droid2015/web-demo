using Platform.Core.Domain.Entities;
using Platform.Core.Domain.Interfaces;

namespace Platform.Core.Services;

public class ModuleService
{
    private readonly IRepository<Module> _moduleRepository;

    public ModuleService(IRepository<Module> moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<IEnumerable<Module>> GetAllModulesAsync()
    {
        return await _moduleRepository.GetAllAsync();
    }

    public async Task<Module?> GetModuleByIdAsync(int id)
    {
        return await _moduleRepository.GetByIdAsync(id);
    }

    public async Task<Module> CreateModuleAsync(Module module)
    {
        return await _moduleRepository.AddAsync(module);
    }

    public async Task UpdateModuleAsync(Module module)
    {
        await _moduleRepository.UpdateAsync(module);
    }

    public async Task ToggleModuleAsync(int id, bool isEnabled)
    {
        var module = await _moduleRepository.GetByIdAsync(id);
        if (module != null)
        {
            module.IsEnabled = isEnabled;
            await _moduleRepository.UpdateAsync(module);
        }
    }
}
