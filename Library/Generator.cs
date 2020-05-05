using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

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
            string[] result = new string[1];
            for (int i = 0; i < ProblemsNum; i++)
            {
                try
                {
                    result = GenerateMethod();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Возникла ошибка при генерации задачи.\n" + e.Message);
                    Environment.Exit(0);
                }
                problems[0, i] = $"Задача {i + 1}<br>" + result[0];
                problems[1, i] = result[1];
            }
            return problems;
        }

        /// <summary>
        /// Генерирует заданное пользователем количество задач разных типов, выбирая их рандомно
        /// </summary>
        /// <returns>Двумерный массив - на первой строке условия, на второй - ответы</returns>
        public static string[,] RandomGenerate()
        {
            string[,] problems = new string[2, ProblemsNum];
            string[] result = new string[1];
            // Каждый раз создаем задачу рандомного типа
            for (int i = 0; i < ProblemsNum; i++)
            {
                try
                {
                    switch (rand.Next(3))
                    {
                        case 0:
                            {
                                result = GenOneHeap();
                                break;
                            }
                        case 1:
                            {
                                result = GenTwoHeaps();
                                break;
                            }
                        case 2:
                            {
                                result = GenTwoWords();
                                break;
                            }
                        default:
                            {
                                result = new string[1];
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Возникла ошибка при генерации задачи.\n" + e.Message);
                    Environment.Exit(0);
                }
                problems[0, i] = $"Задача {i + 1}<br>" + result[0];
                problems[1, i] = result[1];
            }
            return problems;
        }

        /// <summary>
        /// Генерирует задачу и ответ для типа "одна куча камней"
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
                actions[i] = ((x) => x + num);
            }
            Array.Sort(toAdd);

            // Действие умножения (пусть будет одно)
            int toMult = rand.Next(2, 4);
            actions[numOfActions - 1] = ((x) => x * toMult);

            // Количество камней для выигрыша
            int toWin = rand.Next(25, 66);

            string[] table = new string[1];
            // Верхнее ограничение для выигрыша
            int upperBound = (toWin - toAdd.Max()) * toMult - rand.Next(8, 14);
            // Создаем таблицу для данных
            try
            {
                table = Tables.OneHeap(toAdd, toMult, toWin, upperBound);
            }
            catch (Exception e)
            {
                MessageBox.Show("Возникла ошибка при рассчитывании решения.\n" + e.Message);
                Environment.Exit(0);
            }

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
                    {
                        oneMoveWins.Add(i);
                    }
                }
            }
            oneMoveWins.Sort();

            // Сразу создадим строку для первого ответа
            string ans1a = $"{(int)Math.Ceiling((double)toWin / toMult)}-{upperBound / toMult}, " +
                string.Join(", ", oneMoveWins);

            // И список для вопроса 1б
            List<int> quest1bInts = new List<int>();
            // Добавляем в него все, что выигрывается не первым ходом из-за ограничения сверху
            for (int i = upperBound / toMult + 1; i < toWin - toAdd[toAdd.Length - 1]; i++)
            {
                quest1bInts.Add(i);
            }

            // Нужно оставить всего 3-4 штуки, остальные удаляем
            int toDelete = quest1bInts.Count - rand.Next(3, 5);
            for (int i = 0; i < toDelete; i++)
            {
                quest1bInts.RemoveAt(rand.Next(quest1bInts.Count));
            }
            // Делаем строкой
            string quest1b = string.Join(", ", quest1bInts);

            // Добавим в список выигрыши от умножения
            for (int i = (int)Math.Ceiling((double)toWin / toMult); i <= upperBound / toMult; i++)
            {
                oneMoveWins.Add(i);
            }

            /// Вопросы 2 и 3
            // Создаем переменные для первых и вторых плюсов и минусов
            string quest2Win, quest2Lose, quest3Win, quest3Lose;
            quest2Win = quest2Lose = quest3Win = quest3Lose = string.Empty;
            // Проходимся примерно с середины таблицы в начало
            for (int i = upperBound / toMult; i > 0; i--)
            {
                // Если клетка проигрыша, то проверям, первая или вторая, и записываем ее
                if (table[i] == "-")
                {
                    if (quest2Lose.Length == 0)
                        quest2Lose = i.ToString();
                    else if (quest2Lose.Length > 0 && quest2Win.Length > 0 && quest3Lose.Length == 0)
                        quest3Lose = i.ToString();
                }
                // Если клетка выигрыша, то тоже проверяем, а если это вторая выигрышная,
                // то выходим из цикла - данные нам больше не нужны
                if (table[i] == "+")
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

            // Строка с шаблоном задания
            string text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежит куча " +
                "камней. Игроки ходят по очереди, первый ход делает Петя. За один ход игрок может:<br>" +
                "- добавить в кучу любое допустимое количество камней: {0};<br>" +
                "- увеличить количество камней в куче в {1} раза.<br>" +
                "Игра завершается в тот момент, когда количество камней в куче становится не менее {2}. " +
                "Если при этом в куче оказалось<br>не более {3} камней, " +
                "то победителем считается игрок, сделавший последний ход. В противном случае победителем<br>" +
                "становится его противник. В начальный момент в куче было S камней, 1 ≤ S ≤ {4}.<br>" +
                "Задание 1.<br>" +
                "а) При каких значениях числа S Петя может выиграть в один ход? Укажите все такие " +
                "значения и соответствующие ходы Пети.<br>" +
                "б) У кого из игроков есть выигрышная стратегия при S = {5}? Опишите " +
                "выигрышные стратегии для этих случаев.<br>" +
                "Задание 2. У кого из игроков есть выигрышная стратегия при S = {6}? " +
                "Опишите соответствующие выигрышные стратегии.<br>" +
                "Задание 3. У кого из игроков есть выигрышная стратегия при S = {7}? Постройте дерево " +
                "всех партий, возможных при этой выигрышной стратегии (в виде рисунка или таблицы). " +
                "На рёбрах дерева указывайте, кто делает ход, в узлах – количество камней в позиции.";

            // Строка для форматирования шаблона
            string[] data = new string[] { string.Join(", ", toAdd), toMult.ToString(),
                toWin.ToString(), upperBound.ToString(), (toWin-1).ToString(), quest1b,
                string.Join(", ", new string[] { quest2Win, quest2Lose }), quest3 };

            // Создаем шаблон для ответа и записываем ответ для каждого пункта.
            string answer = "1a) {0}<br>1б) {1}<br>2) {2}<br>3) {3}<br>Развернутые ответы проверяются учителем.";

            string ans1b = string.Empty;

            for (int i = 0; i < quest1bInts.Count; i++)
            {
                ans1b += $"S = {quest1bInts[i]}: " + (table[quest1bInts[i]] == "+" ? "Петя<br>" : "Ваня<br>");
            }

            string ans2 = $"S = {quest2Win}: " + (table[int.Parse(quest2Win)] == "+" ? "Петя<br>" : "Ваня<br>") +
                $"S = {quest2Lose}: " + (table[int.Parse(quest2Lose)] == "+" ? "Петя<br>" : "Ваня<br>");

            string ans3 = $"S = {quest3}: " + (table[int.Parse(quest3)] == "+" ? "Петя<br>" : "Ваня<br>");

            // Строка для форматирования шаблона
            string[] answers = new string[] { ans1a, ans1b, ans2, ans3 };

            return new string[] { string.Format(text, data), string.Format(answer, answers) };
        }

        /// <summary>
        /// Генерирует задачу и ответ для типа "две кучи камней"
        /// </summary>
        /// <returns>Массив из двух строк - условие и ответ</returns>
        static string[] GenTwoHeaps()
        {
            // Генерация числовых значений
            int toAdd = rand.Next(1, 3);
            int toMult = rand.Next(2, 4);
            int toWin = rand.Next(35, 81);
            // Создание таблицы
            string[,] table = new string[1, 1];
            try
            {
                table = Tables.TwoHeaps(toAdd, toMult, toWin);
            }
            catch (Exception e)
            {
                MessageBox.Show("Возникла ошибка при рассчитывании решения.\n" + e.Message);
                Environment.Exit(0);
            }

            // Нашли все клетки с -1
            List<int[]> quest1array = new List<int[]>();
            for (int i = 1; i < toWin; i++)
            {
                for (int j = i; j < toWin; j++)
                {
                    if (table[i, j] == "-1") quest1array.Add(new int[] { i, j });
                }
            }

            // Теперь ищем клетки с +2 (в окрестности клетки -1 на две координаты назад)
            List<int[]> quest2array = new List<int[]>();
            foreach (var pair in quest1array)
            {
                for (int i = pair[0] - 2; i <= pair[0]; i++)
                {
                    for (int j = pair[1] - 2; j <= pair[1]; j++)
                    {
                        if (i > 0 && j > 0 && i <= j)
                        {
                            if (table[i, j] == "+2") quest2array.Add(new int[] { i, j }); ///////////
                        }
                    }
                }
            }

            // Ищем клетки -1/2 или -2 (в окрестностях +2 на одну координату назад)
            List<int[]> quest3array = new List<int[]>();
            foreach (var pair in quest2array)
            {
                for (int i = pair[0] - 1; i <= pair[0]; i++)
                {
                    for (int j = pair[1] - 1; j <= pair[1]; j++)
                    {
                        if (i > 0 && j > 0 && i <= j)
                        {
                            if (table[i, j] == "-") quest3array.Add(new int[] { i, j });
                        }
                    }
                }
            }

            // Выбрали рандомно две клетки -1, сделали строкой
            string[] quest1filtered = new string[2];
            int indToAdd;
            for (int i = 0; i < 2; i++)
            {
                indToAdd = rand.Next(quest1array.Count);
                quest1filtered[i] = $"({string.Join(", ", quest1array[indToAdd])})";
                quest1array.RemoveAt(indToAdd);
            }
            string quest1 = string.Join(", ", quest1filtered);

            // Выбрали рандомно две или три клетки +2, сделали строкой
            int len = rand.Next(2, 4);
            string[] quest2filtered = new string[len];
            for (int i = 0; i < len; i++)
            {
                indToAdd = rand.Next(quest2array.Count);
                quest2filtered[i] = $"({string.Join(", ", quest2array[indToAdd])})";
                quest2array.RemoveAt(indToAdd);
            }
            string quest2 = string.Join(", ", quest2filtered);

            // Сделали строкой координаты для третьего вопроса
            string quest3 = $"({string.Join(", ", quest3array[rand.Next(quest3array.Count)])})";

            // Строка с шаблоном задания
            string text = "Два игрока, Петя и Ваня, играют в следующую игру. Перед игроками лежат две кучи " +
                "камней. Игроки ходят по очереди, первый ход делает Петя. За один ход игрок может:<br> а) добавить " +
                "в одну из куч (по своему выбору) {0};<br> б) увеличить количество камней в куче в {1} " +
                "раза.<br>Победителем считается игрок, сделавший последний ход, т.е. первым получивший такую " +
                "позицию, что в обеих кучах всего будет {2} камней или больше.<br> Задание 1. Для каждой из " +
                "начальных позиций {3} укажите, кто из игроков имеет выигрышную стратегию.<br>" +
                "Задание 2. Для каждой из начальных позиций {4} укажите, кто из игроков " +
                "имеет выигрышную стратегию.<br> Задание 3. Для начальной позиции {5} укажите, кто из игроков " +
                "имеет выигрышную стратегию. Постройте дерево всех партий, возможных при указанной выигрышной " +
                "стратегии.";

            // Строка для форматирования шаблона
            string[] data = new string[] { toAdd.ToString() + (toAdd == 1 ? " камень" : " камня"),
                toMult.ToString(), toWin.ToString(), quest1, quest2, quest3 };

            // Строка с шаблоном ответа
            string answer = "1.<br>{0}<br>2.<br>{1}<br>3. {2}<br>Развернутые ответы проверяются учителем.";
            string ans1 = string.Empty;

            // Создаем ответы
            foreach (var pair in quest1filtered)
            {
                ans1 += $"{pair}: Ваня<br>";
            }

            string ans2 = string.Empty;

            foreach (var pair in quest2filtered)
            {
                ans2 += $"{pair}: Петя<br>";
            }

            string ans3 = quest3 + ": Ваня<br>";

            // Возвращаем отформатированные текст и решение
            return new string[] { string.Format(text, data), string.Format(answer, new string[] { ans1, ans2, ans3 }) };
        }

        /// <summary>
        /// Генерирует задачу и ответ для типа "два слова"
        /// </summary>
        /// <returns>Массив из двух строк - условие и ответ</returns>
        static string[] GenTwoWords()
        {
            // Генерируем данные для условия
            int[] toAdd = new int[] { rand.Next(1, 3), rand.Next(1, 3) };
            int[] toMult = new int[] { rand.Next(2, 4), rand.Next(2, 4) };
            Adder[] actionsX = new Adder[] { x => x + toAdd[0], x => x * toMult[0] };
            Adder[] actionsY = new Adder[] { x => x + toAdd[1], x => x * toMult[1] };
            int toWin = rand.Next(30, 61);

            // Создаем табличку для этих даннных
            string[,] table = new string[1, 1];
            try
            {
                table = Tables.TwoWords(actionsX, actionsY, toWin);
            }
            catch (Exception e)
            {
                MessageBox.Show("Возникла ошибка при рассчитывании решения.\n" + e.Message);
                Environment.Exit(0);
            }

            // Нашли все клетки с -1
            List<int[]> minus1array = new List<int[]>();
            for (int i = 1; i < toWin; i++)
            {
                for (int j = i; j < toWin; j++)
                {
                    if (table[i, j] == "-1") minus1array.Add(new int[] { i, j });
                }
            }

            // Клетки с +2 (в окрестности клетки -1 на две координаты назад)
            List<int[]> plus2array = new List<int[]>();
            foreach (var pair in minus1array)
            {
                for (int i = pair[0] - 2; i <= pair[0]; i++)
                {
                    for (int j = pair[1] - 2; j <= pair[1]; j++)
                    {
                        if (i > 0 && j > 0 && i <= j)
                        {
                            if (table[i, j] == "+2") plus2array.Add(new int[] { i, j });
                        }
                    }
                }
            }

            // Клетки с -1,2 (в окрестности клетки +2 на две координаты назад)
            List<int[]> minus12array = new List<int[]>();
            foreach (var pair in plus2array)
            {
                for (int i = pair[0] - 2; i <= pair[0]; i++)
                {
                    for (int j = pair[1] - 2; j <= pair[1]; j++)
                    {
                        if (i > 0 && j > 0 && i <= j)
                        {
                            if (table[i, j] == "-1,2") minus12array.Add(new int[] { i, j });
                        }
                    }
                }
            }

            // Клетки с + (в окрестностях -1,2 на две координаты назад)
            List<int[]> plusArray = new List<int[]>();
            foreach (var pair in minus12array)
            {
                for (int i = pair[0] - 2; i <= pair[0]; i++)
                {
                    for (int j = pair[1] - 2; j <= pair[1]; j++)
                    {
                        if (i > 0 && j > 0 && i <= j)
                        {
                            if (table[i, j] == "+") plusArray.Add(new int[] { i, j });
                        }
                    }
                }
            }

            // Список пар для первого вопроса
            List<string> quest1 = new List<string>();
            // Список ответов для первого вопроса
            List<string> ans1 = new List<string>();

            // Добавляем в него две -1
            int indToAdd;
            for (int i = 0; i < 2; i++)
            {
                indToAdd = rand.Next(minus1array.Count);
                quest1.Add($"({string.Join(", ", minus1array[indToAdd])})");
                ans1.Add($"({string.Join(", ", minus1array[indToAdd])}): Ваня");
                minus1array.RemoveAt(indToAdd);
            }

            // Добавляем один + в рандомное место
            indToAdd = rand.Next(plusArray.Count);
            // Позиция на которой будет эта пара
            int placeToAdd = rand.Next(quest1.Count + 1);
            quest1.Insert(placeToAdd, $"({string.Join(", ", plusArray[indToAdd])})");
            ans1.Insert(placeToAdd, $"({string.Join(", ", plusArray[indToAdd])}): Петя");
            plusArray.RemoveAt(indToAdd);

            // Список для второго вопроса
            List<string> quest2 = new List<string>();
            // Список ответов для второго вопроса
            List<string> ans2 = new List<string>();

            // Добавляем в него две +2
            for (int i = 0; i < 2; i++)
            {
                indToAdd = rand.Next(plus2array.Count);
                quest2.Add($"({string.Join(", ", plus2array[indToAdd])})");
                ans2.Add($"({string.Join(", ", plus2array[indToAdd])}): Петя");
                plus2array.RemoveAt(indToAdd);
            }

            // В рандомное место в нем одну или две -1/2
            for (int i = 0; i < rand.Next(1, 3); i++)
            {
                indToAdd = rand.Next(minus12array.Count);
                placeToAdd = rand.Next(quest2.Count + 1);
                quest2.Insert(placeToAdd, $"({string.Join(", ", minus12array[indToAdd])})");
                ans2.Insert(placeToAdd, $"({string.Join(", ", minus12array[indToAdd])}): Ваня");
                minus12array.RemoveAt(indToAdd);
            }

            // В третьем вопросе - рандом: либо +2, либо -1/2, либо +
            int indQuest3 = rand.Next(3);
            string quest3 = string.Empty;
            string ans3 = string.Empty;
            switch (indQuest3)
            {
                case 0:
                    {
                        indToAdd = rand.Next(plus2array.Count);
                        quest3 = $"({string.Join(", ", plus2array[indToAdd])})";
                        ans3 = "Петя<br>";
                        break;
                    }
                case 1:
                    {
                        indToAdd = rand.Next(minus12array.Count);
                        quest3 = $"({string.Join(", ", minus12array[indToAdd])})";
                        ans3 = "Ваня<br>";
                        break;
                    }
                case 2:
                    {
                        indToAdd = rand.Next(plusArray.Count);
                        quest3 = $"({string.Join(", ", plusArray[indToAdd])})";
                        ans3 = "Петя<br>";
                        break;
                    }
            }

            // Строка с шаблоном задания
            string text = "Два игрока, Петя и Ваня играют в игру с цепочками символов. Игра начинается " +
                "со слова, которое состоит из n букв Х и m букв Y. Такое слово будем обозначать как (n, m). " +
                "Игроки ходят по очереди, первый ход делает Петя. За один ход игрок может:<br>" +
                "1) добавить в слово {0} Х;<br>2) добавить в слово {1} Y;<br>3) увеличить " +
                "количество букв Х в {2} раза;<br>4) увеличить количество букв Y в {3} раза.<br>Игра " +
                "завершается в тот момент, когда длина слова становится не менее {4} символов. Победителем " +
                "считается игрок, сделавший последний ход, т.е. первым получивший слово длиной {4} или " +
                "больше.<br>Задание 1. Для каждой из начальных позиций {5} укажите, кто из игроков имеет " +
                "выигрышную стратегию.<br>Задание 2. Для каждой из начальных позиций {6} укажите, кто из " +
                "игроков имеет выигрышную стратегию.<br> Задание 3. Для начальной позиции {7} укажите, " +
                "кто из игроков имеет выигрышную стратегию. Постройте дерево всех партий, возможных при " +
                "указанной выигрышной стратегии.";

            // Данные для форматирования шаблона
            string[] data = new string[] { toAdd[0] == 1 ? "одну букву" : "две буквы",
                toAdd[1] == 1 ? "одну букву" : "две буквы", toMult[0].ToString(), toMult[1].ToString(),
                toWin.ToString(), string.Join(", ", quest1), string.Join(", ", quest2), quest3 };

            string answer = "1.<br>{0}<br><br>2.<br>{1}<br><br>3. {2}<br>Развернутые ответы проверяются учителем.";

            return new string[] { string.Format(text, data), string.Format(answer, new string[]
                { string.Join("<br>", ans1), string.Join("<br>", ans2), ans3 }) };
        }
    }
}
