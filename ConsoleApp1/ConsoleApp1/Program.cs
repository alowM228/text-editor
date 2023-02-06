using System;
using System.IO;
using Newtonsoft.Json;

namespace TextEditor
{
    class Program
    {
        static void Main()
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.Write("Что вы хотите сделать?\n 1-Открыть файл\n 2-Cоздать новый файл\n 0-Выйти\n ");
            byte option = byte.Parse(Console.ReadLine()!);

            switch (option)
            {
                case 0: ExitProgram(); break;
                case 1: OpenFile(); break;
                case 2: CreateNewFile(); break;
                case 3: CreateNewJsonFile(); break;
                default: Menu(); break;
            }
        }

        static void ExitProgram()
        {
            Console.WriteLine("Текстовый редактор закрыт.");
            System.Environment.Exit(0);
        }


        static void OpenFile()
        {
            Console.Clear();
            Console.WriteLine("Какой путь к файлу вы хотите открыть?");
            string path = Console.ReadLine()!;

            using (var file = new StreamReader(path))
            {
                string text = file.ReadToEnd();
                Console.WriteLine(text);
            }

            Console.WriteLine("Нажмите ENTER, чтобы вернуться в меню");
            Console.ReadLine();
            Menu();
        }

        static void CreateNewJsonFile()
        {
            Console.Clear();
            Console.WriteLine("Введите данные в формате JSON ниже (ESC для выхода)");
            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - ");
            string text = "";

            do
            {
                text += Console.ReadLine();
                text += Environment.NewLine;
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            SaveJSONFile(text);
        }


        static void CreateNewFile()
        {
            Console.Clear();
            Console.WriteLine("Введите текст ниже (ESC для выхода)");
            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - - - - ");
            string text = "";
            Console.WriteLine("Выберите тип файла: 1-TXT, 2-JSON");
            int fileType = int.Parse(Console.ReadLine()!);

            switch (fileType)
            {
                case 1:
                    do
                    {
                        text += Console.ReadLine();
                        text += Environment.NewLine;
                    }
                    while (Console.ReadKey().Key != ConsoleKey.Escape);

                    SaveFile(text);
                    break;
                case 2:
                    Console.WriteLine("Введите JSON объект: ");
                    string jsonText = Console.ReadLine()!;
                    SaveJSONFile(jsonText);
                    break;
                default:
                    Console.WriteLine("Неверный тип файла");
                    CreateNewFile();
                    break;
            }
        }

        static void SaveFile(string text)
        {
            Console.Clear();
            Console.WriteLine("Какой путь для сохранения файла?");
            var path = Console.ReadLine();

            using (var file = new StreamWriter(path!))
            {
                file.Write(text);
            }

            Console.WriteLine($"Файл {path} успешно сохранен!");
            Console.WriteLine("Нажмите ENTER, чтобы вернуться в меню");
            Console.ReadKey();
            Menu();
        }

       
        static void SaveJSONFile(string text)
        {
            Console.Clear();
            Console.WriteLine("Какой путь для сохранения JSON файла?");
            var path = Console.ReadLine();

            using (var file = new StreamWriter(path!))
            {
                file.Write(JsonConvert.SerializeObject(text));
            }

            Console.WriteLine($"JSON файл {path} успешно сохранен!");
            Console.WriteLine("Нажмите ENTER, чтобы вернуться в меню");
            Console.ReadKey();
            Menu();
        }
    }
}