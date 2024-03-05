using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBig : Enemy
{
    public override void DestroyEnemy()
    {
        GridManager.BigEnemies.Remove(gameObject);
        Destroy(gameObject);
    }
    protected override void OnMouseDown ()
    {

    }
}
