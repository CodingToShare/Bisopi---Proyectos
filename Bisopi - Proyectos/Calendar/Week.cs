using System.Globalization;

namespace Bisopi___Proyectos.Calendar
{
    public class Week
    {
        public DateTime StartDate { get; set; }
        public int Month { get; set; }
        public List<Day> Days { get; set; } = new List<Day>();

        public Week(DateTime startDate, int month)
        {
            StartDate = startDate;
            Month = month;

            Initialize();
        }

        private void Initialize()
        {
            Days.Clear();

            var ci = new CultureInfo("es-ES");
            int days = 7;

            var actualDate = StartDate;

            while (days > 0)
            {
                if (actualDate.Month == Month)
                {
                    Days.Add(new Day(actualDate.ToString("ddd", ci)[0].ToString().ToUpper(), actualDate.Day));
                }

                actualDate = actualDate.AddDays(1);

                days--;
            }
        }
    }
}
