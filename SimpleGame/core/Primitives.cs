using System;
using System.Drawing;
using System.Linq;


namespace SimpleGame.Core
{/*
    public static class Primitives
    {
        public static void DrawLine(Window window, Color color, int x1, int y1, int x2, int y2)
        {
            SDL.SDL_SetRenderDrawColor(window.Renderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawLine(window.Renderer, x1, y1, x2, y2);
        }

        public static void DrawLines(Window window, Color color, SDL.SDL_FPoint[] points)
        {
            SDL.SDL_SetRenderDrawColor(window.Renderer, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderDrawLinesF(window.Renderer, points, points.Length);
        }

        public static void DrawIsometricCube(Window window, Color color, Point center, int edgeLength)
        {
            float temp = edgeLength * MathF.Sqrt(3) / 2;

            var cubePoints = new SDL.SDL_FPoint[]
            {
                new SDL.SDL_FPoint() { x = center.X, y = center.Y }, // center
                new SDL.SDL_FPoint() { x = center.X,  y = center.Y - edgeLength }, // top
                new SDL.SDL_FPoint() { x = center.X + temp, y = center.Y - edgeLength / 2 }, // top-right
                new SDL.SDL_FPoint() { x = center.X + temp, y = center.Y + edgeLength / 2 }, // bottom-right
                new SDL.SDL_FPoint() { x = center.X, y = center.Y + edgeLength}, // bottom
                new SDL.SDL_FPoint() { x = center.X - temp, y = center.Y - edgeLength / 2 }, // top-left
                new SDL.SDL_FPoint() { x = center.X - temp, y = center.Y + edgeLength / 2 }, // bottom-lfet
            };

            var upperPolygon = new SDL.SDL_FPoint[]
            {
                cubePoints[0], cubePoints[5], cubePoints[1], cubePoints[2], cubePoints[0]
            };

            var leftPolygon = new SDL.SDL_FPoint[]
            {
                cubePoints[0], cubePoints[5], cubePoints[6], cubePoints[4], cubePoints[0]
            };

            var rightPolygon = new SDL.SDL_FPoint[]
            {
                cubePoints[0], cubePoints[2], cubePoints[3], cubePoints[4], cubePoints[0]
            };

            DrawLines(window, color, upperPolygon);
            DrawLines(window, color, leftPolygon);
            DrawLines(window, color, rightPolygon);
        }
    }*/
}