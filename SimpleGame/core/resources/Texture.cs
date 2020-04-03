using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace SimpleGame.Core.Resources
{
    public class Texture : Resource
    {
        private int _texture;

        public Size Size { get; private set; }
        public int Width => this.Size.Width;
        public int Height => this.Size.Height;


        public Texture(string texturePath)
        {
            var data = LoadTexture(texturePath);

            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
            
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                new int[] { (int)TextureMinFilter.LinearMipmapLinear });
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                new int[] { (int)TextureMagFilter.Nearest });
        }

        private byte[] LoadTexture(string filename)
        {
            byte[] r;

            using (var bmp = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(filename))
            {
                this.Size = bmp.Size;
                
                r = new byte[Width * Height * 4];
                int index = 0;
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        var pixel = bmp.GetPixel(x, y);
                        r[index++] = pixel.R;
                        r[index++] = pixel.G;
                        r[index++] = pixel.B;
                        r[index++] = pixel.A;
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