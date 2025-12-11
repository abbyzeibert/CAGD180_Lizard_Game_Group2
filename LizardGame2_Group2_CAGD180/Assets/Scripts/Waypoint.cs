using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Abby Zeibert
 * 11/19/2025
 * Holds waypoint info
 */

public enum Type { Run, Climb, Leap };

public class Waypoint : MonoBehaviour
{
    /// <summary>
    /// Lizard's state for the next section
    /// </summary>
    public Type waypointType;

    /// <summary>
    /// Index of the next waypoint in scene's Waypoint Manager
    /// </summary>
    public int nextPoint;

    /// <summary>
    /// Point lizard moves to, comes from game object position, not inputted values
    /// </summary>
    public Vector3 toMove;

    public void Start()
    {
        //sets move position from current position
        toMove = transform.position;
    }

    /// <summary>
    /// Returns the position of the waypoint
    /// </summary>
    /// <returns> Vector3 position of this waypoint </returns>
    public Vector3 GetPoint()
    {
        return toMove;
    }
    
}
