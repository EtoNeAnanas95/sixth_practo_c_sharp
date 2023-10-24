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
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("---------------------------------------------------------------------");
                Console.ForegroundColor= ConsoleColor.White;
                Console.Write("Для выхода нажмите ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ESCAPE");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Для сохранения нажмите ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("F1");
                Console.ResetColor();
                Console.SetCursorPosition(0, 2);
                string path = Console.ReadLine();
                string extension = Path.GetExtension(path).ToLower();
                switch (extension)
                {
                    case ".txt":
                        TextEditor editor = new TextEditor(path);
                        ConsoleKeyInfo key;
                        do
                        {
                            editor.Display(extension);
                            key = Console.ReadKey();

                            switch (key.Key)
                            {
                                case ConsoleKey.Escape:
                                    Console.Clear();
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

                        } while (key.Key != ConsoleKey.Escape);
                        break;
                    case ".json":

                        break;
                    case ".xml":
                        break;
                }
            }
        }
    }
}