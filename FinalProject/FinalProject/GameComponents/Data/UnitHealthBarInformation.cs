using Microsoft.Xna.Framework;

namespace FinalProject.GameComponents
{
    internal class UnitHealthBarInformation
    {
        public Rectangle BarRectangle { get; private set; }

        public Vector2 Offset { get; private set; }

        public UnitHealthBarInformation(Rectangle barRectangle, Vector2 offset)
        {
            BarRectangle = barRectangle;
            Offset = offset;
        }
    }
}