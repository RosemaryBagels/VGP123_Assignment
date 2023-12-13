using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemySnail : Enemy
{
    Rigidbody2D rb;
    public float xSpeed;
    public int damage;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xSpeed <= 0)
            xSpeed = 0.5f;

        if (damage <= 0) damage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name == "Walk")
        {
            rb.velocity = sr.flipX ? new Vector2(-xSpeed, rb.velocity.y) : new Vector2(xSpeed, rb.velocity.y);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }

        if (collision.tag == "Player")
        {
            //GetComponent<PlayerController>().TakeDamage(damage);
            //... so are we doin health in the game manager orrr.....
        }

    }

}
