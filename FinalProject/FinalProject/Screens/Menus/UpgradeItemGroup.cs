using System.Collections.Generic;

namespace FinalProject.Screens
{
    internal class ItemGroupUpgrade : ItemGroupMenu
    {
        public Dictionary<string, int> GetLevels()
        {
            Dictionary<string, int> upgrades = new Dictionary<string, int>();
            foreach (ItemMenu item in items)
            {
                ItemUpgrade upgradeItem = (ItemUpgrade)item;
                upgrades[upgradeItem.Text] = upgradeItem.level;
            }
            return upgrades;
        }

        public int GetSelectedCost()
        {
            return ((ItemUpgrade)items[index]).GetCost();
        }

        public void UpgradeSelected()
        {
            if (((ItemUpgrade)items[index]).level < 10)
            {
                ((ItemUpgrade)items[index]).level++;
            }
        }
    }
}