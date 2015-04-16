using FinalProject.Messaging;
using FinalProject.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameResources
{
    internal class PlayerInputComponent : Component
    {
        private const float Speed = 20;
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
        }

        public void KeyReleased(Keys key)
        {
            if (arrowKeys.ContainsKey(key))
            {
                arrowKeys[key] = false;
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