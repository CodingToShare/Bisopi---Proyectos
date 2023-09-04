namespace Bisopi___Proyectos.Calendar
{
    public class Day
    {
        public string Abbreviation { get; set; } = string.Empty;
        public int Number { get; set; }

        public Day(string abbreviation, int number)
        {
            Abbreviation = abbreviation;
            Number = number;
        }
    }
}
