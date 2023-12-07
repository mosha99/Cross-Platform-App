using Pages.Models.StorageModel;

namespace Pages.Models;

public class DaysDecorator : IProgramListDecorator
{
    CustomProgram CustomProgram;
    private readonly ActionTimeEnum TimeEnum;

    public DaysDecorator(CustomProgram customProgram, ActionTimeEnum timeEnum)
    {
        CustomProgram = customProgram;
        TimeEnum = timeEnum;
    }

    public void Decorate(List<ProgramAction> programActions)
    {
        var day = CustomProgram.Actions
            .Where(x => x.SelectedEnumProviders?
                .Contains(TimeEnum) == true)
            .SelectMany(x => x.ProgramActions);

        programActions.AddRange(day);
    }
}