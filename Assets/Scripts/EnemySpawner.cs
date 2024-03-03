using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject enemyBigPrefab;
    float elapsedTime = 0;
    float duration = 3;

    float elapsedTimeSpecial;
    float durationSpecial = 3;

    // Start is called before the first frame update
    void Start()
    {
        duration = Random.Range(10,10);
        durationSpecial = Random.Range(30, 120);
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime >= duration) 
        {
            
            SpawnWave();
            duration = Random.Range(10, 10);
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;

        // Special

        if (elapsedTimeSpecial >= durationSpecial) 
        {
            int randomEvent = Random.Range(1, 2);
            if (randomEvent == 0)
                CorruptionThePlant();
            else
                SpawnBigEnemiesWave();
            durationSpecial = Random.Range(30,120);
            elapsedTimeSpecial = 0;
        }
        elapsedTimeSpecial += Time.deltaTime;
    }

    void SpawnWave()
    {
        int number = Random.Range(1, 4);
        for (int i = 0; i < number; i++)
        {
            GameObject en = Instantiate(enemyPrefab);
            GridManager.Enemies.Add(en);
        }
    }


    void CorruptionThePlant()
    {
        List<GameObject> plants = new List<GameObject>();
        foreach (var list in GridManager.Grid)
        {
            foreach (var plant in list)
            {
                int chikdNumber = plant.transform.childCount;
                if (chikdNumber > 0)
                {
                    plants.Add(plant.gameObject.transform.GetChild(0).transform.gameObject);
                }
            }
        }

        if (plants.Count > 0) 
        {
            int randomIndex = Random.Range(0, plants.Count);
            plants[randomIndex].GetComponent<BasePlant>().CorruptionCount++;
        }
    }

    void SpawnBigEnemiesWave()
    {
        int randomCount = Random.Range(1, 4);
        for (int i = 0; i < randomCount; i++)
        {
            GameObject en = Instantiate(enemyBigPrefab);
            GridManager.BigEnemies.Add(en);
        }
    }
}
