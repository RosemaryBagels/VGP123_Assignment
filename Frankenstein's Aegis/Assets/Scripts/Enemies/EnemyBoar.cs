using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyBoar : Enemy
{
    Rigidbody2D rb;
    public float xSpeed;
    AudioSourceManager asm;

    public AudioClip boarHitSound;
    public AudioClip boarDeathSound;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        asm = GetComponent<AudioSourceManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (!asm) Debug.Log("yer pig needs an audiosource manager");

        if (xSpeed <= 0)
            xSpeed = 2;

        OnTakeDamage += PlayHitSound;
        OnDeath += PlayDeathSound;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name == "Walk")
        {
            rb.velocity = !sr.flipX ? new Vector2(-xSpeed, rb.velocity.y) : new Vector2(xSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.health--;
        }
    }

    void PlayHitSound()
    {
        asm.PlayOneShot(boarHitSound, false);
    }

    void PlayDeathSound()
    {
        asm.PlayOneShot(boarDeathSound, false);
        Debug.Log("Death Sound Called");
    }
}
