using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    [SerializeField] bool isObistecal;
    private SpriteRenderer spriteRn;
    private PlantEnum plant;
    private int growth = 0;
    private int corruption = 0;
    private bool isPlanting = false;
    private bool isCorruption = false;

    public bool IsCorruption { get { return isCorruption; } set { isCorruption = value; } }

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
        isPlanting = true;
        this.plant = plant;
        growth = 0;
        corruption = 0;
        // reduce Money;
    }

    public void harvest ()
    {
        // put itme in invrntory
        // add Money
       ChangeToEmpty();
    }

    public void click(PlantEnum plant)
    {
        if (growth >= 99)
        {
            harvest();
        }
        else if (!isPlanting)
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
    void UpdateData()
    {
        if (isCorruption && isPlanting && corruption <= 90)
        {
            corruption += 10;
            if (corruption > 99)
                corruption = 99;
            Debug.Log(corruption);
        }
        if (corruption >= 99)
        {
            ChangeToEmpty();
        }
        else if (isPlanting && growth <= 90)
        {
            growth += 10;
        }
        if (isPlanting)
        {
            int growthLevel = (growth * (GridManager.DimensionLenthe - 1 ) / 100);
            int corruptionLevel = (corruption * (GridManager.DimensionLenthe ) / 100);
            spriteRn.sprite = GridManager.GetPlantSprite(plant, growthLevel, corruptionLevel);
        }

    }
    IEnumerator UpdateCell()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1);
            UpdateData();
        }
    }
}
