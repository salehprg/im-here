using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Transform background;
    public Vector2 start;
    public Vector2 end;
    public float speed;

    float length;
    void Start()
    {
        length = end.x - start.x;
    }

    // Update is called once per frame
    void Update()
    {
        float mappedvalue = Map(transform.position.x , start.x , end.x , 0 , 6);
        // Debug.Log(mappedvalue);

        // background.localPosition = new Vector3(mappedvalue-3 , background.localPosition.y , background.localPosition.z);

        background.gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2((mappedvalue) * speed , 0);
    }

    public float Map (float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
