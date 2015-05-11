using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class SystemDrawing
    {
        private Dictionary<string, List<DrawableComponent>> drawableLayers;
        private string[] Layers = { "Enemy", "EnemyBullet", "Player", "PlayerBullet", "HealthBar", "Debug" };

        public SystemDrawing()
        {
            drawableLayers = new Dictionary<string, List<DrawableComponent>>();
            foreach (string layer in Layers)
            {
                drawableLayers[layer] = new List<DrawableComponent>();
            }
        }

        public void AddDrawable(string layer, DrawableComponent drawable)
        {
            drawableLayers[layer].Add(drawable);
        }

        public void Dispose()
        {
            foreach (string layer in drawableLayers.Keys)
            {
                drawableLayers[layer].Clear();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (DrawableComponent drawable in drawableLayers["PlayerBullet"])
            {
                drawable.Draw(spriteBatch);
            }
            foreach (DrawableComponent drawable in drawableLayers["Player"])
            {
                drawable.Draw(spriteBatch);
            }
            foreach (DrawableComponent drawable in drawableLayers["EnemyBullet"])
            {
                drawable.Draw(spriteBatch);
            }
            foreach (DrawableComponent drawable in drawableLayers["Enemy"])
            {
                drawable.Draw(spriteBatch);
            }
            foreach (DrawableComponent drawable in drawableLayers["HealthBar"])
            {
                drawable.Draw(spriteBatch);
            }
            foreach (DrawableComponent drawable in drawableLayers["Debug"])
            {
                //drawable.Draw(spriteBatch);
            }
        }

        public void RemoveDrawable(string layer, DrawableComponent drawable)
        {
            drawableLayers[layer].Remove(drawable);
        }
    }
}