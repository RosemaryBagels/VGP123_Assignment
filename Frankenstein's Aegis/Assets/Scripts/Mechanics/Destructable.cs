using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int health;
    AudioSourceManager asm;
    public GameObject marker;

    public AudioClip boxHitSound;
    public AudioClip shatterSound;
    
    // Start is called before the first frame update
    void Start()
    {
        asm = GetComponent<AudioSourceManager>();
        if (!asm) Debug.Log("yer box needs an audiosource manager");
        if (!marker) Debug.Log("yer box needs a marker attached");

        if (health <= 0) health = 15;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        asm.PlayOneShot(boxHitSound, false);

        if (health <= 0)
        {
            PlayShatter();
            string uniqueIdentifier = gameObject.GetInstanceID().ToString();

            GameObject instantiatedMarker = Instantiate(marker, transform.position, Quaternion.identity);
            instantiatedMarker.name = "Marker_" + uniqueIdentifier;
            Destroy(gameObject, 0.6f);
        }        
    }

    public void PlayShatter()
    {
        asm.PlayOneShot(shatterSound, false);
        Debug.Log("Shatter played");
    }
}
