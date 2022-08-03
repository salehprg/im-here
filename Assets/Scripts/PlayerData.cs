using System.Linq;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Movement")]
    public float maxspeed;
    public float speed;
    
    [Header("Jump")]
    
    public float jumpHeight;
    public float jumpcut_multiplier;
    public bool falling , rising , climbing;
    public bool jump;
}