using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Environment : MonoBehaviour
{
    public UnityEvent thunderEvent;

    public float thunder_time_min;
    public float thunder_time_max;

    float lastTime;
    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float thunder_time = Random.Range(thunder_time_min , thunder_time_max);

        if(lastTime < Time.time)
        {
            thunderEvent.Invoke();
            lastTime = Time.time + thunder_time;
        }


    }
}
