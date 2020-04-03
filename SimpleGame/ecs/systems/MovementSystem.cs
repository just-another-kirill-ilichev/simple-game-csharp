using System.Linq;
using Jitter.LinearMath;
using OpenTK.Input;
using SimpleGame.Core;
using SimpleGame.ECS.Components;

namespace SimpleGame.ECS.Systems
{
    public class MovementSystem : SystemBase
    {
        public MovementSystem(Application app) : base(app)
        {
        }

        public JVector CalcVelocity(float speed)
        {
            var state = Keyboard.GetState(); // Does not implemented in .Net core version of OpenTK
            var direction = new JVector();

            if (state.IsKeyDown(Key.W) || state.IsKeyDown(Key.Up))
                direction.Z -= 1;

            if (state.IsKeyDown(Key.S) || state.IsKeyDown(Key.Down))
                direction.Z += 1;

            if (state.IsKeyDown(Key.A) || state.IsKeyDown(Key.Left))
                direction.X -= 1;

            if (state.IsKeyDown(Key.D) || state.IsKeyDown(Key.Right))
                direction.X += 1;

            if (direction != JVector.Zero)
                return JVector.Normalize(direction) * speed;
            else
                return direction;
        }

        public override void Update(uint deltaMs)
        {
            var entities = OwnerApp.EntityManager.WithComponent<MovableComponent>().ToArray();

            foreach (var entity in entities)
            {
                var ridgidBodyComponent = OwnerApp.EntityManager.GetComponent<RigidBodyComponent>(entity);
                var movableComponent = OwnerApp.EntityManager.GetComponent<MovableComponent>(entity);

                var velocity = JVector.Multiply(CalcVelocity(movableComponent.Speed), deltaMs / 16.6f);

                if (velocity != JVector.Zero && !(ridgidBodyComponent.Body is null))
                {
                    ridgidBodyComponent.Body.ApplyImpulse(velocity);
                }

                if (Keyboard.GetState().IsKeyDown(Key.Space)) // TODO && !ridgidBodyComponent.Body.IsActive)
                {
                    ridgidBodyComponent.Body.ApplyImpulse(new JVector(0.0f, movableComponent.JumpSpeed, 0.0f));
                }
            }
        }
    }
}