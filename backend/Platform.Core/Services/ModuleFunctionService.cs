using Platform.Core.Domain.Entities;
using Platform.Core.Domain.Interfaces;

namespace Platform.Core.Services;

/// <summary>
/// Service for managing module functions
/// </summary>
public class ModuleFunctionService
{
    private readonly IRepository<ModuleFunction> _moduleFunctionRepository;
    private readonly IRepository<Module> _moduleRepository;

    public ModuleFunctionService(
        IRepository<ModuleFunction> moduleFunctionRepository,
        IRepository<Module> moduleRepository)
    {
        _moduleFunctionRepository = moduleFunctionRepository;
        _moduleRepository = moduleRepository;
    }

    public async Task<IEnumerable<ModuleFunction>> GetAllFunctionsAsync()
    {
        return await _moduleFunctionRepository.GetAllAsync();
    }

    public async Task<IEnumerable<ModuleFunction>> GetFunctionsByModuleIdAsync(int moduleId)
    {
        var allFunctions = await _moduleFunctionRepository.GetAllAsync();
        return allFunctions.Where(f => f.ModuleId == moduleId);
    }

    public async Task<ModuleFunction?> GetFunctionByIdAsync(int id)
    {
        return await _moduleFunctionRepository.GetByIdAsync(id);
    }

    public async Task<ModuleFunction> CreateFunctionAsync(ModuleFunction function)
    {
        // Verify module exists
        var module = await _moduleRepository.GetByIdAsync(function.ModuleId);
        if (module == null)
        {
            throw new ArgumentException($"Module with ID {function.ModuleId} does not exist");
        }

        return await _moduleFunctionRepository.AddAsync(function);
    }

    public async Task UpdateFunctionAsync(ModuleFunction function)
    {
        await _moduleFunctionRepository.UpdateAsync(function);
    }

    public async Task DeleteFunctionAsync(int id)
    {
        await _moduleFunctionRepository.DeleteAsync(id);
    }

    public async Task ToggleFunctionAsync(int id, bool isEnabled)
    {
        var function = await _moduleFunctionRepository.GetByIdAsync(id);
        if (function != null)
        {
            function.IsEnabled = isEnabled;
            await _moduleFunctionRepository.UpdateAsync(function);
        }
    }
}
