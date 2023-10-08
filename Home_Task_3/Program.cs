using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8; // Устанавливаем кодировку UTF-8

        Dictionary<string, List<int>> highScores = new Dictionary<string, List<int>>
        {
            { "1-2", new List<int>() }, // Уровень 1, 1 подсказка
            { "2-3", new List<int>() }, // Уровень 2, 2 подсказки
            { "3-5", new List<int>() }  // Уровень 3, 4 подсказки
        };

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Играть");
            Console.WriteLine("2 - Посмотреть таблицу рекордов");
            Console.WriteLine("0 - Выход из игры");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Пожалуйста, введите число.");
                continue;
            }

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Выход из игры. До свидания!");
                    return;
                case 1:
                    Console.WriteLine("Выберите уровень сложности:");
                    Console.WriteLine("1 - От 0 до 10 (1 подсказка)");
                    Console.WriteLine("2 - От 0 до 100 (2 подсказки)");
                    Console.WriteLine("3 - От 0 до 1000 (4 подсказки)");

                    int levelChoice;
                    if (!int.TryParse(Console.ReadLine(), out levelChoice))
                    {
                        Console.WriteLine("Пожалуйста, введите число.");
                        continue;
                    }

                    int maxNumber;
                    int maxAttempts;

                    switch (levelChoice)
                    {
                        case 1:
                            maxNumber = 10;
                            maxAttempts = 2;
                            break;
                        case 2:
                            maxNumber = 100;
                            maxAttempts = 3;
                            break;
                        case 3:
                            maxNumber = 1000;
                            maxAttempts = 5;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор уровня сложности. Попробуйте еще раз.");
                            continue;
                    }

                    PlayGame(maxNumber, maxAttempts, highScores[$"{levelChoice}-{maxAttempts}"]);
                    break;
                case 2:
                    ShowHighScores(highScores);
                    break;
                default:
                    Console.WriteLine("Неверный выбор действия. Попробуйте еще раз.");
                    break;
            }
        }
    }

    static void PlayGame(int maxNumber, int maxAttempts, List<int> highScores)
    {
        Random random = new Random();
        int targetNumber = random.Next(maxNumber + 1);
        int attempts = 0;

        Console.WriteLine($"Угадайте число от 0 до {maxNumber}.");

        while (attempts < maxAttempts)
        {
            Console.Write($"Введите вашу догадку (от 0 до {maxNumber}): ");
            int guess;

            if (!int.TryParse(Console.ReadLine(), out guess) || guess < 0 || guess > maxNumber)
            {
                Console.WriteLine($"Пожалуйста, введите число от 0 до {maxNumber}.");
                continue;
            }

            attempts++;

            if (guess == targetNumber)
            {
                Console.WriteLine($"Поздравляю! Вы угадали число {targetNumber} за {attempts} попыток.");
                UpdateHighScores(highScores, attempts);
                break;
            }
            else if (guess < targetNumber)
            {
                Console.WriteLine("Загаданное число больше.");
            }
            else
            {
                Console.WriteLine("Загаданное число меньше.");
            }

            if (attempts < maxAttempts)
            {
                Console.WriteLine($"У вас осталось {maxAttempts - attempts} попыток.");
            }
            else
            {
                Console.WriteLine($"Вы исчерпали все попытки. Загаданное число было {targetNumber}.");
            }
        }
    }

    static void UpdateHighScores(List<int> highScores, int attempts)
    {
        if (highScores.Count == 0 || attempts < highScores[0])
        {
            Console.WriteLine("Поздравляю! Вы установили новый рекорд!");
            highScores.Insert(0, attempts);
        }
        else
        {
            Console.WriteLine($"Ваш результат: {attempts} попыток. Лучший результат: {highScores[0]} попыток.");
        }
    }

    static void ShowHighScores(Dictionary<string, List<int>> highScores)
    {
        Console.WriteLine("Таблица рекордов:");
        foreach (var kvp in highScores)
        {
            string[] parts = kvp.Key.Split('-');
            int level = int.Parse(parts[0]);
            int hints = int.Parse(parts[1]);
            Console.WriteLine($"Уровень {level}, Подсказки {hints}:");

            if (kvp.Value.Count == 0)
            {
                Console.WriteLine("Рекордов пока нет.");
            }
            else
            {
                Console.WriteLine($"Лучший результат: {kvp.Value[0]} попыток.");
            }
        }
    }
}
