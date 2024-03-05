using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurifierPlant : BasePlant
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
                GameManager.Money += money;
                GameManager.Corruption -= corruptionDecrease;

                moneyTimer = 0;
            }

            yield return new WaitForSeconds(1);
        }
    }
}
