using SQLite;

namespace Pages.Models.StorageModel;

public class ActionRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int ProgramActionId { get; set; }
    public int? Record { set; get; } = null;
}

