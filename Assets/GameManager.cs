using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject HandPrefab;
    public Transform[] SpawnPoints;

    public static GameManager Instance;
    public List<GameObject> SpawnedObjects = new List<GameObject>();


    float gameTime;
    float itemSpawnRate = 0.5f;
    float nextTimeToSpawn;



    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if (Time.time > nextTimeToSpawn)
        {
            SpawnItem();
            nextTimeToSpawn = Time.time + (1 / itemSpawnRate);
        }
    }

    void SpawnItem()
    {
        GameObject ItemClone = Instantiate(HandPrefab,GetRandomSpawnPoint().position,HandPrefab.transform.rotation);
        SpawnedObjects.Add(ItemClone);
        if(PercentChance(10))
        {
            ItemClone.GetComponent<HumanHand>().SetMitten();
            ItemClone.name = "Mitts";
        }
    }

    Transform GetRandomSpawnPoint()
    {
        return SpawnPoints[Random.Range(0, SpawnPoints.Length)];
    }

    bool PercentChance(int chance)
    {
        int rand = Random.Range(0, 101);
        return rand < chance;
    }
}
