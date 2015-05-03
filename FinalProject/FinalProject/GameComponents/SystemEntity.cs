using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class SystemEntity
    {
        private List<Entity> entityList;
        private List<Entity> toRemove;

        public SystemEntity()
        {
            entityList = new List<Entity>();
            toRemove = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            entityList.Add(entity);
        }

        public void Dispose()
        {
            foreach (Entity entity in entityList)
            {
                entity.Dispose();
            }
            entityList.Clear();
            toRemove.Clear();
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
            toRemove.Clear();
        }
    }
}