using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlant : BasePlant
{
    [SerializeField]
    private int totalColliction;
    private static int objectNumber = 0;

    protected override void Start()
    {
        base.Start();
        objectNumber++;

        if (objectNumber == totalColliction)
        {
            GiveHimSeed();
        }
    }
    public override IEnumerator StartAbility()
    {
        while (true)
        {
            if (isCorruption)
            {
                health -= damagePreSecond;
            }
            if (health <= 0)
                Destroy(gameObject);
            moneyTimer++;

            if (moneyTimer >= moneyDuration)
            {
                Debug.Log(money + "  " + corruptionDecrease);

                moneyTimer = 0;
            }

            yield return new WaitForSeconds(1);
        }
    }

    void GiveHimSeed()
    {
        Debug.Log("red seed");
    }
}