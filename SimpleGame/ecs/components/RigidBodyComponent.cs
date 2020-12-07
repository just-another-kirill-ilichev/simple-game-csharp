using Jitter.Collision.Shapes;
using Newtonsoft.Json;

namespace SimpleGame.ECS.Components
{
    public class RigidBodyComponent : Component
    {
        public string ShapeResourceRef { get; set; }

        public float Mass { get; set; } = 1.0f; // TODO
        public float Restitution { get; set; } = 0.2f; // TODO
        public float StaticFriction { get; set; } = 0.8f; // TODO
        public float KineticFriction { get; set; } = 0.8f; // TODO

        [JsonIgnore]
        public Jitter.Dynamics.RigidBody Body { get; set; }

        public RigidBodyComponent()
        {
            Body = new Jitter.Dynamics.RigidBody(new BoxShape(0.0f, 0.0f, 0.0f));
        }

        public override object Clone() =>
            new RigidBodyComponent()
            {
                Mass = Mass,
                Restitution = Restitution,
                StaticFriction = StaticFriction,
                KineticFriction = KineticFriction
            };
    }
}