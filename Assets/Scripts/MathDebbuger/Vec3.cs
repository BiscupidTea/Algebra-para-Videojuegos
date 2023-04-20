using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

namespace CustomMath
{
    [Serializable]
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude { get { return (x * x + y * y + z * z); } }
        public Vec3 normalized { get { return new Vec3(x / magnitude, y / magnitude, z / magnitude); } }
        public float magnitude { get { return MathF.Sqrt(x * x + y * y + z * z); } }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }

        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector2(v2.x, v2.y);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        /// <summary>
        /// Calculate the angle between 2 vectors
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float Angle(Vec3 from, Vec3 to)
        {
            return MathF.Acos(Vec3.Dot(from.normalized, to.normalized)) * 180 / MathF.PI;
        }
        /// <summary>
        /// Returns a copy of vector with its magnitude clamped to maxLength.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            return vector.magnitude > maxLength ? vector.normalized * maxLength : vector;
        }
        /// <summary>
        /// Return magnitude 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float Magnitude(Vec3 vector)
        {
            return MathF.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        /// <summary>
        /// Cross Product of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            float i = (a.y * b.z) - (a.z * b.y);
            float j = (a.x * b.z) - (a.z * b.x);
            float k = (a.x * b.y) - (a.y * b.x);
            return new Vec3(i, -j, k);
        }
        /// <summary>
        /// Returns the distance between a and b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Distance(Vec3 a, Vec3 b)
        {
            //pitagoras 
            float distance = MathF.Sqrt(MathF.Pow((a.x - b.x), 2) + MathF.Pow((a.y - b.y), 2) + MathF.Pow((a.z - b.z), 2));
            return distance;
        }
        /// <summary>
        /// Dot Product of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        /// <summary>
        /// Linearly interpolates between two points.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            Mathf.Clamp(t, 0, 1);
            return a + (b - a) * t;
        }
        /// <summary>
        /// Linearly interpolates between two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            return a + (b - a) * t;
        }
        /// <summary>
        /// Returns a vector that is made from the largest components of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            Vec3 MaxPosition;

            if (a.x > b.x)
            {
                MaxPosition.x = a.x;
            }
            else
            {
                MaxPosition.x = b.x;
            }

            if (a.z > b.z)
            {
                MaxPosition.z = a.z;
            }
            else
            {
                MaxPosition.z = b.z;
            }

            if (a.y > b.y)
            {
                MaxPosition.y = a.y;
            }
            else
            {
                MaxPosition.y = b.y;
            }

            return MaxPosition;
        }
        /// <summary>
        /// Returns a vector that is made from the smallest components of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            Vec3 MinPosition;

            if (a.x < b.x)
            {
                MinPosition.x = a.x;
            }
            else
            {
                MinPosition.x = b.x;
            }

            if (a.z < b.z)
            {
                MinPosition.z = a.z;
            }
            else
            {
                MinPosition.z = b.z;
            }

            if (a.y < b.y)
            {
                MinPosition.y = a.y;
            }
            else
            {
                MinPosition.y = b.y;
            }

            return MinPosition;
        }
        /// <summary>
        /// Returns the squared length of this vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float SqrMagnitude(Vec3 vector)
        {
            return (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        /// <summary>
        /// Projects a vector onto another vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        /// <returns></returns>
        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            float sqrMag = Dot(onNormal, onNormal);
            if (sqrMag < epsilon)
            {
                return Zero;
            }
            else
            {
                float dot = Dot(vector, onNormal);
                return onNormal * dot / sqrMag;
            }
        }
        /// <summary>
        /// Reflects a vector off the plane defined by a normal.
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        /// <returns></returns>
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            return inDirection - 2 * (Dot(inDirection, inNormal)) * inNormal;
        }
        /// <summary>
        /// Set x, y and z components of an existing Vector3.
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="newZ"></param>
        public void Set(float newX, float newY, float newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }
        /// <summary>
        /// Multiplies two vectors component-wise.
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vec3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }
        /// <summary>
        /// Makes this vector have a magnitude of 1.
        /// </summary>
        public void Normalize()
        {
            x = x / Magnitude(this);
            y = y / Magnitude(this);
            z = z / Magnitude(this);
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}