using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading;


// Made by WiRR (Logan Miller) 11.6.2021
// Usage: Short little program to play with work hours and print them neatly to a table.

namespace PaycheckSim
{
    class Program
    {
        private static List<WorkDay> _wd = new List<WorkDay>();

        static void Main(string[] args)
        {

            int _hoursSa = AnsiConsole.Ask<int>("[green]Saturday Hours?[/]");
            int _hoursSu = AnsiConsole.Ask<int>("[green]Sunday Hours?[/]");
            int _hoursMo = AnsiConsole.Ask<int>("[green]Monday Hours?[/]");
            int _hoursTu = AnsiConsole.Ask<int>("[green]Tuesday Hours?[/]");
            int _hoursWe = AnsiConsole.Ask<int>("[green]Wednesday Hours?[/]");
            int _hoursTh = AnsiConsole.Ask<int>("[green]Thursday Hours?[/]");
            int _hoursFr = AnsiConsole.Ask<int>("[green]Friday Hours?[/]");

            AnsiConsole.Status()
                .Start("Doing math stuff...", ctx =>
                {
                    addDay("Saturday", _hoursSa);
                    addDay("Sunday", _hoursSu);
                    addDay("Monday", _hoursMo);
                    addDay("Tuesday", _hoursTu);
                    addDay("Wednesday", _hoursWe);
                    addDay("Thursday", _hoursTh);
                    addDay("Friday", _hoursFr);
                    Thread.Sleep(1000);
                });

            drawCont(_wd);
        }

        static public void addDay(string _day, int _totalHours)
        {
            WorkDay _tempWD = new WorkDay();
            _tempWD.Day = _day;
            _tempWD.totalHours = _totalHours;
            _tempWD.otHours = _totalHours - 12;
            _tempWD.regHours = 12;

            _wd.Add(_tempWD);
        }

        static void drawCont(List<WorkDay> _workdays)
        {

            int _overallHours = 0;
            var _table = new Table();
            _table.AddColumns("Day", "Reg", "Overtime", "Total Hours");



            foreach (WorkDay _day in _workdays)
            {
                _overallHours = _overallHours + _day.totalHours;
                _table.AddRow(
                    String.Format("{0}", _day.Day),
                    String.Format("{0}", _day.regHours),
                    String.Format("{0}", _day.otHours),
                    String.Format("{0}", _day.totalHours)
                    );
            }

            _table.AddRow(
                "INFO", Convert.ToString(40),
                Convert.ToString(_overallHours - 40),
                Convert.ToString(_overallHours)
                );

            AnsiConsole.Write(_table);

            Console.ReadLine();
        }
    }
}
