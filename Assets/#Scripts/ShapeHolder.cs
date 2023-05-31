using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeType
{
    L,
    O,
    I,
    W,
    Z
}

[Serializable]
public struct ShapeTypeObject
{
    public ShapeType type;
    public GameObject shapeObject;
}

public class ShapeHolder : MonoBehaviour
{
    public int team = 0;

    [SerializeField] private bool isOpponent;
    private Transform[] spawnPositions = new Transform[3];

    [SerializeField] private List<ShapeTypeObject> shapeObjects;

    private Shape[] shapes = new Shape[3];
    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            spawnPositions[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        team = GameManager.instance.team;
        if (isOpponent) team = team == 0 ? 1 : 0;
        
    }

    private void OnEnable()
    {
        EventManager.instance.OnShapeSpawnedInHolder += SpawnShape;
    }

    private void OnDisable()
    {
        EventManager.instance.OnShapeSpawnedInHolder -= SpawnShape;
    }


    public Shape GetShape(int _index)
    {
        return shapes[_index];
    }

    public bool HolderHasShape()
    {
        var shapes = GetComponentsInChildren<Shape>();
        Debug.LogError(shapes.Length);
        return shapes.Length > 1;
    }
    public void SpawnShape(int _team, int _index, ShapeType _type, int _rotation)
    {
        if (_team != team) return;
        
        for (int i = 0; i < shapeObjects.Count; i++)
        {
            ShapeTypeObject currentShape = shapeObjects[i];
            if (currentShape.type == _type)
            {
                Shape shape = Instantiate(currentShape.shapeObject, spawnPositions[_index]).GetComponent<Shape>();
                shapes[_index] = shape;
                shape.transform.localPosition = Vector3.zero;
                
                shape.Initialize(team,_rotation,!isOpponent);
            }
        }
    }
    
}
