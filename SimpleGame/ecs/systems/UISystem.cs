using System.Linq;
using System.Collections.Generic;

using SimpleGame.Core;
using SimpleGame.ECS.Components;
using SimpleGame.ECS.Components.UI;

namespace SimpleGame.ECS.Systems
{
    public class UISystem : SystemBase
    {
        private Dictionary<int, string> _renderedFontTextureCache;

        public UISystem(SDLApplication owner) : base(owner)
        {
            _renderedFontTextureCache = new Dictionary<int, string>();
        }

        public void UpdateRenderedFontTextureCache(int entity)
        {
            var component = OwnerApp.EntityManager.GetComponent<TextUI>(entity);
            var position = OwnerApp.EntityManager.GetComponent<TransformComponent>(entity);
            var font = OwnerApp.ResourceManager.Get<Font>(component.FontResourceRef);

            using (var surf = font.Print(component.Text, component.TextColor))
            {
                var texture = new Texture(OwnerApp, surf);

                if (_renderedFontTextureCache.ContainsKey(entity))
                {
                    var res = OwnerApp.ResourceManager.Get<Texture>(_renderedFontTextureCache[entity]);
                    res.Dispose();
                    res = texture;
                }
                else
                {
                    _renderedFontTextureCache[entity] = "textureCache" + entity.ToString();
                    OwnerApp.ResourceManager.Add(_renderedFontTextureCache[entity], texture);
                }
            }
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

                if (_renderedFontTextureCache.ContainsKey(entity))
                {
                    OwnerApp.ResourceManager.Get<Texture>(_renderedFontTextureCache[entity]).Draw((int)position.X, (int)position.Y);
                }
                else
                {
                    UpdateRenderedFontTextureCache(entity);
                }
            }
        }
    }
}