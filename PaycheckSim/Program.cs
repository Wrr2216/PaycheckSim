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

            double _hoursSa = AnsiConsole.Ask<double>("[green]Saturday Hours?[/]");
                Console.Clear();

            double _hoursSu = AnsiConsole.Ask<double>("[green]Sunday Hours?[/]");
                Console.Clear();

            double _hoursMo = AnsiConsole.Ask<double>("[green]Monday Hours?[/]");
                Console.Clear();

            double _hoursTu = AnsiConsole.Ask<double>("[green]Tuesday Hours?[/]");
                Console.Clear();

            double _hoursWe = AnsiConsole.Ask<double>("[green]Wednesday Hours?[/]");
                Console.Clear();

            double _hoursTh = AnsiConsole.Ask<double>("[green]Thursday Hours?[/]");
                Console.Clear();

            double _hoursFr = AnsiConsole.Ask<double>("[green]Friday Hours?[/]");
                Console.Clear();

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Pong)
                .Start("Doing math stuff...", ctx =>
                {
                    addDay("Saturday", _hoursSa);
                    addDay("Sunday", _hoursSu);
                    addDay("Monday", _hoursMo);
                    addDay("Tuesday", _hoursTu);
                    addDay("Wednesday", _hoursWe);
                    addDay("Thursday", _hoursTh);
                    addDay("Friday", _hoursFr);
                    Thread.Sleep(2000);
                });

            drawCont(_wd);
        }

        static public void addDay(string _day, double _totalHours)
        {
            WorkDay _tempWD = new WorkDay();
            _tempWD.Day = _day;
            _tempWD.totalHours = _totalHours;
            _tempWD.otHours = _totalHours - 12;
            _tempWD.regHours = 12;

            _wd.Add(_tempWD);
        }

        static public double getEarned(double _hours)
        {
            double _reg = 12;
            double _ot = _hours - _reg;
            double _earned = 0;
            double _hourlyRate = 47.0;
            double _otRate = 70.5;

            _earned = (_reg * Convert.ToInt32(_hourlyRate)) + (_ot * Convert.ToInt32(_otRate));

            return _earned;
        }

        static public double getTotalEarned(double _hours)
        {
            double _reg = 40;
            double _ot = _hours - 40;
            double _earned = 0;
            double _hourlyRate = 47.0;
            double _otRate = 70.5;

            _earned = (_reg * Convert.ToInt32(_hourlyRate)) + (_ot * Convert.ToInt32(_otRate));

            return _earned;
        }

        static public (double SS, double MEDI, double STATE, double FED, double OVERALL) getDeductions(double _totalHours)
        {

            double _earned = getTotalEarned(_totalHours);
            double _totalDed = (_earned * 0.062) + (_earned * 0.0145) + (_earned * 0.059) + (_earned * 0.28);
            double _ss = (_earned * 0.062);
            double _medi = (_earned * 0.0145);
            double _state = (_earned * 0.059);
            double _fed = (_earned * 0.28);

            return (_ss, _medi, _state, _fed, _totalDed);
        }

        static void drawCont(List<WorkDay> _workdays)
        {

            double _overallHours = 0;
            var _table = new Table();
            _table.AddColumns("[yellow]Day[/]", "[yellow]Reg[/]", "[yellow]Overtime[/]", "[yellow]Total Hours[/]", "[yellow]Earned[/]");



            foreach (WorkDay _day in _workdays)
            {
                _overallHours = _overallHours + _day.totalHours;
                _table.AddRow(
                    String.Format("{0}", _day.Day),
                    String.Format("{0}", _day.regHours),
                    String.Format("{0}", _day.otHours),
                    String.Format("{0}", _day.totalHours),
                    String.Format("[green]${0}[/]", getEarned(_day.totalHours))
                    );
            }

            _table.AddRow(
                String.Format("[springgreen1]{0}[/]", "TOTALS"),
                String.Format("[springgreen1]{0}[/]", 40),
                String.Format("[springgreen1]{0}[/]", _overallHours - 40),
                String.Format("[springgreen1]{0}[/]", _overallHours),
                String.Format("[springgreen1]${0}[/]", getTotalEarned(_overallHours))
                );

            var _ded = getDeductions(_overallHours);
            _table.AddRow("[orange3]TOTAL DED[/]", "[orange3]SS[/]", "[orange3]MEDI[/]", "[orange3]STATE (AR)[/]", "[orange3]FED[/]");
            _table.AddRow(
                String.Format("[red]${0}[/]", _ded.OVERALL),
                String.Format("[red]${0}[/]", _ded.SS),
                String.Format("[red]${0}[/]", _ded.MEDI),
                String.Format("[red]${0}[/]", _ded.STATE),
                String.Format("[red]${0}[/]", _ded.FED)
                );

            AnsiConsole.Write(_table);

            Console.ReadLine();
        }
    }
}
