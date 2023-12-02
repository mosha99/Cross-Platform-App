using Pages.Models.StorageModel;
using SQLite;

namespace Pages.Models;

public class ProgramAction
{
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
    public int? Time  { get; set; } = null;
    [Ignore]
    public List<ActionRecord> Records { get; set; } = new List<ActionRecord>();

}