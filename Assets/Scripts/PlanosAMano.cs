using CustomMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMatematic
{
    public struct Plano
    {
        public Plano(Vec3 inNormal, Vec3 inPoint)
        {
            
        }
        public Plano(Vec3 inNormal, float d)
        {

        }
        public Plano(Vec3 a, Vec3 b, Vec3 c)
        {

        }

        public Vec3 normal { get; set; }
        public float distance { get; set; }
        public Vec3 flipped { get; }


    }
}
