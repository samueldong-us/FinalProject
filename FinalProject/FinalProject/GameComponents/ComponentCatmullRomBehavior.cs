using FinalProject.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameComponents
{
    internal class ComponentCatmullRomBehavior : Component
    {
        private const float StartFiringPoint = 20;
        private const float StopFiringPoint = 80;
        private float parameter;
        private List<Vector2> pointList;
        private float timeToComplete;

        public ComponentCatmullRomBehavior(Entity entity, List<Vector2> path, float timeToComplete)
            : base(entity)
        {
            pointList = path;
            this.timeToComplete = timeToComplete;
            parameter = 0;
        }

        public override void Dispose()
        {
        }

        public override void Update(float secondsPassed)
        {
            float oldParameter = parameter;
            parameter += secondsPassed / timeToComplete * 100;
            if (parameter >= 100)
            {
                ScreenGame.MessageCenter.Broadcast<Entity>("Remove Entity", entity);
            }
            else if (parameter > StopFiringPoint)
            {
                entity.MessageCenter.Broadcast("Stop Firing");
            }
            else if (parameter > StartFiringPoint)
            {
                entity.MessageCenter.Broadcast("Start Firing");
            }
            entity.MessageCenter.Broadcast<Vector2>("Set Velocity", GetPointAt(parameter) - GetPointAt(oldParameter));
        }

        private Vector2 GetKeyPointAt(int index)
        {
            if (index < 0)
            {
                return pointList[0] - new Vector2(50, 0);
            }
            else if (index < pointList.Count)
            {
                return pointList[index];
            }
            else
            {
                return pointList[pointList.Count - 1] + new Vector2(50, 0);
            }
        }

        private Vector2 GetPointAt(float parameter)
        {
            int firstIndex = (int)(parameter / 100 * (pointList.Count - 1));
            float innerParameter = (parameter - firstIndex * 100 / (pointList.Count - 1)) / 100;
            return Vector2.CatmullRom(GetKeyPointAt(firstIndex - 1), GetKeyPointAt(firstIndex), GetKeyPointAt(firstIndex + 1), GetKeyPointAt(firstIndex + 2), innerParameter);
        }
    }
}