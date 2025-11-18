using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Run, Climp, Leap };

public class Waypoint : MonoBehaviour
{

    public Type waypointType;
    private Vector3 toMove;

    public void Start()
    {
        toMove = transform.position;
    }

    public Vector3 GetPoint()
    {
        return toMove;
    }
    
}
