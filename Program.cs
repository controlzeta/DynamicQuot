using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace StringHandling
{
    class Program
    {
        public class HandlingStrings
        {
            public string oldString = "Estamos en Querétaro sin hacer nada";

            public string[] states = { "Aguascalientes", "Baja California", "Baja California Sur", "Campeche", "Chiapas", "Chihuahua", "Coahuila", "Colima", 
                             "Distrito Federal", "Durango", "Estado de México", "Guanajuato", "Guerrero", "Hidalgo", "Jalisco", "Michoacán", "Morelos",
                              "Nayarit", "Nuevo León", "Oaxaca", "Puebla", "Querétaro", "Quintana Roo", "San Luis Potosí", "Sinaloa", "Sonora", 
                              "Tabasco", "Tamaulipas", "Tlaxcala", "Veracruz", "Yucatán", "Zacatecas"};

            public int[] intArr = new int[3];

            public Random rand = new Random();

            public int[] findState(string oldString)
            {
                oldString = RemoveDiacritics(oldString);
                int i = 0;
                foreach (var e in states)
                {
                    i++;
                    if (oldString.ToUpper().Contains(RemoveDiacritics(e.ToUpper())))
                    {
                        int start = oldString.ToUpper().IndexOf(RemoveDiacritics(e.ToUpper()));
                        int end = start + e.Length;
                        Console.WriteLine("keyword: " + RemoveDiacritics(e.ToUpper()) + " Found it! at: " + start + " - " + end);
                        Console.WriteLine(oldString.Substring(start, e.Length));
                        Console.WriteLine(i);
                        intArr = new int[] { start, e.Length, i };
                        return intArr;
                    }
                }
                return null;
            }

            public string ChangeState(string oldString)
            {
                string[] myStates = (string[])states.Clone();
                string newString = "";
                if (findState(oldString) != null)
                {
                    string getOff = oldString.Substring(intArr[0], intArr[1]);
                    string estado = myStates[intArr[2]];
                    newString = oldString.Replace(getOff, getRandomState(estado, myStates));

                }
                return newString;
            }

            public string getRandomState(string current, string[] states)
            {
                string state = "";
                do
                {
                    state = states[rand.Next(0, states.Length)];

                } while (current == state);
                return state;
            }

            static string RemoveDiacritics(string text)
            {
                return string.Concat(
                    text.Normalize(NormalizationForm.FormD)
                    .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                                  UnicodeCategory.NonSpacingMark)
                  ).Normalize(NormalizationForm.FormC);
            }

        }

        public class HandlingTime
        {
            public Random rand = new Random();

            public DateTime getRandomTime()
            {
                return new DateTime().AddHours(rand.Next(0, 24));
            }

            public DateTime getRandomDate()
            {
                return new DateTime().AddDays(rand.Next(0, 31));
            }

            public string forDaysOfTheWeek(DateTime date)
            {
                switch (date.DayOfWeek.ToString())
                {
                    case "Sunday":
                        return "Hoy es domingo";
                    case "Monday":
                        return "Hoy es lunes";
                    case "Tuesday":
                        return "Hoy es martes";
                    case "Wednesday":
                        return "Hoy es miercoles";
                    case "Thursday":
                        return "Hoy es Jueves";
                    case "Friday":
                        return "Hoy es Viernes";
                    case "Saturday":
                        return "Hoy es Sabado";
                }
                return "";
            }

            public string forTimeOfTheDay(DateTime Time)
            {
                TimeSpan time = new TimeSpan();
                time = Time.TimeOfDay;
                TimeSpan dawnStart = TimeSpan.Parse("00:01"); // 00:01 AM
                TimeSpan dawnEnd = TimeSpan.Parse("04:59");   // 04:59 AM
                TimeSpan morningStart = TimeSpan.Parse("05:01"); // 05:01 AM
                TimeSpan morningEnd = TimeSpan.Parse("12:00");   // 12:00 PM
                TimeSpan afternoonStart = TimeSpan.Parse("12:01"); // 12:01 PM
                TimeSpan afternoonEnd = TimeSpan.Parse("19:00");   // 07:00 PM
                TimeSpan nightStart = TimeSpan.Parse("19:01"); // 07:01 PM
                TimeSpan nightEnd = TimeSpan.Parse("23:59");   // 11:59 PM
                if (time > dawnStart && time <= dawnEnd)
                {
                    return "Es de Madrugada";
                }
                if (time > morningStart && time <= morningEnd)
                {
                    return "Es de mañana";
                }
                if (time > afternoonStart && time <= afternoonEnd)
                {
                    return "Es de tarde";
                }
                if (time > nightStart && time <= nightEnd)
                {
                    return "Es de Noche ";
                }
                return "esta fuera de horario";

            }

        }

        static void Main(string[] args)
        {
            HandlingStrings hs = new HandlingStrings();
            HandlingTime ht = new HandlingTime();

            for (int i = 0; i < 11; i++)
            {
                //Console.WriteLine("String viejo: ");
                //Console.WriteLine(hs.oldString);
                //Console.WriteLine("String nuevo: ");
                Console.WriteLine(hs.ChangeState(hs.oldString));
                Console.WriteLine("Son las: " + ht.getRandomTime().ToShortTimeString() + " del : " + ht.getRandomDate());
                Console.WriteLine(ht.forDaysOfTheWeek(ht.getRandomDate()));
                Console.WriteLine(ht.forTimeOfTheDay(ht.getRandomTime()));
            }

            Console.ReadKey();
        }
    }
}
