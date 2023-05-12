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

        public float distance;
        public Plano flipped => new(-normal, -distance);

        public Vec3 a;
        public Vec3 b;
        public Vec3 c;

        #region Constructors

        /// <summary>
        /// Crea un plano en base a una normal y un punto.
        /// </summary>
        /// <param name="inNormal"></param>
        /// <param name="inPoint"></param>
        public Plano(Vec3 inNormal, Vec3 inPoint)
        {
            this.normal = Vec3.Cross(inNormal, inPoint);
            this.distance = 0 + Vec3.Dot(inNormal, inPoint);
            a = inNormal;
            b = inNormal;
            c = inNormal;

        }
        /// <summary>
        /// Crea un plano en base a una normal y un float.
        /// </summary>
        /// <param name="inNormal"></param>
        /// <param name="d"></param>
        public Plano(Vec3 inNormal, float d)
        {
            this.normal = inNormal;
            this.distance = d;
            a = inNormal;
            b = inNormal;
            c = inNormal;
        }
        /// <summary>
        /// Crea un plano en base a 3 posiciones vec 3.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Plano(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = Vec3.Cross(b - a, c - a).normalized;
            this.distance = -Vec3.Dot(this.normal, a);
            this.a = a;
            this.b = b;
            this.c = c;
        }
        #endregion

        #region Functions
        /// <summary>
        /// devuelve el punto mas cercano del plano que este mas cerca del punto dado
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            var pointPlaneDistance = Vec3.Dot(point, normal) + distance;
            return point - (normal * pointPlaneDistance);
        }
        /// <summary>
        /// girar la cara del plano para que mire en direcion opuesta
        /// </summary>
        public void Flip()
        {
            this.normal = -normal;
            this.distance = -distance;
        }
        /// <summary>
        /// devuelve la distancia que hay entre el plano y el punto dado
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(point, normal) + distance;
        }
        /// <summary>
        /// devuelve si el putno que se da esta adentro del lado positivo del plano
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
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
        /// <summary>
        /// devuelve si los dos puntos estan en el mismo lado del plano
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public bool SameSide(Vec3 point1, Vec3 point2)
        {
            float distance1 = GetDistanceToPoint(point1);
            float distance2 = GetDistanceToPoint(point2);

            if (distance1 >= 0.0f && distance2 >= 0.0f) //Si la distancia de los dos puntos es mayor o igual a zero retorno true;
            {
                return true;
            }

            else //Si no retorno false.
            {
                return false;
            }
        }
        /// <summary>
        /// setea un plano usando 3 puntos que se encuenrtas adentro del mismo
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = Vec3.Cross(b - a, c - a).normalized;
            this.distance = -Vec3.Dot(this.normal, a);
        }
        /// <summary>
        /// setea el plano usando un punto que se encuentra dentro de el mas una normal para orientarlo
        /// </summary>
        /// <param name="inNormal"></param>
        /// <param name="point"></param>
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 point)
        {
            this.normal = Vec3.Cross(inNormal, point);
            this.distance = 0f + Vec3.Dot(inNormal, point);
        }
        /// <summary>
        /// devuelve una copia del plano dado que se mueve en el translate que se inserta
        /// </summary>
        /// <param name="newPlane"></param>
        /// <param name="translate"></param>
        /// <returns></returns>
        public static Plano Translate(Plano newPlane, Vec3 translate)
        {
            return new Plano(newPlane.normal, newPlane.distance += Vec3.Dot(newPlane.normal, translate));
                                                                                                         
        }
        /// <summary>
        /// traslada el plano en el espacio
        /// </summary>
        /// <param name="translate"></param>
        public void Translate(Vec3 translate)
        {
            distance += Vec3.Dot(normal, translate);
        }

        #endregion
    }
}
