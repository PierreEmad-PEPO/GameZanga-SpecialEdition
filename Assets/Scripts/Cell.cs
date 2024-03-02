using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    [SerializeField] bool isObstacle;

    [Range(0, 100)] private int growth = 0;
    [Range(0, 99)] private int corruption = 0;
    private int progressPerSec = 10;
    private int growthFactor = 1;
    private int corruptionFactor = 0;

    private SpriteRenderer spriteRn;

    private bool isPlanting = false;
    private PlantEnum plant;

    public bool IsObstacle{ get { return isObstacle; } }
    public bool IsPlanting {  get { return isPlanting; } }
    public int Corruption { get { return corruption; } }

    // Start is called before the first frame update
    void Start()
    {
        spriteRn = GetComponent<SpriteRenderer>();
        StartCoroutine(UpdateCell());
    }

    public void Seed (PlantEnum plant)
    {
        if (isPlanting)
            return;
        // you can or not
        isPlanting = true;
        this.plant = plant;
        corruption = 0;
        growth = 0;
        // reduce Money;
        UpdateSprite();
    }

    public void harvest ()
    {
        // put itme in invrntory
        // add Money
       ChangeToEmpty();
    }

    public void Click(PlantEnum plant)
    {
        if (growth >= 100)
        {
            harvest();
        }
        else
        {
            Seed(plant);
        }
    }

    public void ChangeToEmpty ()
    {
        spriteRn.sprite = GridManager.EmptySprite;
        isPlanting = false;
        corruption = 0;
        growth = 0;
    }

    public void StartCorruption()
    {
        corruptionFactor++;
    }

    void UpdateData()
    {
        if (isPlanting)
        {
            UpdateSprite();

            if (corruption >= 99)
                ChangeToEmpty ();

            growth += (growthFactor * progressPerSec);
            corruption += (corruptionFactor * progressPerSec);

            if (growth > 100)
                growth = 100;
            if (corruption > 99)
                corruption = 99;
        }

    }
    void UpdateSprite()
    {
    }
    IEnumerator UpdateCell()
    {
        while (true) 
        {
            UpdateData();
            yield return new WaitForSeconds(1);
        }
    }
}
