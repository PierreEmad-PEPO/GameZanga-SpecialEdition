using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    List<GameObject> enemies = new List<GameObject>();
    float elapsedTime = 0;
    float duration = 3;

    public List<GameObject> Enemies { get { return enemies; } }
    // Start is called before the first frame update
    void Start()
    {
        duration = Random.Range(10,10);
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime >= duration) 
        {
            int count = Random.Range(0, 3);
            SpawnWave(count);
            duration = Random.Range(10, 10);
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
    }

    void SpawnWave(int number)
    {
        for(int i = 0; i < number; i++)
        {
            GameObject en = Instantiate(enemyPrefab);
            enemies.Add(en);
        }
    }
}
