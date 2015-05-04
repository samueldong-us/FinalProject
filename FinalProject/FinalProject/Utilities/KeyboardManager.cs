using FinalProject.Screens;
using Microsoft.Xna.Framework.Input;
using System;

namespace FinalProject.Utilities
{
    internal static class KeyboardManager
    {
        private static Keys[] lastPressedKeys;

        public static void BroadcastChanges(Screen receiver)
        {
            if (lastPressedKeys != null && receiver != null)
            {
                Keys[] currentPressedKeys = Keyboard.GetState().GetPressedKeys();
                foreach (Keys key in currentPressedKeys)
                {
                    if (!Array.Exists<Keys>(lastPressedKeys, element => element == key))
                    {
                        receiver.KeyPressed(key);
                    }
                }
                foreach (Keys key in lastPressedKeys)
                {
                    if (!Array.Exists<Keys>(currentPressedKeys, element => element == key))
                    {
                        receiver.KeyReleased(key);
                    }
                }
            }
            lastPressedKeys = Keyboard.GetState().GetPressedKeys();
        }

        public static Keys[] GetPressedKeys()
        {
            return lastPressedKeys;
        }
    }
}