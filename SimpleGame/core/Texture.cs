using OpenTK.Graphics.OpenGL;

namespace SimpleGame.Core
{
    public class Texture : Resource
    {
        private int _texture;

        public Texture(string texturePath)
        {
            int width, height;

            var data = LoadTexture(texturePath, out width, out height);

            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.Float, data);
            
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                new int[] { (int)TextureMinFilter.LinearMipmapLinear });
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                new int[] { (int)TextureMinFilter.Linear });
        }

        private float[] LoadTexture(string filename, out int width, out int height)
        {
            float[] r;

            using (var bmp = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(filename))
            {
                width = bmp.Width;
                height = bmp.Height;
                r = new float[width * height * 4];
                int index = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = bmp.GetPixel(x, y);
                        r[index++] = pixel.R / 255f;
                        r[index++] = pixel.G / 255f;
                        r[index++] = pixel.B / 255f;
                        r[index++] = pixel.A / 255f;
                    }
                }
            }
            return r;
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, _texture);
        }

        protected override void FreeUnmanaged()
        {
            GL.DeleteTexture(_texture);
        }
    }
}