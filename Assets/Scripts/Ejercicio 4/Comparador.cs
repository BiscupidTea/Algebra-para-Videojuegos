using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comparador : MonoBehaviour
{
    [SerializeField] private ObjectCollider[] meshes;

    void Update()
    {
        foreach (var point in meshes[0].insidePoints)
        {
            if (meshes[1].ComparePoint(point))
            {
                Debug.Log("is colliding");
            }
        }
    }
}
