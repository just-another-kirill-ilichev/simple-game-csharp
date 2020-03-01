using System.Linq;
using System.Collections.Generic;

using SimpleGame.Core;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Components.UI;

namespace SimpleGame.ECS.Systems{
    public class UISystem : SystemBase
    {
        private Dictionary<int, string> _textureCache;

        public UISystem(SDLApplication owner) : base(owner)
        {

        }

        public override void Redraw()
        {
            //var buttons = OwnerApp.EntityManager.Entities
            //    .WithComponent<ButtonUI>(OwnerApp.EntityManager)
            //    .ToArray();

            var texts = OwnerApp.EntityManager
                .WithComponent<TextUI>()
                .ToArray();

            
            foreach (var entity in texts)
            {
                var component = OwnerApp.EntityManager.GetComponent<TextUI>(entity);
                var position = OwnerApp.EntityManager.GetComponent<TransformComponent>(entity);


                var font = OwnerApp.ResourceManager.Get<Font>(component.FontResourceRef);

                using (var surf = font.Print(component.Text, component.TextColor))
                using (var texture = new Texture(OwnerApp, surf))
                {
                    texture.Draw((int)position.X, (int)position.Y); // TODO cache textures
                }
                
            }
        }
    }
}