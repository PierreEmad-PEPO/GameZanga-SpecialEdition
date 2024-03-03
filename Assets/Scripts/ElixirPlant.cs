using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixirPlant : BasePlant
{
    [SerializeField]
    private int totalCollection;
    private static int objectNumber = 0;

    protected override void Start()
    {
        base.Start();
        objectNumber++;

        if (objectNumber == totalCollection)
        {
            GiveHimSeed();
        }
    }
    public override IEnumerator StartAbility()
    {
        while (true)
        {
            health -= (damagePreSecond * corruptionCount);
            if (health <= 0)
                Destroy(gameObject);
            moneyTimer++;

            if (moneyTimer >= moneyDuration)
            {
                Debug.Log(money + "  " + corruptionDecrease);
                GameManager.Money += money;
                GameManager.Corruption -= corruptionDecrease;
                DestroyAllEnemies();
                moneyTimer = 0;
            }

            yield return new WaitForSeconds(1);
        }
    }

    void DestroyAllEnemies()
    {
        foreach(GameObject enemy in GridManager.Enemies) 
        {
            enemy.GetComponent<Enemy>().DestroyEnemy();
        }
        foreach (GameObject enemy in GridManager.BigEnemies)
        {
            enemy.GetComponent<Enemy>().DestroyEnemy();
        }

    }

    void GiveHimSeed()
    {
        Debug.Log("blue seed");
    }
}
