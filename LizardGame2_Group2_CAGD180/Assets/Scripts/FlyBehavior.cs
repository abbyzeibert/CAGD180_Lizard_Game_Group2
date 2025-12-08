using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehavior : MonoBehaviour
{
    public Vector3 toMove;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(3.0f, 7.0f);

        toMove = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 5.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, toMove, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, toMove) < 0.1)
        {
            toMove = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 5.0f), 0);
        }
    }
}
