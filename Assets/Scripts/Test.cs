using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        GameManager.ReadyFirePlant();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            GameManager.Instance.TryBuyPlant();
        }
    }
}
