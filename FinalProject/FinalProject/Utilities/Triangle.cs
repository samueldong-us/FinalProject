using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Utilities
{
    internal class Triangle
    {
        public Vector2 a, b, c;
        private Vector2 bVector, cVector;
        private float dotBB, dotBC, dotCC, denominator;

        public Triangle(Vector2 a, Vector2 b, Vector2 c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            bVector = b - a;
            cVector = c - a;
            dotBB = Vector2.Dot(bVector, bVector);
            dotBC = Vector2.Dot(bVector, cVector);
            dotCC = Vector2.Dot(cVector, cVector);
            denominator = dotBB * dotCC - dotBC * dotBC;
        }

        public bool Intersects(Triangle other)
        {
            return ContainsPoint(other.a) || ContainsPoint(other.b) || ContainsPoint(other.c) || other.ContainsPoint(a) || other.ContainsPoint(b) || other.ContainsPoint(c);
        }

        public Triangle Transform(Matrix matrix)
        {
            Vector2 newA = Vector2.Transform(a, matrix);
            Vector2 newB = Vector2.Transform(b, matrix);
            Vector2 newC = Vector2.Transform(c, matrix);
            return new Triangle(newA, newB, newC);
        }

        private bool ContainsPoint(Vector2 p)
        {
            Vector2 pVector = p - a;
            float dotPB = Vector2.Dot(pVector, bVector);
            float dotPC = Vector2.Dot(pVector, cVector);
            float u = (dotBB * dotPC - dotBC * dotPB) / denominator;
            float v = (dotCC * dotPB - dotBC * dotPC) / denominator;
            return u >= 0 && v >= 0 && u + v < 1;
        }
    }
}