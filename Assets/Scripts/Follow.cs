using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject sprite;
    public Transform following;
    public float offset_X;
    public float speed;
    public float distance;

    Vector2 destination;
    bool reach_goal;

    void Start()
    {
        reach_goal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(reach_goal)
            destination = transform.position;

        if(Vector2.Distance(transform.position , (Vector2)following.position) > distance)
        {
            if(((Vector3)destination - transform.position).x > 0)
            {
                sprite.transform.localScale = new Vector3(Mathf.Abs(sprite.transform.localScale.x), sprite.transform.localScale.y , sprite.transform.localScale.z);
                sprite.transform.localPosition = new Vector3(0 , sprite.transform.localPosition.y , sprite.transform.localPosition.z);
            }
            else
            {
                sprite.transform.localScale = new Vector3(-1 * Mathf.Abs(sprite.transform.localScale.x) , sprite.transform.localScale.y , sprite.transform.localScale.z);
                sprite.transform.localPosition = new Vector3(-2.5f , sprite.transform.localPosition.y , sprite.transform.localPosition.z);
            }

            destination = new Vector2(following.position.x + offset_X , transform.position.y);

            reach_goal = false;
        }
        else
        {
            if(Vector2.Distance(transform.position , destination) < 0.1f)
                reach_goal = true;
        }

        transform.position = Vector3.Lerp(transform.position , destination , Time.deltaTime * speed);
    }
}
