using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPlant : BasePlant
{
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
                DestroyAllEnemies();
                GameManager.Corruption -= corruptionDecrease;
                moneyTimer = 0;
            }

            yield return new WaitForSeconds(1);
        }
    }

    void DestroyAllEnemies()
    {
        foreach (GameObject enemy in GridManager.Enemies)
        {
            enemy.GetComponent<Enemy>().DestroyEnemy();
        }
        foreach (GameObject enemy in GridManager.BigEnemies)
        {
            enemy.GetComponent<Enemy>().DestroyEnemy();
        }

    }
}
