using static Raylib_cs.Raylib;
using Raylib_cs;
using System;
namespace CSLib
{
    class TextInput
    {
        public string text;
        public Rect rect;
        string placeholder;
        int maxLength;
        public int padding;
        int fontSize = 12;
        public bool selected = false;
        public void Initialize()
        {
            text = "";
        }
        public TextInput(Rect rect, string placeholder, int fontSize)
        {
            this.rect = rect;
            this.placeholder = placeholder;
            this.fontSize = fontSize;
            this.padding = (int)((rect.Height - fontSize) /2);
            Initialize();
        }
        public TextInput(Rect rect, string placeholder, int maxLength, int fontSize)
        {
            this.rect = rect;
            this.placeholder = placeholder;
            this.maxLength = maxLength;
            this.fontSize = fontSize;
            this.padding = (int)((rect.Height - fontSize) /2);
            Initialize();

        }
        public TextInput(Rect rect)
        {
            this.rect = rect;
            placeholder = "";
            Initialize();

        }
        public void Render()
        {
            DrawRectangleRoundedLines(rect.RaylibRect(), 0.3f, 4, 2, Color.RED);
            int x = (int)rect.X + padding;
            int y = (int)rect.Y + padding;
            if (text.Length > 0)
            {
                DrawText(text, x, y, fontSize, Color.WHITE);
            }
            else if (placeholder.Length > 0)
            {
                DrawText(placeholder, x, y, fontSize, new Color(200, 200, 200, 255));
            }
        }
        public void AppendCharCode(int keyCode)
        {
            text += (char)keyCode;
        }
        public void SetText(string txt)
        {
            text = txt;
        }
        public void Delete()
        {
            if(text.Length == 0) 
                return;
            text = text.Substring(0, text.Length - 1);
        }
        public void SendInput(int key)
        {
            if (key >= 32 && key <= 125)
            {
                AppendCharCode(key);
            }
            else if (key == 259)
            {
                Delete();
            }
        }
    }
}