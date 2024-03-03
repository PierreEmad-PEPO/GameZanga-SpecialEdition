using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    float elapsedDistance = 0;
    BasePlant targetPlant = null;
    GameObject targetGameObject = null;
    float speed = 2f;

    int rangeX = 10;
    int rangeY = 5;
    bool willDestroy = false;
    bool once = true;

    float screenWidth;
    float screenHeight;

    private void Start()
    {
        Initialize();
        startPos = GenratPointOutsideScreen();
        transform.position = startPos;

        endPos = GetRandomPlantPos();

    }

    private void Update()
    {
        Vector2 pos = Vector2.Lerp(startPos, endPos, elapsedDistance);
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * speed); ;
        //elapsedDistance += speed * Time.deltaTime;

        if (Vector2.Distance(transform.position,endPos) <= .05f)
        {
            if(willDestroy)
            {
                DestroyEnemy();
            }
            else if (targetGameObject != null && once) 
            {
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

    private void OnMouseDown()
    {
        DestroyEnemy();
    }

    Vector3 GenratPointOutsideScreen()
    {
        Vector3 pos = Vector3.zero;
        pos.z = transform.position.z;
        int randomSide = Random.Range(0, 4);

        switch(randomSide)
        {
            case 0: // up
                pos.y = rangeY;
                pos.x = Random.Range(-rangeX, rangeX);
                break;
            case 1: // down
                pos.y = -rangeY;
                pos.x = Random.Range(-rangeX, rangeX);
                break;
            case 2: // right
                pos.y = Random.Range(-rangeY,rangeY);
                pos.x = rangeX;
                break;
            case 3: // Left
                pos.y = Random.Range(-rangeY, rangeY);
                pos.x = -rangeX;
                break;
        }

        return pos;
    }

    Vector3 GetRandomPlantPos ()
    {
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

    private void DestroyEnemy()
    {
        if (targetPlant != null) 
        {
            targetPlant.CorruptionCount--;
        }
        Destroy(gameObject);
    }

    public void Initialize()
    {
        rangeX = Screen.width;
        rangeY = Screen.height;

        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(
            rangeX, rangeY, screenZ);
        Vector3 upperRightCornerWorld =
            Camera.main.ScreenToWorldPoint(upperRightCornerScreen);

        rangeX = (int)(upperRightCornerScreen.x );
        rangeY = (int)(upperRightCornerScreen.y );

    }
}
