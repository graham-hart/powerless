using static Raylib_cs.Raylib;
using Raylib_cs;
using System;
namespace CSLib
{
    class Button
    {
        string text;
        public Rect rect;
        int textWidth;
        int fontSize;
        public Action OnClick;
        int padding;
        public Button(string text, Rect rect, int fontSize, Action onClick, int padding) {
            this.text = text;
            this.rect = rect;
            this.fontSize = fontSize;
            this.textWidth = MeasureText(text, fontSize);
            this.padding = padding;
            this.OnClick = onClick;

        }
        public void Render() {
            DrawRectangleRoundedLines(rect.RaylibRect(), 0.1f, 4, 1, Color.RED);
            DrawText(this.text, (int) this.rect.X+textWidth/2-padding, (int)this.rect.Y+padding, this.fontSize, Color.WHITE);
        }
        public static Button FitToText(string text, Vec2 pos, int fontSize, Action onClick, int padding) {
            Rect r = new Rect(pos.x, pos.y, MeasureText(text, fontSize)+padding*2, fontSize+padding*2);
            return new Button(text, r, fontSize, onClick, padding);
        }
    }
}