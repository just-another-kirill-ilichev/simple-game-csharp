using OpenTK;
using OpenTK.Graphics;
using SimpleGame.Core.Resources;

namespace SimpleGame.Core
{
    public class PrimitivesFactory
    {
        public static ColoredVertex[] CreateSolidCube(float side, Color4 color)
        {
            side = side / 2f; // half side - and other half
            ColoredVertex[] vertices =
            {
                new ColoredVertex(new Vector4(-side, -side, -side, 1.0f),   color),
                new ColoredVertex(new Vector4(-side, -side, side, 1.0f),    color),
                new ColoredVertex(new Vector4(-side, side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(-side, side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(-side, -side, side, 1.0f),    color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f),     color),

                new ColoredVertex(new Vector4(side, -side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, side, side, 1.0f),      color),

                new ColoredVertex(new Vector4(-side, -side, -side, 1.0f),   color),
                new ColoredVertex(new Vector4(side, -side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(-side, -side, side, 1.0f),    color),
                new ColoredVertex(new Vector4(-side, -side, side, 1.0f),    color),
                new ColoredVertex(new Vector4(side, -side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f),     color),

                new ColoredVertex(new Vector4(-side, side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f),     color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, side, side, 1.0f),      color),

                new ColoredVertex(new Vector4(-side, -side, -side, 1.0f),   color),
                new ColoredVertex(new Vector4(-side, side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(side, -side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(side, -side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(-side, side, -side, 1.0f),    color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f),     color),

                new ColoredVertex(new Vector4(-side, -side, side, 1.0f),    color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f),     color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f),     color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f),     color),
                new ColoredVertex(new Vector4(side, side, side, 1.0f),      color),
            };
            return vertices;
        }

        public static TexturedVertex[] CreateTexturedCube(float side, float textureWidth, float textureHeight)
        {
            float h = textureHeight;
            float w = textureWidth;
            side = side / 2f; // half side - and other half

            TexturedVertex[] vertices =
            {
                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(w, h)),

                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),

                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(0, 0)),

                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, h)),
            };
            return vertices;
        }
    }
}