using CustomMatematic;
using CustomMath;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectCollider : MonoBehaviour
{
    public List<Plano> planos;
    public List<Vec3> checkPoints;
    public List<Vec3> insidePoints;
    public List<Vec3> nearestPoints;

    private void Start()
    {
        planos = new List<Plano>();
        checkPoints = new List<Vec3>();
        insidePoints = new List<Vec3>();
        nearestPoints = new List<Vec3>();



        for (int x = 0; x < Grid.grid.GetLength(0); x++)
        {
            for (int y = 0; y < Grid.grid.GetLength(1); y++)
            {
                for (int z = 0; z < Grid.grid.GetLength(2); z++)
                {
                    nearestPoints.Add(Grid.grid[x, y, z]);
                }
            }
        }
    }

    private void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        planos.Clear();

        for (int i = 0; i < mesh.GetIndices(0).Length; i += 3)
        {
            Vec3 vert1 = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i]]));

            Vec3 vert2 = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 1]]));

            Vec3 vert3 = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 2]]));

            var newPlane = new Plano(vert1, vert2, vert3);

            newPlane.distance *= -1;

            planos.Add(newPlane);
        }

        insidePoints.Clear();
        foreach (Vec3 point in nearestPoints)
        {
            int indice = 0;
            int counter = 0;
            foreach (Plano plano in planos)
            {
                if (IsCollidingPlane(plano, point, out Vec3 collidingPoint))
                {
                    Vec3 a = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[indice]]));
                    Vec3 b = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[indice + 1]]));
                    Vec3 c = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[indice + 2]]));

                    if (IsValidPlane(plano, collidingPoint, a, b, c))
                    {
                        counter++;
                    }
                }

                indice += 3;
            }
            if (counter % 2 == 1)
            {
                insidePoints.Add(point);
            }
        }


    }

    private bool IsCollidingPlane(Plano plano, Vec3 point, out Vec3 collidingPoint)
    {
        collidingPoint = Vec3.Zero;

        float denom = Vec3.Dot(plano.normal, Vec3.Right * 20.0f);

        if (Mathf.Abs(denom) > Vec3.epsilon)
        {
            float t = Vec3.Dot((plano.normal * plano.distance - point), plano.normal) / denom;
            if (t >= Vec3.epsilon)
            {
                collidingPoint = point + (Vec3.Right * 20.0f) * t;
                return true;
            }
        }

        return false;
    }

    private bool IsValidPlane(Plano plano, Vec3 point, Vec3 a, Vec3 b, Vec3 c)
    {
        float x1 = a.x; float y1 = a.y;
        float x2 = b.x; float y2 = b.y;
        float x3 = c.x; float y3 = c.y;

        // Area del triangulo
        float areaOrig = Mathf.Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));

        // Areas de los 3 triangulos hechos con el punto y las esquinas
        float area1 = Mathf.Abs((x1 - point.x) * (y2 - point.y) - (x2 - point.x) * (y1 - point.y));
        float area2 = Mathf.Abs((x2 - point.x) * (y3 - point.y) - (x3 - point.x) * (y2 - point.y));
        float area3 = Mathf.Abs((x3 - point.x) * (y1 - point.y) - (x1 - point.x) * (y3 - point.y));


        // Si la suma del area de los 3 triangulos es igual a la del original estamos adentro
        return Math.Abs(area1 + area2 + area3 - areaOrig) < Vec3.epsilon; //fijatse de cambiar pr comparacion aepsilon
    }

    public void DrawPlane(Vec3 position, Vec3 normal, Color color)
    {
        Vector3 v3;
        if (normal.normalized != Vec3.Forward)
            v3 = Vec3.Cross(normal, Vec3.Forward).normalized * normal.magnitude;
        else
            v3 = Vec3.Cross(normal, Vec3.Up).normalized * normal.magnitude; ;
        var corner0 = position + v3;
        var corner2 = position - v3;
        var normall = new Vector3(normal.x, normal.y, normal.z);
        var q = Quaternion.AngleAxis(90.0f, normall);
        v3 = q * v3;
        var corner1 = position + v3;
        var corner3 = position - v3;
        Debug.DrawLine(corner0, corner2, color);
        Debug.DrawLine(corner1, corner3, color);
        Debug.DrawLine(corner0, corner1, color);
        Debug.DrawLine(corner1, corner2, color);
        Debug.DrawLine(corner2, corner3, color);
        Debug.DrawLine(corner3, corner0, color);
        Debug.DrawRay(position, normal, Color.magenta);
    }

    private void OnDrawGizmos()
    {
        foreach (Plano plane in planos)
        {
            DrawPlane(plane.normal * plane.distance, plane.normal, Color.blue);
        }
    }
}
