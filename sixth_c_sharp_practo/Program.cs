using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace sixth_c_sharp_practo
{
    internal class Program
    {
        static void move_cursor(int y, int x, ConsoleKeyInfo key, string[] text) 
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (x > 0)
                        x--;
                    break;
                case ConsoleKey.RightArrow:
                    if (x < text[y].Length)
                        x++;
                    break;
                case ConsoleKey.UpArrow:
                    if (y > 0)
                        y--;
                    break;
                case ConsoleKey.DownArrow:
                    if (y < text.Length - 1)
                        y++;
                    break;
            }
        }
        public static void Main(string[] args)
        {
            Figure figure = new Figure();

            string name = "Квадрат";
            int width = 10;
            int height = 10;

            figure.Name = name;
            figure.Height = height;
            figure.Width = width;
            

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Введите путь до файла (вместе с названием), который вы хотите открыть");
                Console.WriteLine("---------------------------------------------------------------------");
                Console.ResetColor();
                string path = Console.ReadLine();
                    //"C:\\Users\\1\\Desktop\\text.txt"
                string extension = Path.GetExtension(path).ToLower();
                switch (extension)
                {
                    case ".txt":
                        TextEditor editor = new TextEditor(path);
                        do
                        {
                            editor.Display();
                            ConsoleKeyInfo key = Console.ReadKey();

                            switch (key.Key)
                            {
                                case ConsoleKey.Escape:
                                    Environment.Exit(0);
                                    break;
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.RightArrow:
                                case ConsoleKey.UpArrow:
                                case ConsoleKey.DownArrow:
                                    editor.MoveCursor(key);
                                    break;
                                case ConsoleKey.F1:
                                    editor.SaveText(figure);
                                    break;
                                case ConsoleKey.Enter:
                                    break;
                                default:
                                    editor.EditText(key);
                                    break;
                            }

                            editor.MoveCursor(key);

                        } while (true);
                        break;
                        /*var editor = new TextEditor(path);
                        do
                        {
                            editor.Display();
                            key = Console.ReadKey();
                            switch (key.Key)
                            {
                                case ConsoleKey.Escape:
                                    Environment.Exit(0);
                                    break;
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.RightArrow:
                                case ConsoleKey.UpArrow:
                                case ConsoleKey.DownArrow:
                                    editor.MoveCursor(key);
                                    break;
                                case ConsoleKey.F1:
                                    editor.SaveText(figure);
                                    break;
                                case ConsoleKey.Enter:
                                    break;
                                default:
                                    editor.EditText(key);
                                    break;
                            }*/
                    case ".json":

                        break;
                    case ".xml":
                        break;
                }
            }
        }
    }
}