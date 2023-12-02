namespace Pages.Models;

public class EnumProvider
{
    public EnumProvider(Func<object, string> func , object item )
    {
        Func = func;
        Item = item;
    }

    public object Item { set; get; }
    public string Name => ToString();

    public Func<object, string> Func { set; get; }

    public override string ToString()
    {
        return Func(Item);
    }
}