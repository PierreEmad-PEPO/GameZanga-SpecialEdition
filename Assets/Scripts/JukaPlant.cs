using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JukaPlant : BasePlant
{
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
                DestroyClossstEnemy();
                moneyTimer = 0;
            }

            yield return new WaitForSeconds(1);
        }
    }

    void DestroyClossstEnemy() 
    {
        float minDis = 10000;
        GameObject closeEnemy = null;

        foreach (GameObject enemy in GridManager.BigEnemies)
        {
            float dis = Vector3.Distance(transform.position, enemy.transform.position);
            if (dis < minDis) 
            {
                dis = minDis;
                closeEnemy = enemy;
            }
        }

        if (closeEnemy != null && !closeEnemy.IsDestroyed()) 
        {
           closeEnemy.GetComponent<Enemy>().DestroyEnemy();
        }

    }

}
