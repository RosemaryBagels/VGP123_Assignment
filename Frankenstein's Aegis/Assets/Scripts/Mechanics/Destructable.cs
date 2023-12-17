using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int health;
    AudioSourceManager asm;

    public AudioClip boxHitSound;
    public AudioClip shatterSound;
    
    // Start is called before the first frame update
    void Start()
    {
        asm = GetComponent<AudioSourceManager>();
        if (!asm) Debug.Log("yer box needs an audiosource manager");

        if (health <= 0) health = 15;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        asm.PlayOneShot(boxHitSound, false);

        if (health <= 0)
        {
            PlayShatter();
            Destroy(gameObject, 0.6f);
        }

        
    }

    public void PlayShatter()
    {
        asm.PlayOneShot(shatterSound, false);
        Debug.Log("Shatter played");
    }
}
