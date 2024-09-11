using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Utilities instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public float DistanceFormula(Vector2 first, Vector2 second)
    {
        return Mathf.Sqrt( Mathf.Pow(second.x-first.x, 2) * Mathf.Pow(second.y-first.y, 2) );
    }

    public float DistanceFormula(Vector3 first, Vector3 second)
    {
        return Mathf.Sqrt(Mathf.Pow(second.x - first.x, 2) + Mathf.Pow(second.y - first.y, 2));
    }

    public Vector3 SlopeFormula(Vector3 first, Vector3 second)
    {
        return new Vector3(first.x-second.x,first.y-second.y,first.z-second.z);
    }
}
