using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]

public class Weapon : MonoBehaviour
{
    SpriteRenderer sr;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (sr == null) Debug.Log("No Sprite Renderer Reference - weapon");
        if (anim == null) Debug.Log("No Animator Reference - weapon");
    }

    // Update is called once per frame
    void Update()
    {
        sr.flipX = PlayerController.isFlipped;
    }
}
