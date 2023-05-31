using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public event Action<int, int, ShapeType, int> OnShapeSpawnedInHolder;
    public void SpawnShapeOnHolder(int team, int index, ShapeType shape, int rotation)
    {
        OnShapeSpawnedInHolder?.Invoke(team, index, shape, rotation);
    }
    
    public event Action<int, int> OnShapeInteracted;
    public void ShapeInteraction(int team, int index)
    {
        OnShapeInteracted?.Invoke(team, index);
    }
    
    public event Action<int,Vector2> OnShapeDragged;
    public void ShapeDragging(int team, Vector2 pos)
    {
        OnShapeDragged?.Invoke(team,pos);
    }
    
    public event Action OnShapeDraggingEnd;
    public void EndShapeDragging()
    {
        OnShapeDraggingEnd?.Invoke();
    }
}
