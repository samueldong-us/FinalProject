﻿using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class MenuItemGroup
    {
        private int index;
        private List<MenuItem> items;

        public MenuItemGroup()
        {
            items = new List<MenuItem>();
            index = -1;
        }

        public void AddItem(MenuItem item)
        {
            items.Add(item);
            if (index == -1)
            {
                index = 0;
            }
            UpdateItems();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem item in items)
            {
                item.Draw(spriteBatch);
            }
        }

        public string GetSelected()
        {
            return items[index].Text;
        }

        public void MoveDown()
        {
            do
            {
                index = NextIndex();
            } while (items[index].Disabled);
            UpdateItems();
        }

        public void MoveUp()
        {
            do
            {
                index = LastIndex();
            } while (items[index].Disabled);
            UpdateItems();
        }

        private int LastIndex()
        {
            if (index > 0)
            {
                return index - 1;
            }
            else
            {
                return items.Count - 1;
            }
        }

        private int NextIndex()
        {
            return (index + 1) % items.Count;
        }

        private void UpdateItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Selected = i == index;
            }
        }
    }
}