using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbucaPlant : BasePlant
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

            Debug.Log(health);

            if (moneyTimer >= moneyDuration)
            {
                GameManager.Money += money;
                GameManager.Corruption -= corruptionDecrease;
                moneyTimer = 0;
            }

            yield return new WaitForSeconds(1);
        }
    }

    void GiveHimSeed()
    {
        Debug.Log("green seed");
    }
}
