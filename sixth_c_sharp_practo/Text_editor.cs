using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sixth_c_sharp_practo
{
    public class TextEditor
    {

        private int x;
        private int y;
        private string[] text;
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
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            for (var i = 0; i < Math.Min(consoleHeight, text.Length); i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine(text[i]);
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

        public void SaveText()
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (var line in text)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
