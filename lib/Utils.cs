using Raylib_cs;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
namespace CSLib
{
    static class Utils
    {
        public static bool IsLetter(char c)
        {
            int code = (int)c;
            return (code >= 65 && code <= 90) || (code >= 92 && code <= 122);
        }
        public static bool SendInput(TextInput input)
        {
            bool beenPressed = false;
            int key = GetKeyPressed();
            while (key != 0)
            {
                if (IsLetter((char)key) && !IsKeyDown(KEY_LEFT_SHIFT) && !IsKeyDown(KEY_RIGHT_SHIFT))
                {
                    input.SendInput(key + 32);
                }
                else
                {
                    input.SendInput(key);
                }
                key = GetCharPressed();
                beenPressed = true;
            }
            return beenPressed;
        }
        public static void SortTMLayers(TileMap tm, int front)
        {
            tm.layers.Sort();
            if (tm.layers.Contains(front))
            {
                tm.layers.Remove(front);
                tm.layers.Add(front);
            }
        }
        public static void SortTMLayers(TileMap tm)
        {
            tm.layers.Sort();
        }
        public static Vec2 WorldMouseCoords(Camera cam)
        {
            return new Vec2(cam.UnprojectX(GetMouseX()), cam.UnprojectY(GetMouseY()));
        }
        public static double Lerp(double start, double end, double amount)
        {
            return start + (end - start) * amount;
        }
        public static void CameraLerp(Camera cam, Vec2 pos, double amt, double maxDist)
        {
            cam.SetPos(Vec2.Lerp(cam.Position, pos, amt));
            if(cam.pos.DistanceTo(pos) > maxDist) {
                cam.LimitDist(pos, maxDist);
            }
        }
    }
}