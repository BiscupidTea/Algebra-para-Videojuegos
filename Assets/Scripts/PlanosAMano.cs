using CustomMath;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace CustomMatematic
{
    public struct Plano
    {
        public Vec3 normal;
        public Vec3 vertA;
        public Vec3 vertB;
        public Vec3 vertC;

        public float distance;
        public Plano flipped => new(-normal, -distance);

        #region Constructors

        public Plano(Vec3 inNormal, Vec3 inPoint)
        {
            this.normal = Vec3.Cross(inNormal, inPoint);
            this.distance = 0 + Vec3.Dot(inNormal, inPoint);
            vertA = inPoint;
            vertB = inPoint;
            vertC = inPoint;

        }
        public Plano(Vec3 inNormal, float d)
        {
            this.normal = inNormal;
            this.distance = d;
            vertA = normal;
            vertB = normal;
            vertC = normal;
        }
        public Plano(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = Vec3.Cross(b - a, c - a).normalized;
            this.distance = -Vec3.Dot(this.normal, a);
            vertA = a;
            vertB = b;
            vertC = c;
        }
        #endregion

        #region Operators

        public static bool operator ==(Plano left, Plano right)
        {
            return left.normal == right.normal && left.distance == right.distance;
        }

        public static bool operator !=(Plano left, Plano right)
        {
            return !(left == right);
        }
        public static Plano Zero { get { return new Plano(Vec3.Zero, 0); } }

        #endregion

        #region Functions

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            var pointPlaneDistance = Vec3.Dot(point, normal) + distance;
            return point - (normal * pointPlaneDistance);
        }

        public void Flip()
        {
            this.normal = -normal;
            this.distance = -distance;
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(point, normal) + distance;
        }

        public bool GetSide(Vec3 point)
        {
            if (Vec3.Dot(normal, point) + distance > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SameSide(Vec3 point1, Vec3 point2)
        {
            float distance1 = GetDistanceToPoint(point1);
            float distance2 = GetDistanceToPoint(point2);

            return (distance1 > 0.0f && distance2 > 0.0f) || (distance1 <= 0.0f && distance2 <= 0.0f);
        }

        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = Vec3.Cross(b - a, c - a).normalized;
            this.distance = -Vec3.Dot(this.normal, a);
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 point)
        {
            this.normal = Vec3.Cross(inNormal, point);
            this.distance = 0f + Vec3.Dot(inNormal, point);
        }

        public void Translate(Vec3 translate)
        {
            distance += Vec3.Dot(normal, translate);
        }

        #endregion
    }
}
