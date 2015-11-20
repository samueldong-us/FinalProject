using FinalProject.Screens;
using FinalProject.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FinalProject.GameComponents
{
    internal class CatmullRomBehaviorComponent : DrawableComponent
    {
        private const float alpha = 1f;
        private const float deltaTime = .01f;
        private float parameter;
        private List<float> pathLength;
        private List<Vector2> pointList;
        private List<float> pointTimes;
        private float speed;
        private float startFiringPercentage;
        private float stopFiringPercentage;
        private float timePassed;
        private float timeToFinish;

        public CatmullRomBehaviorComponent(Entity entity, List<Vector2> path, float speed, float startFiringPercentage, float stopFiringPercentage)
            : base(entity, "Debug")
        {
            pointList = path;
            pathLength = new List<float>();
            pointTimes = new List<float>();
            float time = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                pointTimes.Add(time);
                pathLength.Add(ApproximateDistance(i));
                time += pathLength[i] / speed;
            }
            pointTimes.Add(time);
            timeToFinish = time;
            this.speed = speed;
            parameter = 0;
            this.startFiringPercentage = startFiringPercentage;
            this.stopFiringPercentage = stopFiringPercentage;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (float p = 0; p < timeToFinish; p += .1f)
            {
                Vector2 point = GetPointAt(p);
                spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)point.X - 2, (int)point.Y - 2, 4, 4), Color.Blue);
            }
            for (int index = 0; index < pointList.Count; index++)
            {
                Vector2 point = GetKeyPointAt(index);
                spriteBatch.Draw(GraphicsUtilities.PlainTexture, new Rectangle((int)point.X - 4, (int)point.Y - 4, 8, 8), Color.Green);
            }
        }

        public override void Update(float secondsPassed)
        {
            if (timePassed + secondsPassed >= timeToFinish)
            {
                GameScreen.Entities.RemoveEntity(entity);
                return;
            }
            else if (timePassed > stopFiringPercentage * timeToFinish)
            {
                entity.MessageCenter.Broadcast("Stop Firing");
            }
            else if (timePassed > startFiringPercentage * timeToFinish)
            {
                entity.MessageCenter.Broadcast("Start Firing");
            }
            entity.MessageCenter.Broadcast<Vector2>("Set Velocity", (GetPointAt(timePassed + secondsPassed) - GetPointAt(timePassed)) / secondsPassed);
            timePassed += secondsPassed;
        }

        private float ApproximateDistance(int index)
        {
            float distance = 0;
            Vector2 lastPoint = pointList[index];
            for (float p = deltaTime; p <= 1; p += deltaTime)
            {
                Vector2 currentPoint = CustomCatmullRom(GetKeyPointAt(index - 1), GetKeyPointAt(index), GetKeyPointAt(index + 1), GetKeyPointAt(index + 2), p);
                distance += Vector2.Distance(currentPoint, lastPoint);
                lastPoint = currentPoint;
            }
            return distance;
        }

        private float CalculateTimeMultiplier()
        {
            Vector2 currentPoint = GetPointAt(parameter);
            Vector2 sampledPoint = GetPointAt(parameter + deltaTime);
            return speed / (sampledPoint - currentPoint).Length() * deltaTime;
        }

        private Vector2 CustomCatmullRom(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float p)
        {
            float t0 = 0;
            float t1 = t0 + (float)Math.Pow(Vector2.Distance(p0, p1), alpha);
            float t2 = t1 + (float)Math.Pow(Vector2.Distance(p1, p2), alpha);
            float t3 = t2 + (float)Math.Pow(Vector2.Distance(p2, p3), alpha);
            float t = (p * (t2 - t1)) + t1;
            Vector2 a1 = (t1 - t) / (t1 - t0) * p0 + (t - t0) / (t1 - t0) * p1;
            Vector2 a2 = (t2 - t) / (t2 - t1) * p1 + (t - t1) / (t2 - t1) * p2;
            Vector2 a3 = (t3 - t) / (t3 - t2) * p2 + (t - t2) / (t3 - t2) * p3;
            Vector2 b1 = (t2 - t) / (t2 - t0) * a1 + (t - t0) / (t2 - t0) * a2;
            Vector2 b2 = (t3 - t) / (t3 - t1) * a2 + (t - t1) / (t3 - t1) * a3;
            return (t2 - t) / (t2 - t1) * b1 + (t - t1) / (t2 - t1) * b2;
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
            int firstIndex = 0;
            while (pointTimes[firstIndex + 1] < parameter)
            {
                firstIndex++;
            }
            float innerParameter = (parameter - pointTimes[firstIndex]) / (pointTimes[firstIndex + 1] - pointTimes[firstIndex]);
            return CustomCatmullRom(GetKeyPointAt(firstIndex - 1), GetKeyPointAt(firstIndex), GetKeyPointAt(firstIndex + 1), GetKeyPointAt(firstIndex + 2), innerParameter);
        }
    }
}