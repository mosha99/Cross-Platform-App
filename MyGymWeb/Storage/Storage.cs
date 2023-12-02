using Blazored.LocalStorage;
using Pages.Interfaces;
using Pages.Models;
using Pages.Models.StorageModel;

namespace MyGymWeb.Storage;

public class Storage : IStorage
{
    private readonly ILocalStorageService _localStorageService;

    public Storage(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public static List<CustomProgram> List = new List<CustomProgram>();

    private async Task Save() => await _localStorageService.SetItemAsync("Data", List);
    private async Task Load() => List = await _localStorageService.GetItemAsync<List<CustomProgram>>("Data");


    public async Task<List<CustomProgram>> GetExercisePrograms()
    {   
        await Load();
        return List;
    }

    public async Task<CustomProgram> GetExerciseProgram(int id)
    {
        await Load();
        return List.Single(x => x.Id == id);
    }

    public async Task SaveCustomProgram(CustomProgram customProgram)
    {
        await Load();

        if (customProgram.Id != 0)
        {
            List.RemoveAll(x => x.Id == customProgram.Id);
        }
        else
        {
            customProgram.Id = 1;
            if (List.Any()) customProgram.Id = List.Select(x => x.Id).Max() + 1;
        }

        List.Add(customProgram);

        await Save();
    }

    public async Task SaveProgramActionResult(ProgramActionResult customProgram)
    {

        await Load();

        customProgram = List.Single(x => x.Id == customProgram.CustomProgramId).Actions.Single(x=>x.Id == customProgram.Id);

        foreach (var action in customProgram.ProgramActions)
        {
            action.ProgramActionResultId = customProgram.Id;
        }
        await Save();

    }

    public Task<List<string>> SearchForName(string name)
    {

        return Task.FromResult(new List<string>());
    }

    public Task SaveRecord(int? record, int ProgramActionId)
    {
        return Task.CompletedTask;
    }

    public async Task RemoveExercisePrograms(int id)
    {
        await Load();

        List.RemoveAll(x => x.Id == id);

        await Save();
    }
}