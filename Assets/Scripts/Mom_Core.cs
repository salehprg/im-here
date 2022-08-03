using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mom_Core : MonoBehaviour
{
    public Transform baby;
    public UnityEvent winEvent;
    public UnityEvent gameoverEvent;
    public UnityEvent jumpOnBreakableEvent;

    int jumpOnBreakable;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyCheck(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Vector2 normal = other.contacts[0].normal;

            if(Vector2.Dot(normal , Vector2.up) > 0.5f)
            {
                other.gameObject.GetComponent<Enemy>().damage(1);
                GetComponent<Movement>().doJump(5);
            }
            else
            {
                gameoverEvent.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        EnemyCheck(other);

        if(other.gameObject.tag == "BreakWood")
        {   
            jumpOnBreakableEvent.Invoke();
            
            Destroy(other.gameObject.transform.GetChild(Random.Range(0 , other.transform.childCount)).gameObject);

            jumpOnBreakable++;
            if(jumpOnBreakable >= 3)
            {
                Destroy(other.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            gameoverEvent.Invoke();
        }

        if(other.gameObject.tag == "Deadzone")
        {
            gameoverEvent.Invoke();
        }

        if(other.gameObject.tag == "ChildFollow")
        {
            baby.GetComponent<Follow>().enabled = true;
            baby.GetComponent<AudioSource>().Stop();

            winEvent.Invoke();
        }
    }
}
