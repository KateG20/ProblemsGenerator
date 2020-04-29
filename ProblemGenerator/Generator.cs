using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProblemGenerator
{
    public static class Generator
    {
        static Random rand = new Random();
        public delegate int Adder(int x);
        public delegate string[] GenerateType();

        public static int ProblemType { get; set; }
        public static int ProblemsNum { get; set; }

        /// <summary>
        /// Генерирует заданное пользователем количество задач заданного типа
        /// </summary>
        /// <returns>Двумерный массив - на первой строке условия, на второй - ответы</returns>
        public static string[,] Generate()
        {
            GenerateType GenerateMethod;
            if (ProblemType == 0) GenerateMethod = GenOneHeap;
            else if (ProblemType == 1) GenerateMethod = GenTwoHeaps;
            else GenerateMethod = GenTwoWords;

            string[,] problems = new string[2, ProblemsNum];
            for (int i = 0; i < ProblemsNum; i++)
            {
                problems[0, i] = $"Задача {i + 1}<br>" + GenerateMethod()[0];
                problems[1, i] = GenerateMethod()[1];
            }
            return problems;

            //string[] tasks = new string[ProblemsNum];
            //string[] answers = new string[ProblemsNum];
            //for (int i = 0; i < ProblemsNum; i++)
            //{
            //    tasks[i] = $"Задача {i+1}<br>" + GenerateMethod()[0];
            //    answers[i] = GenerateMethod()[1];
            //}


            //Tables.TwoHeaps(2, 3, 48);

            //Adder add = (x) => x + rand.Next(1, 4);
            //Adder mult = (x) => x * rand.Next(1, 4);
            //Adder add = (x) => x + 1;
            //Adder mult = (x) => x * 2;
        }


        /// <summary>
        /// Геренирует задачу и ответ типа "одна куча камней"
        /// </summary>
        /// <returns>Массив из двух строк - условие и ответ</returns>
        static string[] GenOneHeap()
        {
            // Количество действий
            int numOfActions = rand.Next(2, 5);
            // Массив действий
            Adder[] actions = new Adder[numOfActions];
            // Действия сложения 
            int[] toAdd = new int[numOfActions - 1];
            int num;
            // Добавляем действия сложения (все действия - 1), так, чтобы не повторялось
            for (int i = 0; i < numOfActions - 1; i++)
            {
                do
                {
                    num = rand.Next(1, 4);
                } while (toAdd.Contains(num));
                toAdd[i] = num;
                actions[i] = (x) => x + num;
            }
            Array.Sort(toAdd);

            // Действие умножения (пусть будет одно)
            int toMult = rand.Next(2, 4);
            actions[numOfActions - 1] = (x) => x * toMult;

            // Количество камней для выигрыша
            int toWin = rand.Next(25, 66);

            string[] table;
            // Верхнее ограничение для выигрыша, если оно есть.
            int upperBound = (toWin - toAdd.Max()) * toMult - rand.Next(8, 14);
            int isUpperBounded = 1;// rand.Next(2);
            string additionalString = "";


            // 50 на 50 - задача с верхней границей или без
            if (isUpperBounded == 1)
            {
                table = Tables.OneHeap(actions, toWin, upperBound);
                additionalString = $"Если при этом в куче оказалось<br>не более {upperBound} камней, " +
                "то победителем считается игрок, сделавший последний ход. В противном случае победителем<br>" +
                "становится его противник. ";
            }
            else table = Tables.OneHeap(actions, toWin, 100000);


            /////////////////////////////////////////////////////// фальшивая табличка
            //toAdd = new int[] { 1,2,3 };
            //toMult = 3;
            //toWin = 61;
            //upperBound = 110;
            //table = Tables.OneHeap(new Adder[] { x => x + 1, x => x + 2, x => x + 3, x => x * 3 }, 61, 110);

            // 1б - рандомно 1-2 минуса и плюса из переда таблицы
            // 2 - первый минус и первый плюс в основной части
            // 3 - второй минус (или второй плюс)

            /// Вопрос 1б
            // Создаем два списка с неколькими проигрышными и выигрышными клетками.
            List<int> fastLoses = new List<int>();
            List<int> fastWins = new List<int>();

            // Список клеток, из которых можно выиграть первым ходом.
            List<int> oneMoveWins = new List<int>();

            foreach (var term in toAdd)
            {
                for (int i = toWin - 1; i >= toWin - term; i--)
                {
                    if (!oneMoveWins.Contains(i)) 
                        oneMoveWins.Add(i);
                }
            }
            //MessageBox.Show(string.Join(" ", oneMoveWins));

            // Количество выигрышных клеток подряд.
            //int currWins = 0;

            // Проходимся во всей таблице с конца, добавляя номера клеток 
            // в соответствующие списки.
            for (int i = table.Length - 2; i > 0; i--)
            {
                // Добавляем выигрышные не в первый ход (они в вопросе 1а) и проигрышные клетки
                if (table[i] == "+1")
                {
                    //++currWins;
                    //// Когда встретили уже три подряд выигрышные клетки, то 
                    //// скорей всего искать уже нечего, т.к. максимум операция добавления 
                    //// добавляет три камня, а значит в голове таблицы может быть максимум 
                    //// две выигрышных клетки подряд
                    //if (currWins == 3) break;
                    if (!oneMoveWins.Contains(i))
                        fastWins.Add(i);
                }
                else
                {
                    fastLoses.Add(i);
                    //currWins = 0;
                }
                // Одного проигрыша и двух выигрышей достаточо
                if (fastWins.Count >= 2 && fastLoses.Count >= 1) break;
            }

            // Создали список всех подходящих номеров клеток для вопроса 1б
            List<int> quest1bInts = new List<int>();
            int indToAdd;

            // Добавляем две выигрышные клетки из найденных (вдруг их больше двух)
            for (int i = 0; i < 2; i++)
            {
                indToAdd = rand.Next(fastWins.Count);
                quest1bInts.Add(fastWins[indToAdd]);
                // После добавления удаляем этот элемент, чтобы не было повторений
                fastWins.RemoveAt(indToAdd);
            }
             
            // Добавляем одну проигрышную
            quest1bInts.Add(fastLoses[rand.Next(fastLoses.Count)]);
            

            ///////////// по-моему дичь какая-то, зачем я это вообще писала
            /*
            int cellToAdd = 0;
            // Добавляем две выигрышные клетки
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    cellToAdd = fastWins[rand.Next(fastWins.Count)];
                    MessageBox.Show("fast wins! " + string.Join(" ", fastWins) + ".\n" + 
                        "fast loses " + string.Join(" ", fastLoses) + ".\n" + i.ToString());
                }
                while (quest1bInts.Contains(cellToAdd));
                quest1bInts.Add(cellToAdd);
            }

            // И одну или две проигрышные
            for (int i = 0; i < rand.Next(1, 3); i++)
            {
                do
                {
                    try
                    {
                        cellToAdd = fastLoses[rand.Next(fastLoses.Count)]; // index out of range??
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Exception! fast wins " + string.Join(" ", fastWins) + ".\n" + 
                            "fast loses! " + string.Join(" ", fastLoses) + ".\n" + i.ToString());
                        Environment.Exit(0);
                    }
                }
                while (quest1bInts.Contains(cellToAdd));
                quest1bInts.Add(cellToAdd);
            }
            */

            // Сортируем
            quest1bInts.Sort();

            // Запоминаем строкой
            string quest1b = string.Join(", ", quest1bInts);

            /// Вопросы 2 и 3
            // Создаем переменные для первых и вторых плюсов и минусов
            string quest2Win, quest2Lose, quest3Win, quest3Lose;
            quest2Win = quest2Lose = quest3Win = quest3Lose = string.Empty;
            // Проходимся примерно с середины таблицы в начало
            for (int i = upperBound / toMult; i > 0; i--)
            {
                // Если клетка проигрыша, то проверям, первая или вторая, и записываем ее
                if (table[i] == "-1")
                {
                    if (quest2Lose.Length == 0)
                        quest2Lose = i.ToString();
                    else if (quest2Lose.Length > 0 && quest2Win.Length > 0 && quest3Lose.Length == 0)
                        quest3Lose = i.ToString();
                }
                // Если клетка выигрыша, то тоже проверяем, а если это вторая выигрышная,
                // то выходим из цикла - данные нам больше не нужны
                if (table[i] == "+1")
                {
                    if (quest2Lose.Length > 0 && quest2Win.Length == 0) quest2Win = i.ToString();
                    else if (quest3Lose.Length > 0)
                    {
                        quest3Win = i.ToString();
                        break;
                    }
                }
            }
            
            // Выбираем рандомом, чтобы в третьем вопросе выводил клетку - или +
            string quest3;
            if (rand.Next(2) == 1) quest3 = quest3Win;
            else quest3 = quest3Lose;

            // Шаблон текста
            string text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежит куча " +
                "камней. Игроки ходят по очереди,<br>первый ход делает Петя. За один ход игрок может:<br>" +
                "- добавить в кучу любое допустимое количество камней: {0}, или<br>" +
                "- увеличить количество камней в куче в {1} раза.<br>" +
                "Игра завершается в тот момент, когда количество камней в куче становится не менее {2}. " +
                "{3}В начальный момент в куче было S камней, 1 ≤ S ≤ {4}.<br>" +
                "Задание 1.<br>" +
                "а) При каких значениях числа S Петя может выиграть в один ход? Укажите все такие " +
                "значения и соответствующие ходы Пети.<br>" +
                "б) У кого из игроков есть выигрышная стратегия при S = {5}? Опишите " +
                "выигрышные стратегии для этих случаев.<br>" +
                "Задание 2. У кого из игроков есть выигрышная стратегия при S = {6}? " +
                "Опишите соответствующие выигрышные стратегии.<br>" +
                "Задание 3. У кого из игроков есть выигрышная стратегия при S = {7}? Постройте дерево " +
                "всех партий, возможных при этой<br>выигрышной стратегии (в виде рисунка или таблицы). " +
                "На рёбрах дерева указывайте, кто делает ход, в узлах – количество<br>камней в позиции.";

            // Строка для форматирования шаблона
            string[] data = new string[] { string.Join(", ", toAdd), toMult.ToString(),
                toWin.ToString(), additionalString, (toWin-1).ToString(), quest1b,
                string.Join(", ", new string[] { quest2Win, quest2Lose }), quest3 };

            string tableString = "<br>";
            for (int i = 1; i < table.Length; i++)
            {
                tableString += $"{i}{table[i][0]}";
            }
            //string task = string.Format(text, data);
            //string answer = "Решение\n1, a. {0}.\n1, б. {1}.\n2. {3}.\n3. {4}\nРазвернутые ответы не генерируются";
            //string ans1a = string.Join(", ", oneMoveWins);

            return new string[] { string.Format(text, data) + tableString, "solution" };
        }

        static string[] GenTwoHeaps()
        {
            return new string[2];
        }

        static string[] GenTwoWords()
        {
            return new string[2];
        }
    }
}
