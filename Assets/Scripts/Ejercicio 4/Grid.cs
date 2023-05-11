using CustomMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private float delta = 0.2f;
    [SerializeField] private bool active = false;
    [SerializeField] private static int gridSize = 11;
    [SerializeField] private float gridBallSize = 0.5f;
    public static Vec3[,,] grid = new Vec3[gridSize, gridSize, gridSize];

    void Start()
    {
        active = true;
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int z = 0; z < grid.GetLength(2); z++)
                {
                    grid[x, y, z] = new Vec3(x, y, z) * delta;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (active)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    for (int z = 0; z < grid.GetLength(2); z++)
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawSphere(new Vec3(x, y, z) * delta, gridBallSize);
                    }
                }
            }
        }
    }
}
