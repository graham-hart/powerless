using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
namespace CSLib
{
    class Input
    {
        static Queue<KeyboardKey> keysPressed = new Queue<KeyboardKey>();
        static Queue<MouseButton> mouseButtonsPressed = new Queue<MouseButton>();
        public static KeyboardKey GetKeyPressed()
        {
            if (keysPressed.Count != 0)
                return (KeyboardKey)keysPressed.Dequeue();
            return (KeyboardKey)(-1);
        }
        public static void FlushKeys()
        {
            keysPressed.Clear();
        }
        public static void FlushMouse()
        {
            mouseButtonsPressed.Clear();
        }
        public static bool IsKeyPressed(KeyboardKey key)
        {
            return keysPressed.Contains(key);
        }
        public static bool IsMouseButtonPressed(MouseButton btn)
        {
            return mouseButtonsPressed.Contains(btn);
        }
        public static void LogInput()
        {
            KeyboardKey key = (KeyboardKey)Raylib.GetKeyPressed();
            while (key != 0)
            {
                keysPressed.Enqueue(key);
                key = (KeyboardKey)Raylib.GetKeyPressed();
            }
            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                mouseButtonsPressed.Enqueue(MouseButton.MOUSE_LEFT_BUTTON);
            if (IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
                mouseButtonsPressed.Enqueue(MouseButton.MOUSE_RIGHT_BUTTON);
            if (IsMouseButtonPressed(MouseButton.MOUSE_MIDDLE_BUTTON))
                mouseButtonsPressed.Enqueue(MouseButton.MOUSE_MIDDLE_BUTTON);

        }
        public static bool IsKeyDown(KeyboardKey key)
        {
            return Raylib.IsKeyDown(key);
        }
        public static bool IsKeyUp(KeyboardKey key)
        {
            return Raylib.IsKeyUp(key);
        }
        public static Vec2 GetMousePos()
        {
            return new Vec2(GetMousePosition());
        }
        public static bool IsMouseButtonDown(MouseButton btn)
        {
            return Raylib.IsMouseButtonDown(btn);
        }
        public static bool IsMouseButtonUp(MouseButton btn)
        {
            return Raylib.IsMouseButtonUp(btn);
        }
    }
}