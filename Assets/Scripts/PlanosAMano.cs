using CustomMath;
using System.Collections;
using System.Collections.Generic;
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
    }
}
