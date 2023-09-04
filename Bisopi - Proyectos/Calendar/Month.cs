using Bisopi___Proyectos.Extensions;
using System.Globalization;

namespace Bisopi___Proyectos.Calendar
{
    public class Month
    {
        public string Name { get; set; } = string.Empty;
        public List<Week> Weeks { get; set; } = new List<Week>();


        public int MonthNumber = 0;
        public int MonthYear = 0;

        public Month(int monthNumber, int monthYear)
        {
            MonthNumber = monthNumber;
            MonthYear = monthYear;

            Initialize();
        }

        public void Initialize()
        {
            Weeks.Clear();

            var date = new DateTime(MonthYear, MonthNumber, 1);
            var ci = new CultureInfo("es-ES");

            Name = date.ToString("MMMM", ci);

            DateTime? lastWeekDate = null;

            while (date.Month <= MonthNumber)
            {
                var startOfWeek = date.StartOfWeek(DayOfWeek.Monday);

                if (startOfWeek != lastWeekDate)
                {
                    lastWeekDate = startOfWeek;

                    Weeks.Add(new Week(startOfWeek, MonthNumber));
                }

                date = date.AddDays(1);
            }
        }
    }
}
