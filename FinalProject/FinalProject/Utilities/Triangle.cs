using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.Utilities
{
    internal class Triangle
    {
        public Vector2 A, B, C;

        public Triangle(Vector2 A, Vector2 B, Vector2 C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public bool Intersects(Triangle other)
        {
            return ContainsPoint(other.A) || ContainsPoint(other.B) || ContainsPoint(other.C) || other.ContainsPoint(A) || other.ContainsPoint(B) || other.ContainsPoint(C);
        }

        public Triangle Transform(Matrix matrix)
        {
            Vector2 newA = Vector2.Transform(A, matrix);
            Vector2 newB = Vector2.Transform(B, matrix);
            Vector2 newC = Vector2.Transform(C, matrix);
            return new Triangle(newA, newB, newC);
        }

        private bool ContainsPoint(Vector2 p)
        {
            Vector2 bVector = B - A;
            Vector2 cVector = C - A;
            Vector2 pVector = p - A;
            float dotBB = Vector2.Dot(bVector, bVector);
            float dotBC = Vector2.Dot(bVector, cVector);
            float dotCC = Vector2.Dot(cVector, cVector);
            float dotPB = Vector2.Dot(pVector, bVector);
            float dotPC = Vector2.Dot(pVector, cVector);
            float denominator = dotBB * dotCC - dotBC * dotBC;
            float u = (dotBB * dotPC - dotBC * dotPB) / denominator;
            float v = (dotCC * dotPB - dotBC * dotPC) / denominator;
            return u >= 0 && v >= 0 && u + v < 1;
        }
    }
}