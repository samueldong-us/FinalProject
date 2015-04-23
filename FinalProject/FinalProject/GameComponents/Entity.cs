﻿using FinalProject.Messaging;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class Entity
    {
        public MessageCenter MessageCenter;
        public Vector2 Position;
        public float Rotation;
        public float Scale;
        private List<Component> components;
        private List<Component> toRemove;

        public Entity()
        {
            components = new List<Component>();
            toRemove = new List<Component>();
            MessageCenter = new MessageCenter();
            MessageCenter.AddListener("Clean Up", CleanUp);
            Position = Vector2.Zero;
            Scale = 1;
            Rotation = 0;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public void Dispose()
        {
            foreach (Component component in components)
            {
                component.Dispose();
            }
        }

        public void RemoveComponent(Component component)
        {
            toRemove.Add(component);
        }

        public Matrix ToScreenMatrix(Rectangle source)
        {
            return Matrix.CreateTranslation(-source.Width / 2, -source.Height / 2, 0) * Matrix.CreateScale(Scale) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateTranslation(Position.X, Position.Y, 0);
        }

        public void Update(float secondsPassed)
        {
            foreach (Component component in components)
            {
                component.Update(secondsPassed);
            }
        }

        private void CleanUp()
        {
            foreach (Component component in toRemove)
            {
                components.Remove(component);
                component.Dispose();
            }
            toRemove.Clear();
        }
    }
}