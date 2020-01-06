using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication
{
    class Program
    {
        private static string textToParse =
                "analogWrite(nogi_S, 40); "
        +
        "delay(cykl); " +
        "analogWrite(nogi_P, 160);" +
        "delay(cykl);" +
        "analogWrite(nogi_L, 160);" +
        "delay(cykl);" +
        "analogWrite(nogi_S, 160);" +
        "delay(cykl);" +
        "analogWrite(nogi_P, 40);" +
        "delay(cykl);" +
        "analogWrite(nogi_L, 40);" +
        "delay(cykl); "
        ;
        private static LegDriver currDriver;
        private static List<LegDriver> drivers;
        static void Main(string[] args)
        {
            CommandParser p = new CommandParser();
            drivers = p.ParseTextToDriversList(textToParse);
            if (drivers.Any())
            {
                currDriver = drivers.First();
            }
            while (true) // w Unity działa niejako w pętli wywołując metodę Update
            {
                currDriver.PerformMove();
                if (currDriver.MoveFinished)
                {
                    currDriver = drivers[drivers.IndexOf(currDriver) + 1];
                }
            }
        }
    }
}
