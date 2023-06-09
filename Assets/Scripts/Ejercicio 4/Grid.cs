using CustomMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private bool active = false;
    [SerializeField] private float gridBallSize = 0.5f;
    public static float delta = 1f;
    public static int gridSize = 11;
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
                        Gizmos.DrawWireSphere(new Vec3(x, y, z) * delta, gridBallSize);
                    }
                }
            }
        }
    }
}
