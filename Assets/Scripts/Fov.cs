using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Fov : MonoBehaviour
{
    public LayerMask obstacleMask;
    public float viewRadius;
    public float viewAngle;

    public Vector2 DirFromAngle(float angleDeg, bool global)
    {
        if (!global)
        {
            angleDeg += transform.eulerAngles.z;
        }

        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
    }
}
