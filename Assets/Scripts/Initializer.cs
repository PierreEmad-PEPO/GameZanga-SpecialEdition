using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] Vector2 startPoint;

    void Awake()
    {
        InputManager.Initialize(startPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
