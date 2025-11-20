using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Run, Climb, Leap };

public class Waypoint : MonoBehaviour
{

    public Type waypointType;
    public int nextPoint;
    public Vector3 toMove;

    public void Start()
    {
        toMove = transform.position;
    }

    public Vector3 GetPoint()
    {
        return toMove;
    }
    
}
