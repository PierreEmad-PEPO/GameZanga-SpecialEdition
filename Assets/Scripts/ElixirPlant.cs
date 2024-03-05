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
                DestroyPlant();
            moneyTimer++;

            if (moneyTimer >= moneyDuration)
            {
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
        while (GridManager.Enemies.Count > 0) 
        {
            GridManager.Enemies[0].GetComponent<Enemy>().DestroyEnemy();
        }
        while (GridManager.BigEnemies.Count > 0)
        {
            GridManager.BigEnemies[0].GetComponent<Enemy>().DestroyEnemy();
        }

    }

    void GiveHimSeed()
    {
        Debug.Log("blue seed");
    }
}
