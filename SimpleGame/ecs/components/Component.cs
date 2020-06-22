using System;
using Newtonsoft.Json;

namespace SimpleGame.ECS.Components
{
    public abstract class Component : ICloneable
    {
        public abstract object Clone();
    }
}