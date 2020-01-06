using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static SampleApplication.Enum;

namespace SampleApplication
{
    public class CommandParser : MonoBehaviour // klasa Unity
    {
        public List<LegDriver> ParseTextToDriversList(string text)
        {
            List<string> stringCommands = new List<string>();
            List<Command> commands = new List<Command>();
            stringCommands = text.Trim().Split(';').ToList();
            stringCommands.Remove("");
            List<LegDriver> drivers = new List<LegDriver>();
            foreach (string command in stringCommands)
            {
                commands.Add(new Command(command));
            }
            foreach (Command command in commands)
            {
                drivers.Add(ParseCommandToDriver(command));
            }
            return drivers;

        }
        private LegDriver ParseCommandToDriver(Command command)
        {

            LegDriver driver;
            switch (command.Type)
            {
                case CommandType.wait:
                    driver = new LegDriver(null, command.Range);
                    break;
                case CommandType.nogi_L:
                    driver = new LegDriver(new List<Leg> { FindObjectOfType<LeftLegBottom>(), FindObjectOfType<LeftLegTop>() }, command.Range);
                    break;
                case CommandType.nogi_P:
                    driver = new LegDriver(new List<Leg> { FindObjectOfType<RightLegBottom>(), FindObjectOfType<RightLegTop>() }, command.Range);
                    break;
                case CommandType.nogi_S:
                    driver = new LegDriver(new List<Leg> { FindObjectOfType<RightLegMiddle>(), FindObjectOfType<LeftLegMiddle>() }, command.Range);
                    break;
                case CommandType.unknown:
                   // Debug.Log("Nieznany typ ruchu lub błąd konwersji");
                    throw new Exception("Nieznany typ ruchu");
                default:
                    throw new Exception("Nieznany typ ruchu lub błąd konwersji");
                    break;
            }
            return driver;
        }
    }
}
