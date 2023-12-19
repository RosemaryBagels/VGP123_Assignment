using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    public GameObject bigRock;
    int count;
    public Text resourceCount;
    
    // Start is called before the first frame update
    void Start()
    {
        count = GameManager.Instance.count;
        if (count <= 0 )
        { 
            bigRock.SetActive(false);
        }
        else
        {
            bigRock.SetActive(true);
            resourceCount.text = count.ToString();
        }
        GameManager.Instance.OnResourceValueChanged.AddListener((value) => UpdateResources(value));
        //Debug.Log("count is " + count.ToString());
    }

    void UpdateResources(int value)
    {
        //Debug.Log("value is " + value.ToString());
        if (value > 0)
        {
            bigRock.SetActive(true);
            resourceCount.text = value.ToString();
        }
    }
}