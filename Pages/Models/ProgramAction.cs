using Pages.Models.StorageModel;
using SQLite;

namespace Pages.Models;

public class ProgramAction
{
    public ProgramAction()
    {

    }
    public ProgramAction(ProgramAction Action)
    {
        Id = Action.Id;
        ProgramActionResultId = Action.ProgramActionResultId;
        Name = Action.Name;
        DescriptionVisibility = Action.DescriptionVisibility;
        Description = Action.Description;
        CountPreSet = Action.CountPreSet;
        SetCount = Action.SetCount;
        Super = Action.Super;
        ShowRecord = Action.ShowRecord;
        ParentSuper = Action.ParentSuper;
        TimeVisibility = Action.TimeVisibility;
        Time = Action.Time;
        Records = Action.Records;
    }

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int ProgramActionResultId { get; set; }

    public string? Name { get; set; }
    public bool DescriptionVisibility { get; set; }
    public string? Description { get; set; }
    public int? CountPreSet { get; set; } = null;
    public int? SetCount { get; set; } = null;
    public bool Super { get; set; }

    [Ignore]
    public bool ShowRecord { get; set; }

    public bool ParentSuper { get; set; }
    public bool TimeVisibility { get; set; }
    public int? Time { get; set; } = null;
    [Ignore]
    public List<ActionRecord> Records { get; set; } = new List<ActionRecord>();
}