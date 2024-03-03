using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class InputManager
{
    static Camera mainCamera;
    static Vector2 startPoint;

    public static void Initialize(Vector2 _startPoint)
    {
        mainCamera = Camera.main;
        startPoint = _startPoint;
        CeilPoint(ref startPoint.x, ref startPoint.y);
    }

    static void CeilPoint(ref float x, ref float y)
    {
        x = Mathf.CeilToInt(x);
        y = Mathf.CeilToInt(y);
    }

    public static Vector3 GetMousePos()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 world = mainCamera.ScreenToWorldPoint(mousePos);
        world.z = mainCamera.nearClipPlane;
        return world;
    }

    public static Tuple<int,int> MouseToGrid()
    {
        Vector3 world = GetMousePos();
        CeilPoint(ref world.x, ref world.y);
        int r = (int)(world.y - startPoint.y);
        int c = (int)(world.x - startPoint.x);

        Tuple<int, int> ret = new Tuple<int, int>(r,c);
        Debug.Log(ret);
        return ret;
    }
}
