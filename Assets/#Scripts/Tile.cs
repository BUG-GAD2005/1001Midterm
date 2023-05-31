using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Vector2Int tileCoord;

    private Image activeImage;
    private Image inactiveImage;
    private Image previewImage;
    private Transform _transform;
    private void Awake()
    {
        Referances();
        CalculateTileCoord();
    }
    
    //Referance assigning
    private void Referances()
    {
        _transform = transform;
        
        //activeImage = _transform.GetChild(0).GetComponent<Image>();
        //inactiveImage = _transform.GetChild(1).GetComponent<Image>();
        //previewImage = _transform.GetChild(2).GetComponent<Image>();
    }

    //Tile calculates its place in grid
    private void CalculateTileCoord()
    {
        var childCount = transform.root.childCount;
        var childIndex = transform.GetSiblingIndex();
        
        tileCoord = new Vector2Int(
            childIndex / 10,
            childIndex % 10
        );
    }

    //Preview for dragging pieces
    public void TilePreview(bool preview)
    {
        previewImage.enabled = preview;
        inactiveImage.enabled = !preview;
    }
    
    //Changing tile activeness
    public void SetTileActive(bool active = true)
    {
        activeImage.enabled = active;
        inactiveImage.enabled = !active;
    }

    //Tile's break process
    public void DestroyTile()
    {
        //TODO SPAWN PARTICLE
        
        activeImage.enabled = false;
        inactiveImage.enabled = true;
    }
}
