using System;
using UnityEngine;

public class Placement : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    // private void Update()
    // {
    //     Vector2 mousePosition = Input.mousePosition;
    //     RaycastHit hitInfo;
    //     Ray ray = mainCamera.ScreenPointToRay(mousePosition);
    //     if (!Physics.Raycast(ray, out hitInfo))
    //     {
    //         return;
    //     }

    //     transform.position = hitInfo.point;

    // }
}
