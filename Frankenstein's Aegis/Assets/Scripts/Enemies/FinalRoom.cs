using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    public GameObject boarSpawn;
    public GameObject snailSpawn;
    public GameObject victoryHeart;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!boarSpawn) Debug.Log("Boss Rush needs boars");
        if (!snailSpawn) Debug.Log("Boss Rush needs snails");
        if (!victoryHeart) Debug.Log("Boss Rush needs a winstate");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(boarSpawn, new Vector3(4.0f, 1.0f, 0), Quaternion.identity);
            Instantiate(boarSpawn, new Vector3(-4.0f, 1.0f, 0), Quaternion.identity);
            Instantiate(snailSpawn, new Vector3(-7.0f, -3.0f, 0), Quaternion.identity);
            Instantiate(snailSpawn, new Vector3(7.0f, -3.0f, 0), Quaternion.identity);
            victoryHeart.SetActive(true);
            Destroy(gameObject, 1);
        }
    }
}
