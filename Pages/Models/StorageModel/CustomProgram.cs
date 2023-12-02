using SQLite;

namespace Pages.Models.StorageModel;

public class CustomProgram
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }

    [Ignore]
    public List<ProgramActionResult> Actions { get; set; }
}