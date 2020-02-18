using System.Drawing;

namespace SimpleGame.ECS.Components.UI
{
    public class ButtonUI : TextUI
    {
        public Color HoveredBackgroundColor { get; set; }
        public string HoveredBackgroundTextureResourceName { get; set; }
        public Color HoveredTextColor { get; set; }
        public Color PressedBackgroundColor { get; set; }
        public string PressedBackgroundTextureResourceName { get; set; }
        public Color PressedTextColor { get; set; }
    }
}