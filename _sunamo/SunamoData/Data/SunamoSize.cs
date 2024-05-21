namespace SunamoShared;


public class SunamoSize //: IParser
{
    public double Width { get; set; }
    public double Height { get; set; }
    public SunamoSize()
    {
    }
    public SunamoSize(double width, double height)
    {
        Width = width;
        Height = height;
    }
    public bool IsNegativeOrZero()
    {
        bool w = Width <= 0;
        bool h = Height <= 0;
        return w || h;
    }
    public void Parse(string input)
    {
        var d = input.Split(',');
        //ParserTwoValues.ParseDouble(AllStrings.comma, SHParts.RemoveAfterFirstFunc(input, char.IsLetter, new char[] { AllChars.comma }));
        Width = double.Parse(d[0]);
        Height = double.Parse(d[1]);
    }
    public override string ToString()
    {
        //return ParserTwoValues.ToString(AllStrings.comma, Width.ToString(), Height.ToString());
        return Width + "," + Height;
    }
}