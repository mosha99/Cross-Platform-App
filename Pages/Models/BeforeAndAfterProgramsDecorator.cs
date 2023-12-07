using Pages.Models.StorageModel;

namespace Pages.Models;

public class BeforeAndAfterProgramsDecorator : IProgramListDecorator
{
    IProgramListDecorator Decorated;
    CustomProgram CustomProgram;

    public BeforeAndAfterProgramsDecorator(IProgramListDecorator decorated, CustomProgram customProgram)
    {
        CustomProgram = customProgram;
        Decorated = decorated;
    }

    public void Decorate(List<ProgramAction> programActions)
    {
        var beforAndAfter =
            CustomProgram.Actions
                .Where(x => x.SelectedEnumProviders?
                    .Contains(ActionTimeEnum.BeforeAndAfterTraining) == true)
                .SelectMany(x => x.ProgramActions)
                .Select(x => new ProgramAction(x));

        var befor = CustomProgram.Actions
            .Where(x => x.SelectedEnumProviders?
                .Contains(ActionTimeEnum.BeforeTraining) == true)
            .SelectMany(x => x.ProgramActions)
            .Select(x => new ProgramAction(x));

        var after = CustomProgram.Actions
            .Where(x => x.SelectedEnumProviders?
                .Contains(ActionTimeEnum.AfterTraining) == true)
            .SelectMany(x => x.ProgramActions)
            .Select(x => new ProgramAction(x));


        programActions.AddRange(beforAndAfter);
        programActions.AddRange(befor);
        Decorated.Decorate(programActions);
        programActions.AddRange(after);
        programActions.AddRange(beforAndAfter);
    }
}