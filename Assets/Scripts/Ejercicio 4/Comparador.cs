using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comparador : MonoBehaviour
{
    [SerializeField] private ObjectCollider[] meshes;
    [SerializeField] private Material trueCollision;
    [SerializeField] private Material falseCollision;
    private bool IsColliding;

    void Update()
    {
        IsColliding = false;
        foreach (var point in meshes[0].insidePoints)
        {
            if (meshes[1].ComparePoint(point))
            {
                Debug.Log("is colliding");
                IsColliding = true;
            }
        }

        if (IsColliding)
        {
            meshes[0].GetComponent<MeshRenderer>().material = trueCollision;
            meshes[1].GetComponent<MeshRenderer>().material = trueCollision;
        }
        else
        {
            meshes[0].GetComponent<MeshRenderer>().material = falseCollision;
            meshes[1].GetComponent<MeshRenderer>().material = falseCollision;
        }
    }
}
