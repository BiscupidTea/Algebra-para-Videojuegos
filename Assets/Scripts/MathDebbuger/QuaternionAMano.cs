using CustomMath;
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
            float x = (a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.z);
            float y = (a.w * b.y + a.x * b.z + a.y * b.w - a.z * b.x);
            float z = (a.w * b.z + a.x * b.y + a.y * b.x - a.z * b.w);
            float w = (a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z);

            return new QuaternionMod(x, y, z, w);
        }

        public static Vec3 operator *(QuaternionMod QuaternionMod, Vec3 vec3)
        {
            float rotX = QuaternionMod.x * 2f;
            float rotY = QuaternionMod.y * 2f;
            float rotZ = QuaternionMod.z * 2f;

            float rotX2 = QuaternionMod.x * rotX;
            float rotY2 = QuaternionMod.y * rotY;
            float rotZ2 = QuaternionMod.z * rotZ;

            float rotXY = QuaternionMod.x * rotY;
            float rotXZ = QuaternionMod.x * rotZ;
            float rotYZ = QuaternionMod.y * rotZ;

            float rotWX = QuaternionMod.w * rotX;
            float rotWY = QuaternionMod.w * rotY;
            float rotWZ = QuaternionMod.w * rotZ;

            Vec3 result = Vec3.Zero;

            result.x = (1f - (rotY2 + rotZ2)) * vec3.x + (rotXY - rotWZ) * vec3.y + (rotXZ + rotWY) * vec3.z;
            result.y = (rotXY + rotWZ) * vec3.x + (1f - (rotX2 + rotZ2)) * vec3.y + (rotYZ - rotWX) * vec3.z;
            result.z = (rotXZ - rotWY) * vec3.x + (rotYZ + rotWX) * vec3.y + (1f - (rotX2 + rotY2)) * vec3.z;

            return result;
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

            sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.z * 0.5f);
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.z * 0.5f);
            qz.Set(0, 0, sinAngle, cosAngle);

            sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.x * 0.5f);
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.x * 0.5f);
            qx.Set(sinAngle, 0, 0, cosAngle);

            sinAngle = Mathf.Sin(Mathf.Deg2Rad * euler.y * 0.5f);
            cosAngle = Mathf.Cos(Mathf.Deg2Rad * euler.y * 0.5f);
            qy.Set(0, sinAngle, 0, cosAngle);

            r = qy * qx * qz;

            return r;
        }

        public static Vec3 FromQuaternionToEuler(QuaternionMod rotation)
        {
            float sqw = rotation.w * rotation.w;
            float sqx = rotation.x * rotation.x;
            float sqy = rotation.y * rotation.y;
            float sqz = rotation.z * rotation.z;

            float unit = sqx + sqy + sqz + sqw; 
            float test = rotation.x * rotation.w - rotation.y * rotation.z;
            Vec3 v;

            if (test > 0.4999f * unit)   
            {
                v.y = 2f * Mathf.Atan2(rotation.y, rotation.x);
                v.x = Mathf.PI / 2;
                v.z = 0;
                return NormalizeAngles(v * Mathf.Rad2Deg);
            }
            if (test < -0.4999f * unit)  
            {
                v.y = -2f * Mathf.Atan2(rotation.y, rotation.x);
                v.x = -Mathf.PI / 2;
                v.z = 0;
                return NormalizeAngles(v * Mathf.Rad2Deg);
            }

            QuaternionMod q = new QuaternionMod(rotation.w, rotation.z, rotation.x, rotation.y);
            v.y = Mathf.Atan2(2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     
            v.x = Mathf.Asin(2f * (q.x * q.z - q.w * q.y));                                           
            v.z = Mathf.Atan2(2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));     
            return NormalizeAngles(v * Mathf.Rad2Deg);
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

            return IsEqualUsingDot(dot) ? 0f : (Mathf.Acos(Mathf.Min(Mathf.Abs(dot), 1f)) * 2f * Mathf.Rad2Deg);
        }
        private static bool IsEqualUsingDot(float dot)
        {
            return dot > 0.999999f;
        }
        public static QuaternionMod AngleAxis(float angle, Vec3 axis)
        {
            axis.Normalize();
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
            Vec3 axis = Vec3.Cross(fromDirection, toDirection);
            float angle = Vec3.Angle(fromDirection, toDirection);
            return AngleAxis(angle, axis.normalized);
        }
        public static QuaternionMod Inverse(QuaternionMod rotation)
        {
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
            QuaternionMod r;
            float time = 1 - t;
            r.x = time * a.x + t * b.x;
            r.y = time * a.y + t * b.y;
            r.z = time * a.z + t * b.z;
            r.w = time * a.w + t * b.w;

            r.Normalize();

            return r;
        }
        public static QuaternionMod LookRotation(Vec3 forward, Vec3 upwards)
        {
            Vec3 dir = (upwards - forward).normalized;
            Vec3 rotAxis = Vec3.Cross(Vec3.Forward, dir);
            float dot = Vec3.Dot(Vec3.Forward, dir);

            QuaternionMod result;
            result.x = rotAxis.x;
            result.y = rotAxis.y;
            result.z = rotAxis.z;
            result.w = dot + 1;

            return result.Normalized;
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
            QuaternionMod r;

            float time = 1 - t;

            float wa, wb;

            float angle = Mathf.Acos(Dot(a, b));

            angle = Mathf.Abs(angle);

            float sn = Mathf.Sin(angle);

            wa = Mathf.Sin(time * angle) / sn;
            wb = Mathf.Sin((1 - time) * angle) / sn;

            r.x = wa * a.x + wb * b.x;
            r.y = wa * a.y + wb * b.y;
            r.z = wa * a.z + wb * b.z;
            r.w = wa * a.w + wb * b.w;

            r.Normalize();

            return r;
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
