using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class ItemGroupMenu
    {
        protected int index;
        protected List<ItemMenu> items;

        public ItemGroupMenu()
        {
            items = new List<ItemMenu>();
            index = -1;
        }

        public void AddItem(ItemMenu item)
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
            foreach (ItemMenu item in items)
            {
                item.Draw(spriteBatch);
            }
        }

        public string GetSelected()
        {
            if (index == -1)
            {
                throw new Exception("MenuItemGroup Must Not Be Empty");
            }
            return items[index].Text;
        }

        public void MoveDown()
        {
            if (index == -1)
            {
                throw new Exception("MenuItemGroup Must Not Be Empty");
            }
            int startingIndex = index;
            do
            {
                index = NextIndex();
                if (index == startingIndex && items[index].Disabled)
                {
                    throw new Exception("At Least One Menu Item Must Be Enabled");
                }
            } while (items[index].Disabled);
            UpdateItems();
        }

        public void MoveUp()
        {
            if (index == -1)
            {
                throw new Exception("MenuItemGroup Must Not Be Empty");
            }
            int startingIndex = index;
            do
            {
                index = LastIndex();
                if (index == startingIndex && items[index].Disabled)
                {
                    throw new Exception("At Least One Menu Item Must Be Enabled");
                }
            } while (items[index].Disabled);
            UpdateItems();
        }

        public void Reset()
        {
            items.Clear();
            index = -1;
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