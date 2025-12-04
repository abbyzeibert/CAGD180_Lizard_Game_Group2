using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/*
 * Kafka Suenishi
 * calculates units climbed and moves lizard up
 * 12/02/25
 */

public class ClimbBarScript : MonoBehaviour
{


    public GameObject lizard;
    public int unitsClimbed;
   


    /// <summary>
    /// Calculates units moved depending on which color player released bar on 
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (other.gameObject.tag == "green")
            {
                lizard.transform.position += Vector3.up * 5 * Time.deltaTime;
                unitsClimbed += 5;
            }
            else if (other.gameObject.tag == "yellow")
            {
                lizard.transform.position += Vector3.up * 3 * Time.deltaTime;
                unitsClimbed += 3;
            }
            else if (other.gameObject.tag == "red")
            {
                lizard.transform.position += Vector3.up * 1 * Time.deltaTime;
                unitsClimbed += 1;
            }
        }
    }


    void LizardWiggle()
    {
        //lizard.transform.Rotate(30 * direction, 0, 0);
        // farthest value: 16
        //closest value: -16
    }


}
