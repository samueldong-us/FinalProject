using FinalProject.Messaging;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class PlayerInputComponent : Component
    {
        private const float Speed = 300;
        private Dictionary<Keys, bool> arrowKeys;

        public PlayerInputComponent(MessageCenter entityMessageCenter)
            : base(entityMessageCenter)
        {
            arrowKeys = new Dictionary<Keys, bool>();
            arrowKeys[Keys.Left] = false;
            arrowKeys[Keys.Right] = false;
            arrowKeys[Keys.Up] = false;
            arrowKeys[Keys.Down] = false;
            GameScreen.GameMessageCenter.AddListener<Keys>("Key Pressed", KeyPressed);
            GameScreen.GameMessageCenter.AddListener<Keys>("Key Released", KeyReleased);
        }

        public override void Dispose()
        {
            GameScreen.GameMessageCenter.RemoveListener<Keys>("Key Pressed", KeyPressed);
            GameScreen.GameMessageCenter.RemoveListener<Keys>("Key Released", KeyReleased);
        }

        public void KeyPressed(Keys key)
        {
            if (arrowKeys.ContainsKey(key))
            {
                arrowKeys[key] = true;
            }
            if (key == Keys.Space)
            {
                messageCenter.Broadcast("Start Shooting");
            }
        }

        public void KeyReleased(Keys key)
        {
            if (arrowKeys.ContainsKey(key))
            {
                arrowKeys[key] = false;
            }
            if (key == Keys.Space)
            {
                messageCenter.Broadcast("Stop Shooting");
            }
        }

        public override void Update(float secondsPassed)
        {
            float xSpeed = 0;
            float ySpeed = 0;
            if (arrowKeys[Keys.Left])
            {
                xSpeed -= Speed;
            }
            if (arrowKeys[Keys.Right])
            {
                xSpeed += Speed;
            }
            if (arrowKeys[Keys.Up])
            {
                ySpeed -= Speed;
            }
            if (arrowKeys[Keys.Down])
            {
                ySpeed += Speed;
            }
            messageCenter.Broadcast<Vector2>("Set Velocity", new Vector2(xSpeed, ySpeed));
        }
    }
}