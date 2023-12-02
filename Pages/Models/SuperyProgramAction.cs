namespace Pages.Models;

public class SuperyProgramAction : ProgramAction
{
    public SuperyProgramAction(ProgramAction action)
    {
        Id = action.Id;
        ProgramActionResultId = action.ProgramActionResultId;
        Name = action.Name;
        DescriptionVisibility = action.DescriptionVisibility;
        Description = action.Description;
        CountPreSet = action.CountPreSet;
        SetCount = action.SetCount;
        Super = action.Super;
        ShowRecord = action.ShowRecord;
        ParentSuper = action.ParentSuper;
        TimeVisibility = action.TimeVisibility;
        Time = action.Time;
        Records = action.Records;
    }
    public List<ProgramAction> ChildeActions { get; set; } = new List<ProgramAction>();
}