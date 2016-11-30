namespace VHS.System.OutputReports.LineBreaker
{
    public class HtmlLineBreaker : ILineBreaker
    {
        public string AddLineBreak(string line)
        {
            return line + "<br>";
        }
    }
}