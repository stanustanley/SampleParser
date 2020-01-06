using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SampleApplication.Enum;

namespace SampleApplication
{
    public class Command
    {
        public CommandType Type { get; set; }
        public float Range { get; set; }
        public Command(string text)
        {
            this.Type = GetCommandType(text);
            this.Range = GetNumber(text);
        }
        private CommandType GetCommandType(string command)
        {
            CommandType moveType;
            string strCommand = command.Split('(')[0].Trim();
            switch (strCommand)
            {
                case "delay":
                    moveType = CommandType.wait;
                    break;
                case "analogWrite":
                    moveType = ParseAnalogWriteMethod(command);
                    break;
                default:
                    moveType = CommandType.unknown;
                    // Debug.Log("Error with: " + command);
                    break;
            }
            return moveType;

        }
        private CommandType ParseAnalogWriteMethod(string command)
        {
            CommandType moveType;
            string strMove = command.Split('(')[1].Split(',')[0].Trim(); // analogWrite(nogi_S,1); => nogi_S

            switch (strMove)
            {
                case "nogi_S":
                    moveType = CommandType.nogi_S;
                    break;
                case "nogi_P":
                    moveType = CommandType.nogi_P;
                    break;
                case "nogi_L":
                    moveType = CommandType.nogi_L;
                    break;
                default:
                    moveType = CommandType.unknown;
                    // Debug.Log("Error with: " + command);
                    break;
            }
            return moveType;

        }

        private int GetNumber(string command)
        {
            CommandType moveType = this.Type;
            if (moveType == CommandType.wait)
            {
                string strTimeMs = command.Split('(')[1].Split(')')[0].Trim();
                int timeMs = 0;
                try
                {
                    timeMs = int.Parse(strTimeMs);
                }
                catch (FormatException ex) // w późniejszym czasie pobranie zmiennej od użytkownika
                {
                    // timeMs = DotBehaviorScript.delayVariable;
                }
                return timeMs;
            }
            if (moveType == CommandType.unknown)
            {
                return 0;
            }
            else
            {
                string strRange = command.Split(',')[1].Split(')')[0].Trim();
                int range = 0;
                try
                {
                    range = int.Parse(strRange);
                }
                catch (Exception ex)
                {
                    // Debug.Log(ex.Message);
                    moveType = CommandType.unknown;
                    range = 0;
                }
                return range;
            }
        }
        private int GetRangeFromUser(string variable)
        {
            int i = 0;
            Console.WriteLine($"Podaj wartość zmiennej {variable}");
            string x = Console.ReadLine();
            try
            {
                i = int.Parse(x);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Podana wartość nie jest liczbą");
                i = GetRangeFromUser(variable);
            }
            return i;

        }

    }
}
