using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemGenerator
{
    public static class Generator
    {
        static Random rand = new Random();
        public delegate int Adder(int x);

        public static string Generate()
        {
            Tables.TwoHeaps(2, 3, 48);
            return "";

            //Adder add = (x) => x + rand.Next(1, 4);
            //Adder mult = (x) => x * rand.Next(1, 4);
            //Adder add = (x) => x + 1;
            //Adder mult = (x) => x * 2;

            // Количество действий
            int numOfActions = rand.Next(2, 5);
            // Массив действий
            Adder[] actions = new Adder[numOfActions];
            int[] toAdd = new int[numOfActions - 1];
            int num;
            // Добавляем действия сложения (все действия - 1), чтобы не повторялось
            for (int i = 0; i < numOfActions - 1; i++)
            {
                do
                {
                    num = rand.Next(1, 4);
                } while (toAdd.Contains(num));
                actions[i] = (x) => x + num;
            }
            // Действие умножения (пусть будет одно)
            int toMult = rand.Next(1, 4);
            actions[numOfActions-1] = (x) => x * toMult;

            // Количество, чтобы выиграть
            int toWin = rand.Next(25, 66);

            // 50 на 50 - задача с верхней границей или без
            if (rand.Next(2) == 0)
                return Tables.OneHeap(actions, toWin, toWin * toMult - rand.Next(8, 14));
            else return Tables.OneHeap(actions, toWin, 100000);
        }
    }
}
