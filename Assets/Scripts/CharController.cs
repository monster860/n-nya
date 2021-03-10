using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    Animator animator;
    public SpriteRenderer[] flip_entries;
    float blinkTimer = 5.0f;
    public float walkDir = 0.0f;
    public float walkSpeed = 5.0f;
    public float jumpSpeed = 5.0f;
    Rigidbody2D rigidbody2d;
    public BoxCollider2D floorSense;
    public bool faceRight = false;

    public Pickupable heldObject;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        blinkTimer -= Time.deltaTime;
        if(blinkTimer < 0) {
            animator.SetTrigger("Blink");
            blinkTimer = Random.Range(4.0f, 6.0f);
        }
        if(Mathf.Abs(walkDir) > 0.1) {
            faceRight = walkDir > 0;
            animator.SetInteger("Walking", 1);
        } else {
            animator.SetInteger("Walking", 0);
        }
        if(flip_entries.Length > 0 && faceRight != flip_entries[0].flipX) {
            foreach(SpriteRenderer renderer in flip_entries) {
                if(renderer.gameObject != gameObject) {
                    if(renderer.flipX != faceRight) {
                        Vector3 pos = renderer.transform.localPosition;
                        pos.x = -pos.x;
                        renderer.transform.localPosition = pos;
                    }
                }
                renderer.flipX = faceRight;
            }
        }
    }

    Collider2D[] touchingCollidersArray = new Collider2D[128];

    public bool TouchingFloor()
    {
        int amount = floorSense.GetContacts(touchingCollidersArray);
        for(int i = 0; i < amount; i++) {
            Collider2D collider = touchingCollidersArray[i];
            if(!collider.isTrigger) return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        Vector2 vel = rigidbody2d.velocity;
        float targetVelX = walkDir * walkSpeed;
        if(TouchingFloor()) {
            rigidbody2d.gravityScale = Mathf.Abs(walkDir);
            vel.x = targetVelX;
            vel.y = Mathf.Max(vel.y, -Mathf.Abs(vel.x));
            rigidbody2d.velocity = vel;
        } else {
            rigidbody2d.gravityScale = 1;
            rigidbody2d.AddForce(Vector3.right * Mathf.Sign(targetVelX - vel.x) * 10.0f);
        }
    }

    public void Jump()
    {
        Vector2 vel = rigidbody2d.velocity;
        if(TouchingFloor()) {
            vel.y = jumpSpeed;
            rigidbody2d.velocity = vel;
        }
    }
}
