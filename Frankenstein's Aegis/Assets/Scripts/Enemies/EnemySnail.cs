using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemySnail : Enemy
{
    Rigidbody2D rb;
    AudioSourceManager asm;
    public float xSpeed;
    public float speed1;
    public float speed2;
    public int damage;
    public float range;
    public GameObject marker;
    public AudioClip snailHitSound;
    public AudioClip snailDeathSound;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        asm = GetComponent<AudioSourceManager>();

        if (!asm) Debug.Log("yer snail needs an audiosource manager");
        if (!marker) Debug.Log("yer snail needs its shell attached");

        if (xSpeed <= 0) xSpeed = 0.2f;
        if (speed1 <= 0) speed1 = 0.2f;
        if (speed2 <= 0) speed2 = 3.0f;
        if (damage <= 0) damage = 2;
        if (range <= 0) range = 3.0f;

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

        if (curPlayingClips[0].clip.name == "Idle")
        {
            rb.velocity = !sr.flipX ? new Vector2(-xSpeed, rb.velocity.y) : new Vector2(xSpeed, rb.velocity.y);

        }

        float distance = Vector3.Distance(GameManager.Instance.playerInstance.transform.position, transform.position);

        if (distance <= range)
        {
            anim.SetBool("isInRange", true);
            xSpeed = speed2;
        }
        else
        {
            anim.SetBool("isInRange", false);
            xSpeed = speed1;
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
            GameManager.Instance.health -= damage;
        }

        if (collision.gameObject.tag == "Destructable")
        {
            sr.flipX = !sr.flipX;
        }
    }

    void PlayHitSound()
    {
        anim.SetTrigger("Damage");
        asm.PlayOneShot(snailHitSound, false);
    }

    void PlayDeathSound()
    {
        asm.PlayOneShot(snailDeathSound, false);
        anim.SetTrigger("Death");
        Debug.Log("Death Sound Called");
        string uniqueIdentifier = gameObject.GetInstanceID().ToString();

        GameObject instantiatedMarker = Instantiate(marker, transform.position, Quaternion.identity);
        instantiatedMarker.name = "Marker_" + uniqueIdentifier;
    }
}
