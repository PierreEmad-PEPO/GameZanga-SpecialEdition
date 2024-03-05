using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    protected Vector2 startPos;
    protected Vector2 endPos;
    protected GameObject targetPlant = null;
    protected float speed = 2f;

    bool willDestroy = false;
    bool isReached = false;

    public GameObject TargetPlant { get { return targetPlant; } }

    protected void Start()
    {
        startPos = GenratPointOutsideScreen();
        transform.position = startPos;
        endPos = GetRandomPlantPos();

    }

    protected void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
        if (Vector2.Distance(transform.position, endPos) <= .05f)
            isReached = true;

        if (willDestroy && isReached)
            Destroy(targetPlant);

        if ( isReached && (targetPlant == null || targetPlant.IsDestroyed()))
        {
            endPos = GetRandomPlantPos();
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
        isReached = false;
        Vector3 dir;
        float angle;
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
            if (plants[rnadomIndex].CompareTag("Plant"))
            {
                targetPlant = plants[rnadomIndex];
                pos = plants[rnadomIndex].transform.position;
                dir = pos - transform.position;
                angle = Vector3.Angle(dir,Vector2.right);
                if (dir.y < 0)
                    angle = 360 - angle;
                transform.rotation = Quaternion.Euler (0, 0, angle);

                return pos;
            }
        }
        willDestroy = true;
        dir = pos - transform.position;
        angle = Vector3.Angle(dir, Vector2.right);
        if (dir.y < 0)
            angle = 360 - angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        return pos;
    }

    public virtual void DestroyEnemy()
    {
        GridManager.Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
