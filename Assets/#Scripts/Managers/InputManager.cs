using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // public static InputManager instance;
    //
    // private void Awake()
    // {
    //     if (!instance) instance = this;
    // }
    //
    // [SerializeField] private LayerMask layerMask;
    // private int team = 0;
    //
    // private bool shapeSelected = false;
    //
    // public void OnPointerClick(Shape shape)
    // {
    //     EventManager.instance.ShapeInteraction(0,shape);
    //     shapeSelected = true;
    // }
    //
    // private void Update()
    // {
    //     if (Input.touchCount < 1) return;
    //     
    //     Touch touch = Input.GetTouch(0);
    //     
    //     if(touch.phase == TouchPhase.Ended && shapeSelected)
    //     {
    //         shapeSelected = false;
    //         EventManager.instance.EndShapeDragging();
    //     }
    //     else if(shapeSelected)
    //     {
    //         Debug.Log("input dragging");
    //         EventManager.instance.ShapeDragging(touch.position);
    //     }
    // }
}