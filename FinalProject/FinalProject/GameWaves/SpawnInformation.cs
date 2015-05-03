using System;
using System.Collections.Generic;

namespace FinalProject.GameWaves
{
    internal class SpawnInformation
    {
        private Dictionary<string, object> Information;

        public float SpawnTime { get; private set; }

        public SpawnInformation(float spawnTime)
        {
            SpawnTime = spawnTime;
            Information = new Dictionary<string, object>();
        }

        public void AddInformation(string name, object information)
        {
            Information[name] = information;
        }

        public T GetInformation<T>(string name)
        {
            if (Information[name] is T)
            {
                return (T)Information[name];
            }
            else
            {
                throw new Exception("Information \"" + name + "\" Type Mismatch");
            }
        }
    }
}