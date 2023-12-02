using Pages.Interfaces;
using Pages.Models;
using Pages.Models.StorageModel;
using SQLite;
using static SQLite.SQLite3;

namespace MyGYMMaui.Storage;

public class Storage : IStorage
{
    SQLiteAsyncConnection _connection;

    public const string DatabaseFilename = "TodoSQLite.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public async Task init()
    {
        try
        {
            await Permissions.RequestAsync<Permissions.Microphone>();
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (status == PermissionStatus.Granted) { }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


        if (_connection is not null)
            return;

        _connection = new SQLiteAsyncConnection(DatabasePath, Flags);

        try
        {
            await _connection.CreateTableAsync<ProgramActionResult>();
            await _connection.CreateTableAsync<ProgramAction>();
            await _connection.CreateTableAsync<CustomProgram>();
            await _connection.CreateTableAsync<ActionRecord>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


    }
    public async Task<List<CustomProgram>> GetExercisePrograms()
    {
        await init();
        var result = await _connection.Table<CustomProgram>().ToListAsync();
        return result;
    }

    public async Task<CustomProgram> GetExerciseProgram(int id)
    {
        await init();

        var result = await _GetExerciseProgram(id);

        return result;
    }
    public async Task<CustomProgram> _GetExerciseProgram(int id)
    {
        await init();

        var result = (await _connection.Table<CustomProgram>().ToListAsync()).Single(x => x.Id == id);

        result.Actions = await _connection.Table<ProgramActionResult>().Where(x => x.CustomProgramId == result.Id).ToListAsync();

        foreach (var programActionResult in result.Actions)
        {
            programActionResult.ProgramActions = await _connection.Table<ProgramAction>().Where(x => x.ProgramActionResultId == programActionResult.Id).ToListAsync();
            foreach (var programAction in programActionResult.ProgramActions)
            {
                programAction.Records = await _connection.Table<ActionRecord>().Where(x => x.ProgramActionId == programAction.Id).ToListAsync();
            }
        }

        return result;
    }

    public async Task SaveCustomProgram(CustomProgram customProgram)
    {
        await init();

        await _connection.InsertAsync(customProgram);

        foreach (var item in customProgram.Actions)
        {
            item.CustomProgramId = customProgram.Id;
            var id = await _connection.InsertAsync(item);

            foreach (var itemProgramAction in item.ProgramActions)
            {
                itemProgramAction.ProgramActionResultId = item.Id;
                await _connection.InsertAsync(itemProgramAction);
            }

        }
    }

    public async Task<List<string>> SearchForName(string name)
    {
        await init();

        var result = await _connection.Table<ProgramAction>().Where(x => x.Name.Contains(name)).ToListAsync();

        return result.Select(x => x.Name).Distinct().Take(5).ToList();
    }

    public async Task SaveRecord(int? record, int ProgramActionId)
    {
        await init();

        await _connection.InsertAsync(new ActionRecord() { ProgramActionId = ProgramActionId, Record = record });
    }

    public async Task RemoveExercisePrograms(int Id)
    {
        await _connection.Table<CustomProgram>().DeleteAsync(x => x.Id == Id);
        await _connection.Table<ProgramActionResult>().DeleteAsync(x => x.CustomProgramId == Id);
    }
    public async Task SaveProgramActionResult(ProgramActionResult customProgram)
    {
        foreach (var action in customProgram.ProgramActions)
        {
            action.ProgramActionResultId = customProgram.Id;
        }

        await _connection.Table<ProgramAction>().DeleteAsync(x => x.ProgramActionResultId == customProgram.Id);
        await _connection.InsertAllAsync(customProgram.ProgramActions);
    }
}