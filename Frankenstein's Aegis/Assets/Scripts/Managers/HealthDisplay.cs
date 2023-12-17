using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public GameObject heartPrefab;
    int maxHealth = 6; //GameManager.Instance.maxHealth; hard coded BAD
    int curHealth = 6;
    List<Heart> hearts = new List<Heart>();

    private void Start()
    {
        //Debug.Log("Start method called");
        curHealth = GameManager.Instance.health;
        DrawHearts();
        GameManager.Instance.OnHealthValueChanged.AddListener((value) => UpdateCurHealth(value));        
    }

    void UpdateCurHealth(int value)
    {
        curHealth = value;
        Debug.Log("Health is " + curHealth.ToString());
        DrawHearts();
    }

    public void DrawHearts()
    {
        //Debug.Log("DrawHearts method called");

        ClearHearts();

        int healthRemainder = maxHealth % 2;
        int heartsToMake = (maxHealth/2) + healthRemainder;

        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
            //Debug.Log("Heart Made");
        }

        for (int i = 0; i < hearts.Count; i++) 
        {
            int heartStatusRemainder = Mathf.Clamp(curHealth - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        Heart heartComponent = newHeart.GetComponent<Heart>();
        heartComponent.SetHeartImage(HeartStatus.Full);
        hearts.Add(heartComponent);
    }
    
    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        hearts = new List<Heart> ();
    }
}
