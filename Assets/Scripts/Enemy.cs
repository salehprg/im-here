using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int HP;
    public UnityEvent deathEvent;
    public UnityEvent damageEvent;

    public void damage(int damageamount)
    {
        HP -= damageamount;

        damageEvent.Invoke();
        
        if(HP <= 0)
        {
            deathEvent.Invoke();
        }
    }

    public void _Destroy(float time)
    {
        Destroy(this.gameObject , time);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
