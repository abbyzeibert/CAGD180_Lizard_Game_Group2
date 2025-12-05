using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaTraining : MonoBehaviour
{
    public GameObject lizard;
    public List<GameObject> flies = new List<GameObject>();

    public GameObject flyPrefab;

    public bool isMoving = false;
    public bool isExtending = false;

    public float tongueSpeed = 5f;
    public float rotateSpeed = 5f;
    public int curFly = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flies.Count > 0)
        {
            Vector3 lookPos = flies[curFly].transform.position - lizard.transform.position;
            lookPos.z = 0;
            Quaternion lookDir = Quaternion.LookRotation(lookPos, new Vector3(0, 0, -90));
            lizard.transform.rotation = Quaternion.Slerp(lizard.transform.rotation, lookDir, Time.deltaTime * 2);

            ExtendTongue();
            RetractTongue();
        }
    }

    public void SpawnFly()
    {
        flies.Add(Instantiate(flyPrefab, new Vector3(Random.Range(-8, 8), Random.Range(1, 5)), Quaternion.Euler(0,0,0)));
        if (!isMoving)
        {
            StartCoroutine(GoThroughFlies());
        }
    }

    public void ExtendTongue()
    {
        if (isExtending && isMoving)
        {
            transform.Translate(Vector3.right * tongueSpeed * Time.deltaTime);
        }
    }

    public void RetractTongue()
    {
        if (!isExtending && isMoving)
        {
            transform.Translate(Vector3.left * tongueSpeed * Time.deltaTime);
        }
    }

    public IEnumerator GoThroughFlies()
    {
    //    while(flies.Count > 0)
    //    {
    //        if(curFly > flies.Count)
    //        {
    //            flies.Clear();
    //            break;
    //        }
    //        else
    //        {
    //            isMoving = true;
    //            if (flies[curFly] != null)
    //            {
    //                isExtending = true;
    //            }
    //            else
    //            {
    //                isExtending = false;
    //                yield return new WaitForSeconds(2);
    //                isMoving = false;
    //                curFly++;
    //            }
    //        }
    //    }
    }
}
