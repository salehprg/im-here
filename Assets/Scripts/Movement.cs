using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{

    public UnityEvent stopMovementEvent;
    public UnityEvent movementEvent;
    public UnityEvent jumpingEvent;
    public UnityEvent groundEvent;
    public UnityEvent risingEvent;
    public UnityEvent fallingEvent;

    public PlayerData playerData;
    public GameObject sprite;
    public Vector2 offset;
    public float groundcheck_height = 0;
    public bool CanClimb;

    Vector2 moveinput;

    int jumpcount = 0;
    bool jump , jumpcut;
    bool touchground;

    Rigidbody2D rigidbody;
    Collider2D collider;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {  
        jumpcut = false;
    }

    
    Vector2 input;
    void Update()
    {
        moveinput = new Vector2(Input.GetAxis("Horizontal") , Input.GetAxis("Vertical"));

        if(Mathf.Abs(moveinput.x) > 0.01)
        {
            collider.isTrigger = false;

            sprite.transform.localScale = new Vector3(Mathf.Sign(moveinput.x) > 0 ? Mathf.Abs(sprite.transform.localScale.x) : -1 * Mathf.Abs(sprite.transform.localScale.x) , sprite.transform.localScale.y , sprite.transform.localScale.z);
            sprite.transform.localPosition = new Vector3(moveinput.x < 0 ? -2.5f : 0 , sprite.transform.localPosition.y , sprite.transform.localPosition.z);
        }

        if(jumpcount < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            collider.isTrigger = true;

            jumpcount++;
            jump = true;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            jumpcut = true;
        }

    }

    public void doJump(float amount)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, amount);
        playerData.jump = true;
        jump = false;

        jumpingEvent.Invoke();
    }
    
    private void FixedUpdate() 
    {
        float speedX = playerData.maxspeed * moveinput.x;
		speedX = Mathf.Lerp(rigidbody.velocity.x, speedX, 1);

        rigidbody.velocity = new Vector2(speedX , rigidbody.velocity.y);
        
        playerData.speed = Mathf.Abs(speedX);

        if(jump)
        {
            doJump(playerData.jumpHeight);
        }

        if(jumpcut)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x , rigidbody.velocity.y * playerData.jumpcut_multiplier);
            jumpcut = false;
        }

        if(rigidbody.velocity.y < 0)
        {
            collider.isTrigger = false;
        }

        CheckForGround();

       
    
    }

    void CheckForGround()
    {
        Vector2 startpos = new Vector2(transform.position.x , transform.position.y) + offset;
        RaycastHit2D hitinfo = Physics2D.Linecast(startpos + offset , startpos + (Vector2.down * groundcheck_height));

        if(hitinfo.collider == null)
        {
            if(rigidbody.velocity.y < 0)
            {
                playerData.rising = false;
                playerData.falling = true;

                fallingEvent.Invoke();
            }
            else if(rigidbody.velocity.y > 0)
            {
                playerData.rising = true;
                playerData.falling = false;

                risingEvent.Invoke();
            }
        }
        else
        {
            playerData.rising = playerData.falling = false;
        }
    }

    void OnGroundCheck(Collision2D other)
    {
        ContactPoint2D contactPoint = other.contacts.Where(x => x.otherCollider.gameObject == this.gameObject).FirstOrDefault();

        if(contactPoint.collider != null)
        {
            float direction = Vector2.Dot(contactPoint.normal , Vector2.up);

            if(direction >= 0.4f)
            {
                playerData.jump = false;
                jumpcount = 0;

                groundEvent.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        OnGroundCheck(other);
    }

    private void OnCollisionStay2D(Collision2D other) {
        OnGroundCheck(other);

        if(!CanClimb)
            return;

        ContactPoint2D contactPoint = other.contacts.Where(x => x.otherCollider.gameObject == this.gameObject).FirstOrDefault();

        if(contactPoint.collider != null)
        {
            float direction = Vector2.Dot(contactPoint.normal , Vector2.up);

            if(direction >= -0.1f && direction <= 0.1f)
            {
                playerData.jump = false;
                playerData.climbing = true;
                jumpcount = 0;
            }
            else
                playerData.climbing = false;
        }
    }
}
