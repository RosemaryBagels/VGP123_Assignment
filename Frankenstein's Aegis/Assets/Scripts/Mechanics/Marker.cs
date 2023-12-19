using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.Timeline;

public class Marker : MonoBehaviour
{
    public float delay = -1.0f;
    public GameObject prefabSpawned;
    
    // Start is called before the first frame update
    void Start()
    {
        if (delay < 0) delay = 0.8f;
        if (!prefabSpawned) Debug.Log("Marker is missing a spawn");

        StartCoroutine(DelayedAtSpawn());
    }
    IEnumerator DelayedAtSpawn()
    {
        // Delay for the specified duration
        yield return new WaitForSeconds(delay);

        // Call the AtSpawn method after the delay
        AtSpawn();
    }

    void AtSpawn()
    {
        string uniqueIdentifier = gameObject.GetInstanceID().ToString();

        GameObject instantiatedObject = Instantiate(prefabSpawned, transform.position, Quaternion.identity);
        instantiatedObject.name = "Object_" + uniqueIdentifier;
        //Instantiate(prefabSpawned);
        //prefabSpawned.transform.position = transform.position;
        Debug.Log("Called Destroy on " + gameObject.name);
        Destroy(gameObject, 5);
    }
}

