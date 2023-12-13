using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int health;
    
    // Start is called before the first frame update
    void Start()
    {
        if (health <= 0) health = 15;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("health is " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
