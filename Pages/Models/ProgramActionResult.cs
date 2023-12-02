using System.Text.Json;
using SQLite;

namespace Pages.Models;

public class ProgramActionResult
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int CustomProgramId { get; set; }


    [Ignore]
    public List<ProgramAction> ProgramActions { get; set; }

    public string SelectedEnumProvidersstr 
    {
        get => JsonSerializer.Serialize(SelectedEnumProviders);
        set => SelectedEnumProviders = JsonSerializer.Deserialize<List<ActionTimeEnum>>(value);
    }
    [Ignore]
    public List<ActionTimeEnum> SelectedEnumProviders { get; set; }

    public ProgramActionResult()
    {
        
    }

    public ProgramActionResult(List<ProgramAction> programActions, List<ActionTimeEnum> selectedEnumProviders)
    {
        ProgramActions = programActions;
        SelectedEnumProviders = selectedEnumProviders;
    }
}