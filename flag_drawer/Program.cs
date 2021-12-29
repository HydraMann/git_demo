using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Feladat_1
{
    class Program
    {

        static void Main(string[] args)
        {

            bool[,] IsFilled = new bool[Console.WindowWidth, Console.WindowHeight];

            Console.CursorVisible = false;

            FlagDrawer f1 = new FlagDrawer(1, ConsoleColor.Red);
            FlagDrawer f2 = new FlagDrawer(2, ConsoleColor.White);
            FlagDrawer f3 = new FlagDrawer(3, ConsoleColor.Green);
            
            Thread t1 = new Thread(() => f1.Draw(IsFilled));
            Thread t2 = new Thread(() => f2.Draw(IsFilled));
            Thread t3 = new Thread(() => f3.Draw(IsFilled));

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();

            Console.Beep();
            Console.ReadKey();
        }
    }

    class FlagDrawer
    {
        private int y1;
        private int y2;

        private ConsoleColor color;
        private byte start;



        public FlagDrawer(byte start, ConsoleColor color)
        {
            this.start = start;
            this.color = color;
        }

        public void Draw(bool[,] IsFilled)
        {
            while (Contains(IsFilled))
            {
                Random rnd = new Random();
                y1 = rnd.Next(Console.WindowWidth);
                y2 = rnd.Next(Console.WindowHeight / 3 * (start - 1), Console.WindowHeight / 3 * start);

                if (!IsFilled[y1, y2])
                {
                    lock (typeof(FlagDrawer))
                    {
                        Console.SetCursorPosition(y1, y2);
                        Console.ForegroundColor = color;
                        Console.Write("*");
                        IsFilled[y1, y2] = true;
                    }
                }
            }
        }

        private bool Contains(bool[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}