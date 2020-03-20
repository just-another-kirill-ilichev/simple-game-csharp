using Jitter.Collision.Shapes;
using Newtonsoft.Json;

namespace SimpleGame.ECS.Components
{
    public class RigidBodyComponent : Component
    {
        /*public string ShapeResourceRef { get; set; }
        public string MaterialResourceRef { get; set; }
*/
        [JsonIgnore]
        public Jitter.Dynamics.RigidBody Body { get; set; }

        public RigidBodyComponent()
        {
            Body = new Jitter.Dynamics.RigidBody(new BoxShape(0.0f, 0.0f, 0.0f));
        }
    }
}