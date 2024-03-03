using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Vector2 startPos;
    protected Vector2 endPos;
    protected float elapsedDistance = 0;
    protected BasePlant targetPlant = null;
    protected GameObject targetGameObject = null;
    protected float speed = 2f;

    protected bool willDestroy = false;
    protected bool once = true;

    protected bool reached = false;



    protected void Start()
    {
        startPos = GenratPointOutsideScreen();
        transform.position = startPos;

        endPos = GetRandomPlantPos();

    }

    protected void Update()
    {
        Vector2 pos = Vector2.Lerp(startPos, endPos, elapsedDistance);
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * speed); ;
        //elapsedDistance += speed * Time.deltaTime;

        if (Vector2.Distance(transform.position,endPos) <= .1f)
        {
            if(willDestroy)
            {
                DestroyEnemy();
            }
            else if (targetGameObject != null && once) 
            {
                reached = true;
                targetPlant.CorruptionCount ++;
                once = false;
            }
            else if (targetGameObject.IsDestroyed())
            {
                startPos = transform.position;
                endPos = GetRandomPlantPos();
                elapsedDistance = 0;
            }
        }
        
    }

    protected virtual void OnMouseDown()
    {
        DestroyEnemy();
    }

    protected Vector3 GenratPointOutsideScreen()
    {
        Vector3 pos = Vector3.zero;
        pos.z = transform.position.z;
        int randomSide = Random.Range(0, 4);

        switch(randomSide)
        {
            case 0: // up
                pos.y = ScreenUtils.ScreenTop + 1f;
                pos.x = Random.Range(ScreenUtils.ScreenLeft , ScreenUtils.ScreenRight);
                break;
            case 1: // down
                pos.y = ScreenUtils.ScreenBottom - 1f;
                pos.x = Random.Range(ScreenUtils.ScreenLeft , ScreenUtils.ScreenRight);
                break;
            case 2: // right
                pos.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                pos.x = ScreenUtils.ScreenRight + 1;
                break;
            case 3: // Left
                pos.y = Random.Range(ScreenUtils.ScreenBottom , ScreenUtils.ScreenTop);
                pos.x = ScreenUtils.ScreenLeft - 1;
                break;
        }

        return pos;
    }

    protected Vector3 GetRandomPlantPos ()
    {
        reached = false;
        Vector3 pos = GenratPointOutsideScreen();
        List<GameObject> plants = new List<GameObject>();
        foreach(var list in GridManager.Grid)
        {
           foreach(var plant in list) 
           {
                int chikdNumber = plant.transform.childCount;
                if (chikdNumber > 0) 
                {
                    plants.Add(plant.gameObject.transform.GetChild(0).transform.gameObject);
                }
           }
        }
        if (plants.Count > 0)
        {
            int rnadomIndex = Random.Range(0, plants.Count);
            plants[rnadomIndex].gameObject.TryGetComponent<BasePlant>(out targetPlant);
            targetGameObject = plants[rnadomIndex];
            if (targetPlant == null)
            {
                willDestroy = true;
                return pos;
            }
            pos = plants[rnadomIndex].transform.position;
            once = true;
        }
        else
            willDestroy = true;
        transform.LookAt(pos);
        Quaternion rotat = transform.rotation;
        rotat.x = 0;
        rotat.y = 0;
        transform.rotation = rotat;
        return pos;
    }

    public virtual void DestroyEnemy()
    {
        if (targetPlant != null && reached) 
        {
            targetPlant.CorruptionCount--;
        }
        GridManager.Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
