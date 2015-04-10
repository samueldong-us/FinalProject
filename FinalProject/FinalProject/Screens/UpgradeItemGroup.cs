using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Screens
{
    internal class UpgradeItemGroup : MenuItemGroup
    {
        public Dictionary<string, int> GetLevels()
        {
            Dictionary<string, int> upgrades = new Dictionary<string, int>();
            foreach (MenuItem item in items)
            {
                UpgradeItem upgradeItem = (UpgradeItem)item;
                upgrades[upgradeItem.Text] = upgradeItem.level;
            }
            return upgrades;
        }

        public int GetSelectedCost()
        {
            return ((UpgradeItem)items[index]).GetCost();
        }

        public void UpgradeSelected()
        {
            ((UpgradeItem)items[index]).level++;
        }
    }
}