using CustomMath;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float distanceABProprio;
    float distanceABUnity;
    public Vector3 objeto2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (distanceABProprio != DistanceAutoMade(transform.position, objeto2))
        {
            distanceABProprio = DistanceAutoMade(transform.position, objeto2);
            print("Proprio = " + distanceABProprio);
        }

        if (distanceABUnity != Vector3.Distance(transform.position, objeto2))
        {
            distanceABUnity = Vector3.Distance(transform.position, objeto2);
            print("Unity = " + distanceABUnity);
        }
    }

    public static float DistanceAutoMade(Vector3 a, Vector3 b)
    {
        //pitagoras 
        float distance = MathF.Sqrt(MathF.Pow((a.x - b.x), 2) + MathF.Pow((a.y - b.y), 2) + MathF.Pow((a.z - b.z), 2));
        return distance;
    }
}
