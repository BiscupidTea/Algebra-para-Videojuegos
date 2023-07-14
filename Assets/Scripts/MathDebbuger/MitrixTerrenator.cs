using CustomMath;
using System;
using UnityEngine;

public class MitrixTerrenator : MonoBehaviour
{
    public struct MatrixMano
    {
        #region variables
        public float m11;
        public float m12;
        public float m13;
        public float m14;
        public float m21;
        public float m22;
        public float m23;
        public float m24;
        public float m31;
        public float m32;
        public float m33;
        public float m34;
        public float m41;
        public float m42;
        public float m43;
        public float m44;
        #endregion

        #region constructor
        public MatrixMano(Vector4 col1, Vector4 col2, Vector4 col3, Vector4 col4)
        {
            m11 = col1.x;
            m12 = col2.x;
            m13 = col3.x;
            m14 = col4.x;
            m21 = col1.y;
            m22 = col2.y;
            m23 = col3.y;
            m24 = col4.y;
            m31 = col1.z;
            m32 = col2.z;
            m33 = col3.z;
            m34 = col4.z;
            m41 = col1.w;
            m42 = col2.w;
            m43 = col3.w;
            m44 = col4.w;
        }
        #endregion

        #region Operators
        public static MatrixMano operator *(MatrixMano a, MatrixMano b)
        {
            MatrixMano ret = zero;
            for (int i = 0; i < 4; i++)
            {
                ret.SetColumn(i, a * b.GetColumn(i));
            }
            return ret;
        }

        public static Vector4 operator *(MatrixMano a, Vector4 v)
        {
            Vector4 ret;
            ret.x = (float)((double)a.m11 * (double)v.x + (double)a.m12 * (double)v.y + (double)a.m13 * (double)v.z + (double)a.m14 * (double)v.w);
            ret.y = (float)((double)a.m21 * (double)v.x + (double)a.m22 * (double)v.y + (double)a.m23 * (double)v.z + (double)a.m24 * (double)v.w);
            ret.z = (float)((double)a.m31 * (double)v.x + (double)a.m32 * (double)v.y + (double)a.m33 * (double)v.z + (double)a.m34 * (double)v.w);
            ret.w = (float)((double)a.m41 * (double)v.x + (double)a.m42 * (double)v.y + (double)a.m43 * (double)v.z + (double)a.m44 * (double)v.w);
            return ret;
        }

        public static bool operator ==(MatrixMano a, MatrixMano b) => a.GetColumn(0) == b.GetColumn(0) && a.GetColumn(1) == b.GetColumn(1) && a.GetColumn(2) == b.GetColumn(2) && a.GetColumn(3) == b.GetColumn(3);

        public static bool operator !=(MatrixMano a, MatrixMano b) => !(a == b);
        #endregion

        #region Functions
        public float this[int row, int col]
        {
            get
            {
                return this[row + col * 4];
            }
            set
            {
                this[row + col * 4] = value;
            }
        }
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return m11;
                    case 1:
                        return m12;
                    case 2:
                        return m13;
                    case 3:
                        return m14;
                    case 4:
                        return m21;
                    case 5:
                        return m22;
                    case 6:
                        return m23;
                    case 7:
                        return m24;
                    case 8:
                        return m31;
                    case 9:
                        return m32;
                    case 10:
                        return m33;
                    case 11:
                        return m34;
                    case 12:
                        return m41;
                    case 13:
                        return m42;
                    case 14:
                        return m43;
                    case 15:
                        return m44;
                    default:
                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        m11 = value;
                        break;
                    case 1:
                        m21 = value;
                        break;
                    case 2:
                        m31 = value;
                        break;
                    case 3:
                        m41 = value;
                        break;
                    case 4:
                        m12 = value;
                        break;
                    case 5:
                        m22 = value;
                        break;
                    case 6:
                        m32 = value;
                        break;
                    case 7:
                        m42 = value;
                        break;
                    case 8:
                        m13 = value;
                        break;
                    case 9:
                        m23 = value;
                        break;
                    case 10:
                        m33 = value;
                        break;
                    case 11:
                        m43 = value;
                        break;
                    case 12:
                        m14 = value;
                        break;
                    case 13:
                        m24 = value;
                        break;
                    case 14:
                        m34 = value;
                        break;
                    case 15:
                        m44 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Index out of Range!");
                }
            }
        }
        public static MatrixMano zero
        {
            get
            {
                return new MatrixMano()
                {
                    m11 = 0.0f,
                    m12 = 0.0f,
                    m13 = 0.0f,
                    m14 = 0.0f,
                    m21 = 0.0f,
                    m22 = 0.0f,
                    m23 = 0.0f,
                    m24 = 0.0f,
                    m31 = 0.0f,
                    m32 = 0.0f,
                    m33 = 0.0f,
                    m34 = 0.0f,
                    m41 = 0.0f,
                    m42 = 0.0f,
                    m43 = 0.0f,
                    m44 = 0.0f
                };
            }
        }
        public static MatrixMano identity
        {
            get
            {
                MatrixMano m = zero;
                m.m11 = 1.0f;
                m.m22 = 1.0f;
                m.m33 = 1.0f;
                m.m44 = 1.0f;
                return m;
            }
        }
        public Quaternion rotation
        {
            get
            {
                MatrixMano m = this;
                Quaternion q = new Quaternion();
                q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
                q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
                q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
                q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
                q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
                q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
                q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
                return q;
            }
        }
        public Vec3 lossyScale
        {
            get
            {
                return new Vec3(GetColumn(0).magnitude, GetColumn(1).magnitude, GetColumn(2).magnitude);
            }
        }
        public bool IsIdentity
        {
            get
            {
                return m11 == 1f && m22 == 1f && m33 == 1f && m44 == 1f &&
                       m12 == 0f && m13 == 0f && m14 == 0f &&
                       m21 == 0f && m23 == 0f && m24 == 0f &&
                       m31 == 0f && m32 == 0f && m34 == 0f &&
                       m41 == 0f && m42 == 0f && m43 == 0f;
            }
        }
        public float determinant => Determinant(this);
        public MatrixMano transpose => Transpose(this);
        public MatrixMano inverse => Inverse(this);
       
        public static float Determinant(MatrixMano m)
        {
            //uso el teoream de laplace

            //El determinante de una matriz es igual a la suma de los productos (resultado de la multiplicacion) de cada elemento
            //de una fila o columna por el determinante de la fila o columna que se encuentra al lado. 

            return
                m[0, 3] * m[1, 2] * m[2, 1] * m[3, 0] - m[0, 2] * m[1, 3] * m[2, 1] * m[3, 0] -
                m[0, 3] * m[1, 1] * m[2, 2] * m[3, 0] + m[0, 1] * m[1, 3] * m[2, 2] * m[3, 0] +
                m[0, 2] * m[1, 1] * m[2, 3] * m[3, 0] - m[0, 1] * m[1, 2] * m[2, 3] * m[3, 0] -
                m[0, 3] * m[1, 2] * m[2, 0] * m[3, 1] + m[0, 2] * m[1, 3] * m[2, 0] * m[3, 1] +
                m[0, 3] * m[1, 0] * m[2, 2] * m[3, 1] - m[0, 0] * m[1, 3] * m[2, 2] * m[3, 1] -
                m[0, 2] * m[1, 0] * m[2, 3] * m[3, 1] + m[0, 0] * m[1, 2] * m[2, 3] * m[3, 1] +
                m[0, 3] * m[1, 1] * m[2, 0] * m[3, 2] - m[0, 1] * m[1, 3] * m[2, 0] * m[3, 2] -
                m[0, 3] * m[1, 0] * m[2, 1] * m[3, 2] + m[0, 0] * m[1, 3] * m[2, 1] * m[3, 2] +
                m[0, 1] * m[1, 0] * m[2, 3] * m[3, 2] - m[0, 0] * m[1, 1] * m[2, 3] * m[3, 2] -
                m[0, 2] * m[1, 1] * m[2, 0] * m[3, 3] + m[0, 1] * m[1, 2] * m[2, 0] * m[3, 3] +
                m[0, 2] * m[1, 0] * m[2, 1] * m[3, 3] - m[0, 0] * m[1, 2] * m[2, 1] * m[3, 3] -
                m[0, 1] * m[1, 0] * m[2, 2] * m[3, 3] + m[0, 0] * m[1, 1] * m[2, 2] * m[3, 3];
        }
        public static MatrixMano Inverse(MatrixMano m)
        {
            float detA = Determinant(m);
            if (detA == 0)
                return zero;

            //matriz auxiliar en la cual se guarda la determinante de cada una de esas posiciones.
            MatrixMano aux = new MatrixMano()
            {
                //-------1---------
                m11 = m.m22 * m.m33 * m.m44 + m.m23 * m.m34 * m.m42 + m.m24 * m.m32 * m.m43 - m.m22 * m.m34 * m.m43 - m.m23 * m.m32 * m.m44 - m.m24 * m.m33 * m.m42,
                m12 = m.m21 * m.m34 * m.m32 + m.m13 * m.m32 * m.m44 + m.m14 * m.m33 * m.m42 - m.m12 * m.m33 * m.m44 - m.m13 * m.m34 * m.m42 - m.m14 * m.m32 * m.m43,
                m13 = m.m21 * m.m23 * m.m33 + m.m13 * m.m24 * m.m43 + m.m14 * m.m22 * m.m43 - m.m12 * m.m24 * m.m43 - m.m13 * m.m22 * m.m44 - m.m14 * m.m23 * m.m42,
                m14 = m.m21 * m.m24 * m.m22 + m.m13 * m.m22 * m.m34 + m.m14 * m.m23 * m.m32 - m.m12 * m.m23 * m.m34 - m.m13 * m.m24 * m.m32 - m.m14 * m.m22 * m.m33,
                //-------2--------					     								    
                m21 = m.m21 * m.m34 * m.m43 + m.m23 * m.m31 * m.m44 + m.m24 * m.m33 * m.m41 - m.m21 * m.m33 * m.m44 - m.m23 * m.m34 * m.m41 - m.m24 * m.m31 * m.m43,
                m22 = m.m11 * m.m44 * m.m44 + m.m13 * m.m34 * m.m41 + m.m14 * m.m31 * m.m43 - m.m11 * m.m34 * m.m43 - m.m13 * m.m31 * m.m44 - m.m14 * m.m33 * m.m41,
                m23 = m.m11 * m.m24 * m.m43 + m.m13 * m.m21 * m.m44 + m.m14 * m.m23 * m.m41 - m.m11 * m.m23 * m.m44 - m.m13 * m.m24 * m.m41 - m.m14 * m.m21 * m.m43,
                m24 = m.m11 * m.m23 * m.m34 + m.m13 * m.m24 * m.m31 + m.m14 * m.m21 * m.m33 - m.m11 * m.m24 * m.m33 - m.m13 * m.m21 * m.m34 - m.m14 * m.m23 * m.m31,
                //-------3--------					     								    
                m31 = m.m21 * m.m32 * m.m44 + m.m22 * m.m34 * m.m41 + m.m24 * m.m31 * m.m42 - m.m21 * m.m34 * m.m42 - m.m22 * m.m31 * m.m44 - m.m24 * m.m42 * m.m41,
                m32 = m.m11 * m.m34 * m.m42 + m.m12 * m.m31 * m.m44 + m.m14 * m.m32 * m.m41 - m.m11 * m.m32 * m.m44 - m.m12 * m.m34 * m.m41 - m.m14 * m.m31 * m.m42,
                m33 = m.m11 * m.m22 * m.m44 + m.m12 * m.m24 * m.m42 + m.m14 * m.m21 * m.m42 - m.m11 * m.m24 * m.m42 - m.m12 * m.m21 * m.m44 - m.m14 * m.m22 * m.m41,
                m34 = m.m11 * m.m24 * m.m32 + m.m12 * m.m21 * m.m34 + m.m14 * m.m22 * m.m42 - m.m11 * m.m22 * m.m34 - m.m12 * m.m24 * m.m31 - m.m14 * m.m21 * m.m32,
                //------4---------					     								    
                m41 = m.m21 * m.m33 * m.m42 + m.m22 * m.m31 * m.m43 + m.m23 * m.m32 * m.m41 - m.m11 * m.m32 * m.m43 - m.m22 * m.m33 * m.m41 - m.m23 * m.m31 * m.m42,
                m42 = m.m11 * m.m32 * m.m43 + m.m12 * m.m33 * m.m41 + m.m13 * m.m31 * m.m42 - m.m11 * m.m33 * m.m42 - m.m12 * m.m31 * m.m43 - m.m13 * m.m32 * m.m41,
                m43 = m.m11 * m.m23 * m.m42 + m.m12 * m.m21 * m.m43 + m.m13 * m.m22 * m.m41 - m.m11 * m.m22 * m.m43 - m.m12 * m.m23 * m.m41 - m.m13 * m.m21 * m.m42,
                m44 = m.m11 * m.m22 * m.m33 + m.m12 * m.m23 * m.m31 + m.m13 * m.m21 * m.m32 - m.m11 * m.m23 * m.m32 - m.m12 * m.m21 * m.m33 - m.m13 * m.m22 * m.m31
            };

            //Divide las posiciones por el determinante para invertirlos.
            MatrixMano ret = new MatrixMano()
            {
                m11 = aux.m11 / detA,
                m12 = aux.m12 / detA,
                m13 = aux.m13 / detA,
                m14 = aux.m14 / detA,
                m21 = aux.m21 / detA,
                m22 = aux.m22 / detA,
                m23 = aux.m23 / detA,
                m24 = aux.m24 / detA,
                m31 = aux.m31 / detA,
                m32 = aux.m32 / detA,
                m33 = aux.m33 / detA,
                m34 = aux.m34 / detA,
                m41 = aux.m41 / detA,
                m42 = aux.m42 / detA,
                m43 = aux.m43 / detA,
                m44 = aux.m44 / detA

            };
            //matriz invertida
            return ret;
        }
        public static MatrixMano Rotate(Quaternion q)
        {
            //Obtengo la rotacion con un quaternion.
            double num1 = q.x * 2f;
            double num2 = q.y * 2f;
            double num3 = q.z * 2f;

            double num4 = q.x * num1;
            double num5 = q.y * num2;
            double num6 = q.z * num3;
            double num7 = q.x * num2;
            double num8 = q.x * num3;
            double num9 = q.y * num3;

            double num10 = q.w * num1;
            double num11 = q.w * num2;
            double num12 = q.w * num3;

            //Genero la rotacion de la matrix.
            MatrixMano m;
            m.m11 = (float)(1.0 - num5 + num6);
            m.m21 = (float)(num7 + num12);
            m.m31 = (float)(num8 - num11);
            m.m41 = 0.0f;
            m.m12 = (float)(num7 - num12);
            m.m22 = (float)(1.0 - num4 + num6);
            m.m32 = (float)(num9 + num10);
            m.m42 = 0.0f;
            m.m13 = (float)(num8 + num11);
            m.m23 = (float)(num9 - num10);
            m.m33 = (float)(1.0 - num4 + num5);
            m.m43 = 0.0f;
            m.m14 = 0.0f;
            m.m24 = 0.0f;
            m.m34 = 0.0f;
            m.m44 = 1f;

            return m;
        }
        public static MatrixMano Scale(Vec3 v)
        {
            //Se le asignas los valores X, Y, Z del vector a la diagonal (m00, m11, m22).
            //La varianle m33 se le asigna 1 ya que tambien es parte de la diagonal.
            MatrixMano m;
            m.m11 = v.x;
            m.m21 = 0.0f;
            m.m31 = 0.0f;
            m.m41 = 0.0f;
            m.m12 = 0.0f;

            m.m22 = v.y;
            m.m32 = 0.0f;
            m.m42 = 0.0f;
            m.m13 = 0.0f;
            m.m23 = 0.0f;

            m.m33 = v.z;
            m.m43 = 0.0f;
            m.m14 = 0.0f;
            m.m24 = 0.0f;
            m.m34 = 0.0f;
            m.m44 = 1f;

            return m;
        }
        public static MatrixMano Translate(Vec3 v)
        {
            //Se le asignan los valores XYZ a los valores del vector a las variables (m03, m13, m23).
            //A la diagonal se la setea en 1.
            MatrixMano m;
            m.m11 = 1f;
            m.m21 = 0.0f;
            m.m31 = 0.0f;

            m.m41 = v.x;
            m.m12 = 0.0f;
            m.m22 = 1f;
            m.m32 = 0.0f;

            m.m42 = v.y;
            m.m13 = 0.0f;
            m.m23 = 0.0f;
            m.m33 = 1f;

            m.m43 = v.z;
            m.m14 = 0.0f;
            m.m24 = 0.0f;
            m.m34 = 0.0f;
            m.m44 = 1f;

            return m;
        }
        public static MatrixMano Transpose(MatrixMano m)
        {
            return new MatrixMano()
            {
                m12 = m.m21,
                m13 = m.m31,
                m14 = m.m41,
                m21 = m.m12,
                m23 = m.m32,
                m24 = m.m42,
                m31 = m.m13,
                m32 = m.m23,
                m34 = m.m43,
                m41 = m.m14,
                m42 = m.m24,
                m43 = m.m34,
            };
        }
        public static MatrixMano TRS(Vec3 pos, Quaternion q, Vec3 s)
        {
            return (Translate(pos) * Rotate(q) * Scale(s));
        }
        public Vector4 GetColumn(int i)
        {
            return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
        }
        public Vec3 GetPosition()
        {
            return new Vec3(m14, m24, m34);
        }
        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4(m11, m12, m13, m14);
                case 1:
                    return new Vector4(m21, m22, m23, m24);
                case 2:
                    return new Vector4(m31, m23, m33, m34);
                case 3:
                    return new Vector4(m41, m24, m43, m44);
                default:
                    throw new IndexOutOfRangeException("Index out of Range!");
            }
        }
        public Vec3 MultiplyPoint(Vec3 p)
        {
            Vec3 v3;

            v3.x = (float)((double)m11 * (double)p.x + (double)m12 * (double)p.y + (double)m13 * (double)p.z) + m14;
            v3.y = (float)((double)m21 * (double)p.x + (double)m22 * (double)p.y + (double)m23 * (double)p.z) + m24;
            v3.z = (float)((double)m31 * (double)p.x + (double)m32 * (double)p.y + (double)m33 * (double)p.z) + m34;
            float num = 1f / ((float)((double)m41 * (double)p.x + (double)m32 * (double)p.y + (double)m43 * (double)p.z) + m44);
            v3.x *= num;
            v3.y *= num;
            v3.z *= num;
            return v3;
        }
        public Vec3 MultiplyPoint3x4(Vec3 p)
        {
            Vec3 v3;
            v3.x = (float)((double)m11 * (double)p.x + (double)m12 * (double)p.y + (double)m13 * (double)p.z) + m14;
            v3.y = (float)((double)m21 * (double)p.x + (double)m22 * (double)p.y + (double)m23 * (double)p.z) + m24;
            v3.z = (float)((double)m31 * (double)p.x + (double)m32 * (double)p.y + (double)m33 * (double)p.z) + m34;
            return v3;
        }
        public Vec3 MultiplyVector(Vec3 v)
        {
            Vec3 v3;
            v3.x = (float)((double)m11 * (double)v.x + (double)m12 * (double)v.y + (double)m13 * (double)v.z);
            v3.y = (float)((double)m21 * (double)v.x + (double)m22 * (double)v.y + (double)m23 * (double)v.z);
            v3.z = (float)((double)m31 * (double)v.x + (double)m32 * (double)v.y + (double)m33 * (double)v.z);
            return v3;
        }
        public void SetColumn(int index, Vector4 col)
        {
            this[0, index] = col.x;
            this[1, index] = col.y;
            this[2, index] = col.z;
            this[3, index] = col.w;
        }
        public void SetRow(int index, Vector4 row)
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
            this[index, 3] = row.w;
        }
        public void SetTRS(Vec3 pos, Quaternion q, Vec3 s)
        {
            this = TRS(pos, q, s);
        }
        public bool ValidTRS()
        {
            if (lossyScale == Vec3.Zero)
                return false;
            else if (m11 == double.NaN && m21 == double.NaN && m31 == double.NaN && m41 == double.NaN &&
                     m12 == double.NaN && m22 == double.NaN && m32 == double.NaN && m42 == double.NaN &&
                     m13 == double.NaN && m23 == double.NaN && m33 == double.NaN && m43 == double.NaN &&
                     m14 == double.NaN && m24 == double.NaN && m34 == double.NaN && m44 == double.NaN)
                return false;
            else if (rotation.x > 1 && rotation.x < -1 && rotation.y > 1 && rotation.y < -1 && rotation.z > 1 && rotation.z < -1 && rotation.w > 1 && rotation.w < -1)
                return false;
            else
                return true;
        }
        #endregion
    }
}