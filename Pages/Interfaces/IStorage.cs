using Pages.Models;
using Pages.Models.StorageModel;

namespace Pages.Interfaces;

public interface IStorage
{
    public Task<List<CustomProgram>> GetExercisePrograms();
    public Task<CustomProgram> GetExerciseProgram(int id);
    public Task SaveCustomProgram(CustomProgram customProgram);
    public Task SaveProgramActionResult(ProgramActionResult customProgram);
    public Task<List<string>> SearchForName(string name);
    public Task SaveRecord(int? record, int ProgramActionId);
    public Task RemoveExercisePrograms(int id);
}