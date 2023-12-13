using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    //Component Refernces
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    //Movement variables
    public float speed = 6.0f;
    public float jumpForce = 350.0f;

    //Groundcheck stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;

    public static bool isFiring;
    public static bool isFlipped;
    GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        //getting our component references
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //checking for dirty data
        if (rb == null) Debug.Log("No RigidBody Reference");
        if (sr == null) Debug.Log("No Sprite Renderer Reference");
        if (anim == null) Debug.Log("No Animator Reference");

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("groundcheck set to default value");
        }

        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("speed set to default value");
        }
        if (jumpForce <= 0)
        {
            jumpForce = 350.0f;
            Debug.Log("jumpforce set to default value");
        }

        if (groundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }
        weapon = transform.GetChild(0).gameObject;
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        if (isGrounded) rb.gravityScale = 1;

        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "fire")
                rb.velocity = Vector2.zero;            
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        anim.SetFloat("hInput", Mathf.Abs(hInput)); //Mathf.Abs does absolute value
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFiring", isFiring);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (isGrounded && Input.GetButton("Fire1"))
        {
            isFiring = true;
            weapon.SetActive(true);
        }
        else
        {
            isFiring = false;
            weapon.SetActive(false);
        }

        if (hInput != 0)
        {
            isFlipped = (hInput < 0);
            sr.flipX = isFlipped;
        }
    }
}