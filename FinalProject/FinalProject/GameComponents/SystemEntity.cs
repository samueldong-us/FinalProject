using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class SystemEntity
    {
        private List<Entity> entityList;

        private List<Entity> toAdd;
        private List<Entity> toRemove;

        public SystemEntity()
        {
            entityList = new List<Entity>();
            toRemove = new List<Entity>();
            toAdd = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            toAdd.Add(entity);
        }

        public void Dispose()
        {
            foreach (Entity entity in entityList)
            {
                entity.Dispose();
            }
            entityList.Clear();
            toRemove.Clear();
            foreach (Entity entity in toAdd)
            {
                entity.Dispose();
            }
            toAdd.Clear();
        }

        public void RemoveEntity(Entity entity)
        {
            toRemove.Add(entity);
        }

        public void Update(float secondsPassed)
        {
            foreach (Entity entity in entityList)
            {
                entity.Update(secondsPassed);
            }
            foreach (Entity entity in toRemove)
            {
                entityList.Remove(entity);
                entity.Dispose();
            }
            foreach (Entity entity in toAdd)
            {
                entityList.Add(entity);
            }
            toAdd.Clear();
            toRemove.Clear();
        }
    }
}