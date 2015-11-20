using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class PlayerControllerComponent : Component
    {
        private Dictionary<Keys, bool> isDown;
        private float speed;

        public PlayerControllerComponent(Entity entity, float speed)
            : base(entity)
        {
            isDown = new Dictionary<Keys, bool>();
            isDown[Keys.Left] = false;
            isDown[Keys.Right] = false;
            isDown[Keys.Up] = false;
            isDown[Keys.Down] = false;
            this.speed = speed;
            GameScreen.MessageCenter.AddListener<Keys>("Key Pressed", KeyPressed);
            GameScreen.MessageCenter.AddListener<Keys>("Key Released", KeyReleased);
        }

        public override void Dispose()
        {
            GameScreen.MessageCenter.RemoveListener<Keys>("Key Pressed", KeyPressed);
            GameScreen.MessageCenter.RemoveListener<Keys>("Key Released", KeyReleased);
            base.Dispose();
        }

        public override void Update(float secondsPassed)
        {
            Vector2 velocity = Vector2.Zero;
            if (isDown[Keys.Left])
            {
                velocity.X -= speed;
            }
            if (isDown[Keys.Right])
            {
                velocity.X += speed;
            }
            if (isDown[Keys.Up])
            {
                velocity.Y -= speed;
            }
            if (isDown[Keys.Down])
            {
                velocity.Y += speed;
            }
            entity.MessageCenter.Broadcast<Vector2>("Set Velocity", velocity);
        }

        private void KeyPressed(Keys parameterOne)
        {
            isDown[parameterOne] = true;
            if (parameterOne == Keys.Space)
            {
                entity.MessageCenter.Broadcast("Start Firing");
            }
        }

        private void KeyReleased(Keys parameterOne)
        {
            isDown[parameterOne] = false;
            if (parameterOne == Keys.Space)
            {
                entity.MessageCenter.Broadcast("Stop Firing");
            }
        }
    }
}