using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform who;
    public float speed;
    public Vector2 offset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector2.Lerp(transform.position , new Vector2 (who.position.x + offset.x, who.position.y + offset.y), Time.deltaTime * speed);
        newPos.z = transform.position.z;
        
        transform.position = newPos;
    }
}
