using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData))]
public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    public string jumping_param;
    public string speed_param;
    public string risiing_param;
    public string falling_param;

    public PlayerData playerdata;

    void Start()
    {
        playerdata = GetComponent<Movement>().playerData;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(speed_param, playerdata.speed);
        animator.SetBool(jumping_param, playerdata.jump);
        animator.SetBool(risiing_param, playerdata.rising);
        animator.SetBool(falling_param, playerdata.falling);
    }
}
