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
                Debug.Log(money + "  " + corruptionDecrease);
                // Destory All Enemys and all Corruotion;
                moneyTimer = 0;
            }

            yield return new WaitForSeconds(1);
        }
    }
}
