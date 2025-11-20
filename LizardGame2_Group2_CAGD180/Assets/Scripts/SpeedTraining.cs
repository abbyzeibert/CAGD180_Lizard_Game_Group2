using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTraining : MonoBehaviour
{
    public int clickAmount = 0;
    public int speedToAdd = 0;
    public bool isTrainingMoveing = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpaceButtonClickCount();
        if (clickAmount > 100)
        {
            speedToAdd = 3;
        }
        else if (clickAmount > 50)
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
