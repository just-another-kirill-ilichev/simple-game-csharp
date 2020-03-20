using System;
using Jitter;
using Jitter.Collision;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;
using SimpleGame.Core;
using SimpleGame.ECS.Components;

namespace SimpleGame.ECS.Systems
{
    public class PhysicsSystem : SystemBase
    {
        private World _world;

        public PhysicsSystem(Application app) : base(app)
        {
            _world = new World(new CollisionSystemPersistentSAP());
            _world.Gravity = JVector.Zero;
            _world.AllowDeactivation = true;

            var entities = OwnerApp.EntityManager.WithComponent<RigidBodyComponent, TransformComponent>();

            foreach (var entity in entities)
            {
                //OwnerApp.EntityManager.GetComponent<RigidBodyComponent>(entity).Body = new RigidBody(new BoxShape(0.2f, 0.2f, 0.2f));
                var body = OwnerApp.EntityManager.GetComponent<RigidBodyComponent>(entity).Body;
                var transform = OwnerApp.EntityManager.GetComponent<TransformComponent>(entity);
                
                InitBody(ref body, transform, entity);

                OwnerApp.EntityManager.GetComponent<RigidBodyComponent>(entity).Body = body;
            }
        }

        public void InitBody(ref RigidBody body, TransformComponent transform, int entity)
        {
            body.Shape = new BoxShape(0.2f, 0.2f, 0.2f);
            body.Position = new JVector(transform.X, transform.Y, transform.Z);
            body.Orientation = JMatrix.CreateRotationX(transform.RotationX) *
                               JMatrix.CreateRotationY(transform.RotationY) *
                               JMatrix.CreateRotationZ(transform.RotationZ);

            body.SetMassProperties();
            body.Update();

            body.IsActive = true; // TODO
            body.IsStatic = !OwnerApp.EntityManager.HasComponent<MovableComponent>(entity);

            body.Mass = 1.0f; // TODO
            body.Material = new Material()
            {
                Restitution = 0.2f,
                StaticFriction = 0.8f,
                KineticFriction = 0.8f
            }; // TODO

            _world.AddBody(body);
        }


        public JVector RotationMatrixToEulerAngles(JMatrix orientation)
        {
            float sy = JMath.Sqrt(orientation.M11 * orientation.M11 + orientation.M21 * orientation.M21);
            bool singular = sy < 1e-6;

            float x, y, z;

            if (singular)
            {
                x = MathF.Atan2(orientation.M32, orientation.M33);
                y = MathF.Atan2(-orientation.M31, sy);
                z = MathF.Atan2(orientation.M21, orientation.M11);
            }
            else
            {
                x = MathF.Atan2(-orientation.M23, orientation.M22);
                y = MathF.Atan2(-orientation.M31, sy);
                z = 0;
            } 

            return new JVector(x, y, z);
        }

        public void UpdateTransform(int entity)
        {
            var body = OwnerApp.EntityManager.GetComponent<RigidBodyComponent>(entity).Body;
            var transform = OwnerApp.EntityManager.GetComponent<TransformComponent>(entity);

            if (body is null) // TODO
                return;

            transform.X = body.Position.X;
            transform.Y = body.Position.Y;
            transform.Z = body.Position.Z;

            JVector eulerAngles = RotationMatrixToEulerAngles(body.Orientation);

            transform.RotationX = eulerAngles.X;
            transform.RotationY = eulerAngles.Y;
            transform.RotationZ = eulerAngles.Z;
        }

        public override void Update(uint ms)
        {
            float step = ms / 1000.0f;

            _world.Step(1 / 60.0f, false);

            var entities = OwnerApp.EntityManager.WithComponent<RigidBodyComponent>();

            foreach (var entity in entities)
            {
                UpdateTransform(entity);
            }
        }
    }
}