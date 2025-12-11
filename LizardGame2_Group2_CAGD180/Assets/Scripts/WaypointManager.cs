using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Abby Zeibert
 * 11/19/2025
 * Holds a track's waypoints in order
 */

public class WaypointManager : MonoBehaviour
{
    /// <summary>
    /// Holds the race's waypoints, index 0 will start the race.  Add waypoints in order of traversal and reference this list when assigning each waypoint's nextPoint
    /// </summary>
    public Waypoint[] levelWaypoints;

}
