using CustomMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjerciciosVector3 : MonoBehaviour
{
    [Header("Exercise")]
    [SerializeField]private int selectedExercise;
    
    [Header("Coordinates")]
    [SerializeField] private Vec3 Point0;
    [SerializeField] private Vec3 Point1;
    [SerializeField] private Vec3 Point2;
    Vec3 PointEx;

    float lerp;

    private void Update()
    {
        Vec3 previusPointEx = PointEx;

        switch (selectedExercise)
        {
            case 1:
                Ejercicio1();
                break;

            case 2:
                Ejercicio2();
                break;

            case 3:
                Ejercicio3();
                break;

            case 4:
                Ejercicio4();
                break;

            case 5:
                Ejercicio5();
                break;

            case 6:
                Ejercicio6();
                break;

            case 7:
                Ejercicio7();
                break;

            case 8:
                Ejercicio8();
                break;

            case 9:
                Ejercicio9();
                break;

            case 10:
                Ejercicio10();
                break;

            default:
                break;
        }

        if (previusPointEx != PointEx) 
        {
            Debug.Log(PointEx);
        }
    }

    ////////////////////////////////////////////////

    private void Ejercicio1()
    {
        PointEx = new Vec3(Point1.x + Point2.x, Point1.y + Point2.y, Point1.z + Point2.z);
    }

    private void Ejercicio2()
    {
        PointEx = new Vec3(Point1.x - Point2.x, Point1.y - Point2.y, Point1.z - Point2.z);
    }

    private void Ejercicio3()
    {
        PointEx = new Vec3(Point1.x * Point2.x, Point1.y * Point2.y, Point1.z * Point2.z);
    }

    private void Ejercicio4()
    {
        PointEx = Vec3.Cross(Point1, Point2);
    }

    private void Ejercicio5()
    {
        lerp += Time.deltaTime;
        PointEx = Vec3.Lerp(Point1, Point2, lerp);

        if (lerp > 1)
        {
            lerp = 0;
        }
    }

    private void Ejercicio6()
    {
        PointEx = Vec3.Max(Point1, Point2);
    }

    private void Ejercicio7()
    {
        PointEx = Vec3.Project(Point1, Point2.normalized);
    }

    private void Ejercicio8()
    {
        PointEx = Vec3.Reflect(Point1, Point2.normalized);
        PointEx = -PointEx;
    }

    private void Ejercicio9()
    {
        PointEx = Vec3.Reflect(Point1, Point2.normalized);
    }

    private void Ejercicio10()
    {
        lerp += Time.deltaTime;
        PointEx = Vec3.LerpUnclamped(Point1, Point2, lerp);

        if (lerp < -10)
        {
            lerp = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Point0, Point1);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Point0, Point2);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(Point0, PointEx);
    }
}
