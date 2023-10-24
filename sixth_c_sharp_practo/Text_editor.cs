using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace sixth_c_sharp_practo
{
    public class TextEditor
    {
        string[] text;
        private int x;
        private int y;
        private string path;

        public TextEditor(string path)
        {
            text = File.ReadAllLines(path);
            x = 0;
            y = 1;
            this.path = path;
        }
        public void Display(string extension)
        {
            switch (extension)
            {
                case ".txt":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.SetCursorPosition(0, text.Length+1);
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Для выхода нажмите ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ESCAPE");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Для сохранения нажмите ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("F1");
                    Console.ResetColor();
                    Console.SetCursorPosition(0, 1);
                    foreach (string line in text)
                    {
                        Console.WriteLine(line);
                    }

                    Console.SetCursorPosition(x, y+1);
                    break;
                case ".json":

                    break;
                case ".xml":

                    break;
            }
        }

        public void MoveCursor(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (x > 0)
                        x--;
                    else if (y > 0)
                    {
                        x = text[y - 1].Length+1;
                        y--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (x < text[y].Length)
                        x++;
                    else if (y < text.Length - 1)
                    {
                        x = 0;
                        y++;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (y > 0)
                    {
                        if (x > text[y - 1].Length)
                            x = text[y - 1].Length;
                        y--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (y < text.Length - 1)
                    {
                        if (x > text[y + 1].Length)
                            x = text[y + 1].Length;
                        y++;
                    }
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }






        public void EditText(ConsoleKeyInfo keyInfo)
        {
            if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar) ||
                char.IsWhiteSpace(keyInfo.KeyChar))
            {
                text[y] = text[y].Insert(x, keyInfo.KeyChar.ToString());
                x++;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && x > 0)
            {
                text[y] = text[y].Remove(x - 1, 1);
                x--;
            }
        }
        public void EditJson(ConsoleKeyInfo keyInfo)
        {

            if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar) ||
                char.IsWhiteSpace(keyInfo.KeyChar))
            {
                text[y] = text[y].Insert(x, keyInfo.KeyChar.ToString());
                x++;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && x > 0)
            {
                text[y] = text[y].Remove(x - 1, 1);
                x--;
            }
        }
        public void EditXml(ConsoleKeyInfo keyInfo)
        {

            if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar) ||
                char.IsWhiteSpace(keyInfo.KeyChar))
            {
                text[y] = text[y].Insert(x, keyInfo.KeyChar.ToString());
                x++;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && x > 0)
            {
                text[y] = text[y].Remove(x - 1, 1);
                x--;
            }
        }

        public void SaveText(Figure figure)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Введите путь для сохранения (вместе с названием):");
            Console.WriteLine("-------------------------------------------------");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("-------------------------------------------------");
            Console.SetCursorPosition(0, 2);
            Console.ResetColor();
            string new_path = Console.ReadLine();
            File.WriteAllText(new_path, string.Empty);
            string extension = Path.GetExtension(new_path).ToLower();

            figure.Name = text[0];
            figure.Width = Convert.ToInt32(text[1]);
            figure.Height = Convert.ToInt32(text[2]);


            switch (extension)
            {
                case ".txt":
                    using (StreamWriter outputFile = new StreamWriter(new_path))
                    {
                        foreach (string line in text)
                            outputFile.WriteLine(line);
                    }
                    break;
                case ".json":
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        string json = JsonConvert.SerializeObject(figure);
                        File.WriteAllText(new_path, json);
                    }
                    break;
                case ".xml":
                    XmlSerializer serializer = new XmlSerializer(typeof(Figure));
                    using (FileStream fs = new FileStream(new_path, FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(fs, figure);
                    }
                    break;
            }
            
        }
    }
}
