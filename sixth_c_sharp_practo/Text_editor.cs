using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            y = 0;
            this.path = path;
        }
        public void Display()
        {
            Console.Clear();
            foreach (string line in text)
            {
                Console.WriteLine(line);
            }

            Console.SetCursorPosition(x, y);
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
                        x = text[y-1].Length;
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
                        x = text[y - 1].Length;
                        y--;
                    break;
                case ConsoleKey.DownArrow:
                    if (y <= text.Length)
                        x = 0;
                        y++;
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

        public void SaveText(Figure figure)
        {
            Console.Clear();
            Console.WriteLine("Введите путь для сохранения:");
            string new_path = Console.ReadLine();
            File.WriteAllText(new_path, string.Empty);
            using (var writer = new FileStream(path, FileMode.Truncate))
            using (var streamWriter = new StreamWriter(writer))
            {
                string extension = Path.GetExtension(path).ToLower();

                figure.Name = text[0];
                figure.Width = Convert.ToInt32(text[1]);
                figure.Height = Convert.ToInt32(text[2]);


                switch (extension)
                {
                    case ".txt":
                        streamWriter.WriteLine(figure.Name);
                        streamWriter.WriteLine(figure.Width);
                        streamWriter.WriteLine(figure.Height);
                        
                        break;
                    case ".json":
                        string json = JsonConvert.SerializeObject(figure);
                        File.WriteAllText(new_path, json);
                        break;
                    case ".xml":

                        break;
                }
            }
        }
    }
}
