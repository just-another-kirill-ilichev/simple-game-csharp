using System.Drawing;

namespace SimpleGame.ECS.Components.UI
{
    public class TextUI : Component
    {
        public string Text { get; set; }
        public string FontResourceRef { get; set; }
        public Color BackgroundColor { get; set; }
        public string BackgroundTextureResourceRef { get; set; }
        public Color TextColor { get; set; }
    }
}