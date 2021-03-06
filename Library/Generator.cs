﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // Класс, запускающий генерацию
    public static class Generator
    {
        static Random rand;

        // Делегат для методов генерации задач
        delegate string[] GenerateType();
        // Ключ генерации
        public static int Seed { get; set; }
        // Тип задачи
        public static int ProblemsType { get; set; }
        // Количество задач
        public static int ProblemsNum { get; set; }

        /// <summary>
        /// Генерирует заданное пользователем количество задач заданного типа
        /// </summary>
        /// <returns>Двумерный массив - на первой строке условия, на второй - ответы</returns>
        public static string[,] Generate()
        {
            // Проверяем, был ли указан ключ генерации
            if (Seed == 0) rand = new Random();
            else rand = new Random(Seed);
            Problems.Rand = rand;

            GenerateType GenerateMethod;
            if (ProblemsType == 0) GenerateMethod = Problems.GenOneHeap;
            else if (ProblemsType == 1) GenerateMethod = Problems.GenTwoHeaps;
            else GenerateMethod = Problems.GenTwoWords;

            string[,] problems = new string[3, ProblemsNum];
            string[] result = new string[1];
            for (int i = 0; i < ProblemsNum; i++)
            {
                try
                {
                    result = GenerateMethod();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Возникла ошибка при генерации задачи.\n" +
                        "Приложение принудительно завершит работу. " + e.Message);
                    Environment.Exit(0);
                }
                problems[0, i] = $"Задача {i + 1}<br>" + result[0];
                problems[1, i] = result[1];
                problems[2, i] = result[2];
            }
            return problems;
        }

        /// <summary>
        /// Генерирует заданное пользователем количество задач разных типов, выбирая их рандомно
        /// </summary>
        /// <returns>Двумерный массив - на первой строке условия, на второй - ответы</returns>
        public static string[,] RandomGenerate()
        {
            // Проверяем, был ли указан ключ генерации
            if (Seed == 0) rand = new Random();
            else rand = new Random(Seed);

            // Передаем созданный объект рандома в класс генерации задач
            Problems.Rand = rand;

            string[,] problems = new string[3, ProblemsNum];
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
                                result = Problems.GenOneHeap();
                                break;
                            }
                        case 1:
                            {
                                result = Problems.GenTwoHeaps();
                                break;
                            }
                        case 2:
                            {
                                result = Problems.GenTwoWords();
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
                    MessageBox.Show("Возникла ошибка при генерации задачи.\n" +
                        "Приложение принудительно завершит работу. " + e.Message);
                    Environment.Exit(0);
                }
                problems[0, i] = $"Задача {i + 1}<br>" + result[0];
                problems[1, i] = result[1];
                problems[2, i] = result[2];
            }
            return problems;
        }

    }
}
