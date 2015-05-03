using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class UnitValues
    {
        public int Health { get; private set; }

        public UnitValues(int health)
        {
            Health = health;
        }
    }
}