using CustomMath;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuaternionAMano : MonoBehaviour
{
    public struct QuaternionMod
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public const float kEpsilon = 1E-06F;

        public QuaternionMod(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        static readonly QuaternionMod identityQuaternion = new QuaternionMod(0f, 0f, 0f, 1f);

        public static QuaternionMod identity
        {
            get
            {
                return identityQuaternion;
            }
        }

        public QuaternionMod Normalized => Normalize(this);

        #region Operators

        public static bool operator ==(QuaternionMod leftQuaternionMod, QuaternionMod rightQuaternionMod)
        {
            return (leftQuaternionMod == rightQuaternionMod);
        }

        public static bool operator !=(QuaternionMod leftQuaternionMod, QuaternionMod rightQuaternionMod)
        {
            return (leftQuaternionMod != rightQuaternionMod);
        }

        public static QuaternionMod operator *(QuaternionMod a, QuaternionMod b)
        {
            //formula de hamilton
            float x = (a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.z);
            float y = (a.w * b.y + a.x * b.z + a.y * b.w - a.z * b.x);
            float z = (a.w * b.z + a.x * b.y + a.y * b.x - a.z * b.w);
            float w = (a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z);

            return new QuaternionMod(x, y, z, w);
        }


        public static Vec3 operator *(QuaternionMod QuaternionMod, Vec3 vec3)
        {
            // se crea este quaternion sin valor en W que es la representacion de un vector dentro del quaternion 
            //Luego se mutiplican los quaterniones
            QuaternionMod p = new QuaternionMod(vec3.x, vec3.y, vec3.z, 0);
            QuaternionMod p2 = (QuaternionMod * p) * Inverse(QuaternionMod);
            Vec3 res = new Vec3(p2.x, p2.y, p2.z);
            return res;
        }
        #endregion

        #region Functions
        private static Vec3 NormalizeAngles(Vec3 angles)
        {
            angles.x = NormalizeAngle(angles.x);
            angles.y = NormalizeAngle(angles.y);
            angles.z = NormalizeAngle(angles.z);
            return angles;
        }
        private static float NormalizeAngle(float angle)
        {
            while (angle > 360)
                angle -= 360;
            while (angle < 0)
                angle += 360;
            return angle;
        }
        static QuaternionMod FromEulerToQuaternion(Vec3 euler)
        {
            float sinAngle = 0.0f;
            float cosAngle = 0.0f;

            QuaternionMod qx = identity;
            QuaternionMod qy = identity;
            QuaternionMod qz = identity;
            QuaternionMod r = identity;

            sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.z * 0.5f);// Se calcula la parte imaginaria (Z)
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.z * 0.5f);// Y se calcula la parte real W
            qz = new QuaternionMod(0, 0, sinAngle, cosAngle);

            sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.x * 0.5f);// Se calcula la parte imaginaria (X)
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.x * 0.5f);// Y se calcula la parte real W
            qx = new QuaternionMod(sinAngle, 0, 0, cosAngle);

            sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.y * 0.5f);// Se calcula la parte imaginaria (Y)
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.y * 0.5f);// Y se calcula la parte real W
            qy = new QuaternionMod(0, sinAngle, 0, cosAngle);

            // Se multiplica de esta manera (Con la Y al principio)
            r = qy * qx * qz;

            return r;
        }
        public static Vec3 FromQuaternionToEuler(QuaternionMod rotation)
        {
            Vec3 angles;

            //(x-axis rotation)
            float SinX = 2 * (rotation.w * rotation.x + rotation.y * rotation.z); // Y y Z se calculan para saber cuanto afecta esos valores sobre X
            float CosX = 1 - 2 * (rotation.x * rotation.x + rotation.y * rotation.y);// 1 porque es el quaternion normalizado, el 2 porque se mezcla con la cantidad dimenciones
                                                 // Y se 
            angles.x = Mathf.Atan2(SinX, CosX);// resulta en la rotacion del eje en X

            //(y-axis rotation) //Se hace de esta forma para evitar un gimbal lock
            float SinY = Mathf.Sqrt(1 + 2 * (rotation.w * rotation.y - rotation.x * rotation.z));
            float CosY = Mathf.Sqrt(1 - 2 * (rotation.w * rotation.y - rotation.x * rotation.z));
            angles.y = 2 * Mathf.Atan2(SinY, CosY) - MathF.PI / 2; // Luego se realiza la operacion con pi para alinearlo con el resto de los ejes a 90 grados

            //(z-axis rotation) // Se calcula igual que X
            float SinZ = 2 * (rotation.w * rotation.z + rotation.x * rotation.y);
            float CosZ = 1 - 2 * (rotation.y * rotation.y + rotation.z * rotation.z);
            angles.z = Mathf.Atan2(SinZ, CosZ);

            return angles; //devuelve los angulos euler
        }
        public Vec3 eulerAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return FromQuaternionToEuler(this);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this = FromEulerToQuaternion(value);
            }

        }
        public void Normalize()
        {
            this = Normalize(this);
        }
        public static float Angle(QuaternionMod a, QuaternionMod b)
        {
            float dot = Dot(a, b);
            // Se saca el valor absoluto porque los angulos siempre son positivos
            float dotAbs = Mathf.Abs(dot);
            // Luego calcula el angulo entre los 2 quaterniones y se lo multiplica por 2 por la cantidad de dimenciones en las que trabajamos
            return IsEqualUsingDot(dot) ? 0.0f : Mathf.Acos(Mathf.Min(dotAbs, 1.0f)) * 2.0f * Mathf.Rad2Deg;
        }
        private static bool IsEqualUsingDot(float dot)
        {
            return dot > 0.999999f;
        }
        public static QuaternionMod AngleAxis(float angle, Vec3 axis)
        {
            // primero se normaliza el eje de rotacion sobre la cual se va a rotar el quaternion
            axis.Normalize();
            // Se calcula la parte imaginaria de rotacion en base al eje
            axis *= Mathf.Sin(angle * Mathf.Deg2Rad * 0.5f);
            return new QuaternionMod(axis.x, axis.y, axis.z, Mathf.Cos(angle * Mathf.Deg2Rad * 0.5f));
        }
        public static QuaternionMod AxisAngle(Vec3 axis, float angle)
        {
            return AngleAxis(Mathf.Rad2Deg * angle, axis);
        }
        public static float Dot(QuaternionMod a, QuaternionMod b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }
        public static QuaternionMod Euler(float xq, float yq, float zq)
        {
            return FromEulerToQuaternion(new Vec3(xq, yq, zq));
        }
        public static QuaternionMod Euler(Vec3 angle)
        {
            return FromEulerToQuaternion(angle);
        }
        public static QuaternionMod FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            // Crea un quaternion que representa la rotacion de un vector sobre otro
            //Primero seca el eje de rotacion sobre la cual se va a rotar un vector sobre otro
            Vec3 axis = Vec3.Cross(fromDirection, toDirection);
            //Luego se calcula el angulo entre estos 2 vectores.
            float angle = Vec3.Angle(fromDirection, toDirection);
            //Y se rota devuelve un quaternion que almacena la rotacion necesaria para que un vector quede sobre otro.
            return AngleAxis(angle, axis.normalized);
        }
        public static QuaternionMod Inverse(QuaternionMod rotation)
        {
            //Simplemenete niega las partes imaginarias del quaternion para lograr invertir la direccion de la rotacion.
            QuaternionMod inverseQuaternion;
            inverseQuaternion.w = rotation.w;
            inverseQuaternion.x = -rotation.x;
            inverseQuaternion.y = -rotation.y;
            inverseQuaternion.z = -rotation.z;
            return inverseQuaternion;
        }
        public static QuaternionMod Lerp(QuaternionMod a, QuaternionMod b, float t)
        {
            return LerpUnclamped(a, b, Mathf.Clamp01(t));
        }
        public static QuaternionMod LerpUnclamped(QuaternionMod a, QuaternionMod b, float t)
        {
            QuaternionMod r = identity;

            float timeLeft = 1f - t;

            if (Dot(a, b) >= 0f)
            {
                r.x = (timeLeft * a.x) + (t * b.x);
                r.y = (timeLeft * a.y) + (t * b.y);
                r.z = (timeLeft * a.z) + (t * b.z);
                r.w = (timeLeft * a.w) + (t * b.w);
            }
            else
            {
                r.x = (timeLeft * a.x) - (t * b.x);
                r.y = (timeLeft * a.y) - (t * b.y);
                r.z = (timeLeft * a.z) - (t * b.z);
                r.w = (timeLeft * a.w) - (t * b.w);
            }

            r.Normalize();

            return r;
        }
        public static QuaternionMod LookRotation(Vec3 forward, Vec3 upwards)
        {
            //Aca mantiene la relacion entre los ejes
            Vec3 norm = new Vec3();
            forward = norm.Normalize(forward);
            Vec3 right = norm.Normalize(Vec3.Cross(upwards, forward));
            upwards = Vec3.Cross(forward, right);

            //Inicializa una matriz 3x3 con la rotacion de los 3 ejes.
            float m00 = right.x; float m01 = right.y; float m02 = right.z;
            float m10 = upwards.x; float m11 = upwards.y; float m12 = upwards.z;
            float m20 = forward.x; float m21 = forward.y; float m22 = forward.z;

            // define la diagonal
            //saber que eje rotar exactamente
            float diagonals = m00 + m11 + m22;
            var q = new QuaternionMod();
            if (diagonals > 0f)
            {
                float num = Mathf.Sqrt(diagonals + 1f);
                q.w = num * 0.5f;
                num = 0.5f / num;
                q.x = (m12 - m21) * num;
                q.y = (m20 - m02) * num;
                q.z = (m01 - m10) * num;
                return q;
            }
            if (m00 >= m11 && m00 >= m22)
            {
                float num = Mathf.Sqrt(1f + m00 - m11 - m22);
                float num4 = 0.5f / num;
                q.x = 0.5f * num;
                q.y = (m01 + m10) * num4;
                q.z = (m02 + m20) * num4;
                q.w = (m12 - m21) * num4;
                return q;
            }
            if (m11 > m22)
            {
                float num = Mathf.Sqrt(1f + m11 - m00 - m22);
                float num3 = 0.5f / num;
                q.x = (m10 + m01) * num3;
                q.y = 0.5f * num;
                q.z = (m21 + m12) * num3;
                q.w = (m20 - m02) * num3;
                return q;
            }

            float num5 = Mathf.Sqrt(1f + m22 - m00 - m11);
            float num2 = 0.5f / num5;
            q.x = (m20 + m02) * num2;
            q.y = (m21 + m12) * num2;
            q.z = 0.5f * num5;
            q.w = (m01 - m10) * num2;

            return q;
        }
        public static QuaternionMod Normalize(QuaternionMod q)
        {
            float sqrtDot = Mathf.Sqrt(Dot(q, q));

            if (sqrtDot < Mathf.Epsilon)
            {
                return identity;
            }

            return new QuaternionMod(q.x / sqrtDot, q.y / sqrtDot, q.z / sqrtDot, q.w / sqrtDot);
        }
        public static QuaternionMod RotateTowards(QuaternionMod from, QuaternionMod to, float maxDegreesDelta)
        {
            float angle = Angle(from, to);

            if (angle == 0f)
            {
                return to;
            }

            return SlerpUnclamped(from, to, Mathf.Min(1f, maxDegreesDelta / angle));
        }
        public static QuaternionMod Slerp(QuaternionMod a, QuaternionMod b, float t)
        {
            return SlerpUnclamped(a, b, Mathf.Clamp01(t));
        }
        public static QuaternionMod SlerpUnclamped(QuaternionMod a, QuaternionMod b, float t)
        {
            // se chequea que los quaternoiones no sean similares.
            float dot = Dot(a, b);
            if (dot >= 1.0f || dot <= -1.0f)
            {
                return a;
            }
            else if (dot < 0.0f)
            {
                b.x = -b.x;
                b.y = -b.y;
                b.z = -b.z;
                b.w = -b.w;
                dot = -dot;
            }

            float blendA;
            float blendB;
            if (dot < 0.99f)
            {
                //Luego se calcula el angulo entre los quaterniones
                float halfAngle = Mathf.Acos(dot);
                float sinHalfAngle = Mathf.Sin(halfAngle);
                float oneOverSinHalfAngle = 1.0f / sinHalfAngle;
                blendA = Mathf.Sin(halfAngle * (1.0f - t)) * oneOverSinHalfAngle;
                blendB = Mathf.Sin(halfAngle * t) * oneOverSinHalfAngle;
            }
            else
            {
                blendA = 1.0f - t;
                blendB = t;
            }

            QuaternionMod result = new QuaternionMod(blendA * a.x + blendB * b.x, blendA * a.y + blendB * b.y, blendA * a.z + blendB * b.z, blendA * a.w + blendB * b.w);

            return Normalize(result);
        }
        public void Set(float new_x, float new_y, float new_z, float new_w)
        {
            x = new_x;
            y = new_y;
            z = new_z;
            w = new_w;
        }
        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            this = FromToRotation(fromDirection, toDirection);
        }
        public void SetLookRotation(Vec3 view)
        {
            this = LookRotation(view, Vec3.Up);
        }
        public void SetLookRotation(Vec3 view, Vec3 upwards)
        {
            this = LookRotation(view, upwards);
        }
        public void ToAngleAxis(out float angle, out Vec3 axis)
        {
            Normalize();
            angle = 2.0f * Mathf.Acos(w);
            float mag = Mathf.Sqrt(1.0f - w * w);
            if (mag > 0.0001f)
            {
                axis = new Vec3(x, y, z) / mag;
            }
            else
            {
                axis = new Vec3(1, 0, 0);
            }
        }
        public string ToString()
        {
            return new string("X Value : " + this.x + ", Y Value : " + this.y + ", Z Value : " + this.z + ", W Value : " + this.w);
        }

        #endregion
    }
}
