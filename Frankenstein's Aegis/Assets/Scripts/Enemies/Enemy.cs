using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;

    public event Action OnTakeDamage;
    public event Action OnDeath;

    protected int health;
    public int maxHealth;

    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (maxHealth <= 0)
            maxHealth = 20;

        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject, 0.8f);
        }
        else
        { 
        OnTakeDamage?.Invoke();
        }
    }
}
