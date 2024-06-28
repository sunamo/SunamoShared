namespace SunamoShared;


internal class SunamoSize //: IParser
{
    internal double Width { get; set; }
    internal double Height { get; set; }
    internal SunamoSize()
    {
    }
    internal SunamoSize(double width, double height)
    {
        Width = width;
        Height = height;
    }
    internal bool IsNegativeOrZero()
    {
        bool w = Width <= 0;
        bool h = Height <= 0;
        return w || h;
    }
    internal void Parse(string input)
    {
        var d = input.Split(',');
        //ParserTwoValues.ParseDouble(AllStrings.comma, SHParts.RemoveAfterFirstFunc(input, char.IsLetter, new char[] { AllChars.comma }));
        Width = double.Parse(d[0]);
        Height = double.Parse(d[1]);
    }
    internal override string ToString()
    {
        //return ParserTwoValues.ToString(AllStrings.comma, Width.ToString(), Height.ToString());
        return Width + "," + Height;
    }
}