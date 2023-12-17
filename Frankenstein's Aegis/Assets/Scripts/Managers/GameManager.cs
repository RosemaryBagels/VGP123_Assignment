using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    static GameManager instance = null;

    public static GameManager Instance => instance;

    public PlayerController playerPrefab;
    //public HealthDisplay healthPrefab;

    [HideInInspector] public PlayerController playerInstance;
    [HideInInspector] public Transform spawnPoint;
    //[HideInInspector] public HealthDisplay healthCard;

    public UnityEvent<int> OnHealthValueChanged;

    private int _health = 6;
    public int maxHealth = 6;
    public int health
    {
        get { return _health; }
        set
        {
            if (_health > value)
                playerInstance.TakeDamage();

                _health = value;

            if (_health > maxHealth)
                _health = maxHealth;

            if (_health <= 0)
                //game over
                Debug.Log("Game Over");

            Debug.Log("Health has been set to: " + _health.ToString());
            OnHealthValueChanged?.Invoke(_health); //pings live text to update

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //check for press esc - pause???
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        spawnPoint = spawnLocation;
        //healthCard = Instantiate(healthPrefab);
    }

    public void UpdateSpawnPoint(Transform updatedPoint)
    {
        spawnPoint = updatedPoint;
    }
}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}