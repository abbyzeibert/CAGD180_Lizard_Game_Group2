using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Topher Overbey
 * 11/20/25
 * Controls the speed training scene's lizard and gives off the number to add onto the normal speed
*/

public class SpeedTraining : MonoBehaviour
{
    public int clickAmount = 0;
    public int speedToAdd = 0;
    public bool isTrainingMoveing = false;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToStart());
    }

    // Update is called once per frame
    void Update()
    {
        SpaceButtonClickCount();
        
        if (clickAmount > 130)
        {
            speedToAdd = 3;
        }
        else if (clickAmount > 80)
        {
            speedToAdd = 2;
        }
        else
        {
            speedToAdd = 1;
        }
    }
    public void SpaceButtonClickCount()
    {
        if (Input.GetKeyUp(KeyCode.Space) && isTrainingMoveing)
        {
            clickAmount++;
            transform.Rotate(0, 0, (30 * direction));
            direction *= -1;
        }
    }
    
    public IEnumerator WaitToStart()
    {

        yield return new WaitForSeconds(3);
        StartCoroutine(StartTraining());
    }
    public IEnumerator StartTraining()
    {
        isTrainingMoveing = true;
        yield return new WaitForSeconds(30);
        isTrainingMoveing = false;
    }
}
