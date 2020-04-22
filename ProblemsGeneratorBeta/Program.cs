﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProblemsGeneratorBeta
{
    class Program
    {
        //[STAThread]

        static void InputData(out string difficulty, out int number)
        {
            string info = "Есть два типа задач: " +
                "\n1) Задачи с одной кучей камней" +
                "\n2) Задачи с двумя кучами камней";
            Console.WriteLine(info);
            Console.Write("Выберите тип задачи (1 или 2): ");
            do { difficulty = Console.ReadLine(); } while (difficulty != "1" && difficulty != "2");
            Console.Write("Введите количество задач (от 1): ");
            while (!int.TryParse(Console.ReadLine(), out number) && number < 1) { }
        }

        static string GenText(string[] words)
        {
            string text = "";
            string marker = words[0];
            switch (marker)
            {
                case "0":
                    text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежит куча камней. Игроки " +
                        "ходят\nпо очереди, первый ход делает Петя. За один ход игрок может добавить в кучу {1} или увеличить\nколичество " +
                        "камней в куче в {2}. Например, имея кучу из {3} камней, за один ход можно\nполучить кучу из {4} или {5} " +
                        "камней. У каждого игрока, чтобы делать ходы, есть неограниченное количество камней.\nИгра завершается " +
                        "в тот момент, когда количество камней в куче становится не менее {6}. Победителем\nсчитается игрок, сделавший " +
                        "последний ход, то есть первым получивший кучу, в которой будет {7} или\nбольше камней.\nВ начальный момент " +
                        "в куче было S камней, 1 <= S <= {7}.\n1.При каких S: 1а) Петя выигрывает первым ходом; 1б) Ваня выигрывает " +
                        "первым ходом?\n2.Назовите {8} S, при {9} Петя может выиграть своим вторым ходом.\n3.Назовите {10} S, при {11} " +
                        "Ваня выигрывает своим первым или вторым ходом.";
                    break;
                case "1":
                    text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежит куча камней. Игроки " +
                        "ходят\nпо очереди, первый ход делает Петя. За один ход игрок может добавить в кучу {1} или добавить\nв кучу" +
                        " {2}. Например, имея кучу из {3} камней, за один ход можно получить\nкучу из {4} или {5} " +
                        "камней. У каждого игрока, чтобы делать ходы, есть неограниченное количество камней.\nИгра завершается " +
                        "в тот момент, когда количество камней в куче становится не менее {6}. Победителем\nсчитается игрок, сделавший " +
                        "последний ход, то есть первым получивший кучу, в которой будет {6} или\nбольше камней.\nВ начальный момент " +
                        "в куче было S камней, 1 <= S <= {7}.\n1.При каких S: 1а) Петя выигрывает первым ходом; 1б) Ваня выигрывает " +
                        "первым ходом?\n2.Назовите {8} S, при {9} Петя может выиграть своим вторым ходом.\n3.Назовите {10} S, при {11} " +
                        "Ваня выигрывает своим первым или вторым ходом.";
                    break;
                case "2":
                    text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежит куча камней. Игроки ходят по " +
                        "очереди,\nпервый ход делает Петя. За один ход игрок может {1},\n{2}. Игра завершается в тот момент,\nкогда количество " +
                        "камней в куче превышает {3}. Победителем считается игрок, сделавший последний ход, то есть\nпервым получивший " +
                        "кучу, в которой будет {4} или больше камней.\nВ начальный момент в куче было S камней, 1 <= S <= {3}.\nЗадание 1." +
                        "\nа) При каких значениях числа S Петя может выиграть в один ход? Укажите все такие значения и соответствующие\n" +
                        "ходы Пети.\nб) Укажите такое значение S, при котором Петя не может выиграть за один ход, но при любом ходе " +
                        "Пети Ваня\nможет выиграть своим первым ходом. Опишите выигрышную стратегию Вани.\nЗадание 2. Укажите четыре " +
                        "значения S, при которых у Пети есть выигрышная стратегия, причём Петя не может\nвыиграть первым ходом, но Петя " +
                        "может выиграть своим вторым ходом, независимо от того, как будет ходить\nВаня. Для указанных значений S опишите " +
                        "выигрышную стратегию Пети.\nЗадание 3.Укажите такое значение S, при котором у Вани есть выигрышная стратегия, " +
                        "позволяющая ему выиграть\nпервым или вторым ходом при любой игре Пети, и при этом у Вани нет стратегии, которая " +
                        "позволит ему\nгарантированно выиграть первым ходом. Для указанного значения S опишите выигрышную стратегию Вани.\n" +
                        "Постройте дерево всех партий, возможных при этой выигрышной стратегии Вани (в виде рисунка или таблицы).\nНа рёбрах " +
                        "дерева указывайте, кто делает ход, в узлах – количество камней в позиции.";
                    break;
                case "3":
                    text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежит куча камней. Игроки\nходят по очереди, " +
                        "первый ход делает Петя. За один ход игрок может\n{1}.\nИгра завершается в тот момент, когда количество камней в " +
                        "куче становится не менее {2}. Если при\nэтом в куче оказалось не более {3} камней, то победителем считается " +
                        "игрок, сделавший последний\nход. В противном случае победителем становится его противник.\nВ начальный момент в " +
                        "куче было S камней, 1 <= S <= {4}.\nЗадание 1.\nа) При каких значениях числа S Петя может выиграть в один ход? " +
                        "Укажите все такие значения и\nсоответствующие ходы Пети.\nб) У кого из игроков есть выигрышная стратегия при S = " +
                        "{5}? Опишите выигрышные\nстратегии для этих случаев.\nЗадание 2. У кого из игроков есть выигрышная стратегия при " +
                        "S = {6}? Опишите соответствующие\nвыигрышные стратегии.\nЗадание 3. У кого из игроков есть выигрышная стратегия " +
                        "при S = {7}? Постройте дерево всех\nпартий, возможных при этой выигрышной стратегии (в виде рисунка или таблицы). " +
                        "На рёбрах дерева\nуказывайте, кто делает ход; в узлах – количество камней в позиции.";
                    break;
                case "4":
                    text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежат две кучи камней. Игроки ходят по очереди, " +
                        "первый ход делает Петя. За один ход игрок может добавить в одну из куч (по своему выбору) {1}. Для того, чтобы " +
                        "делать ходы, у каждого игрока есть неограниченное количество камней. Игра завершается в тот момент, когда суммарное " +
                        "количество камней в кучах становится не менее {2}. Победителем считается игрок, сделавший последний ход, то есть " +
                        "первым получивший такую позицию, что в кучах всего будет {2} или больше камней.\nВ начальный момент в первой куче " +
                        "было {3} камней, во второй куче – S камней; 1 <= S <= {4}.\n1.При каких S: 1а) Петя выигрывает первым ходом; 1б) Ваня " +
                        "выигрывает первым ходом?\n2.Назовите одно любое значение S, при котором Петя может выиграть своим вторым ходом.\n" +
                        "3.Назовите значение S, при котором Ваня выигрывает своим первым или вторым ходом.";
                    break;
                case "5":
                    text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежат две кучи камней. Игроки ходят по очереди, " +
                        "первый ход делает Петя. За один ход игрок может добавить в одну из куч (по своему выбору) {1}. Победителем " +
                        "считается игрок, сделавший последний ход, т.е. первым получивший такую позицию, что в обеих кучах всего будет " +
                        "{2} камней или больше.\nЗадание 1. Для каждой из начальных позиций {3} укажите, кто из игроков имеет выигрышную " +
                        "стратегию.\nЗадание 2. Для каждой из начальных позиций {4} укажите, кто из игроков имеет выигрышную стратегию.\n" +
                        "Задание 3. Для начальной позиции {5} укажите, кто из игроков имеет выигрышную стратегию. Постройте дерево всех " +
                        "партий, возможных при указанной выигрышной стратегии.";
                    break;
            }
            return string.Format(text, words);
        }

        static void Main(string[] args)
        {
            Random rand = new Random();
            InputData(out string type, out int number);
            string file = type == "1" ? "../../storage1.txt" : "../../storage2.txt";
            string[] storage = File.ReadAllLines(file);
            int taskNum;
            string[] data;
            for (int i = 0; i < number; i++)
            {
                taskNum = rand.Next(storage.Length);
                data = storage[taskNum].Split(';');
                Console.WriteLine(GenText(data) + Environment.NewLine);
            }
        }
    }
}