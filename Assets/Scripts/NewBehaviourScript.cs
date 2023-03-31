using CustomMath;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float magP;
    float sqrtMagP;

    float magU;
    float sqrtMagU;

    Vec3 posision;

    void Start()
    {
        posision.x = transform.position.x;
        posision.y = transform.position.y;
        posision.z = transform.position.z;

        magP = Vec3.Magnitude(posision);
        sqrtMagP = Vec3.SqrMagnitude(posision);

        magU = Vector3.Magnitude(transform.position);
        sqrtMagU = Vector3.SqrMagnitude(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        print("Magnitud P = " + magP);
        print("SqrtMagnitud P = " + sqrtMagP);

        print("Magnitud U = " + magU);
        print("SqrtMagnitud U = " + sqrtMagU);
    }
}
