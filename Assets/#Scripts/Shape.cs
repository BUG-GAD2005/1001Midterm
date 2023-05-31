using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shape : MonoBehaviour
{
    public int team;
    public bool[] pieces;
    public Color pieceColor;
    public bool isMine;

    private Dictionary<int, int[]> rotations = new Dictionary<int, int[]>()
    {
        { 90, new int[] { 2,5,8,1,4,7,0,3,6 } },
        { 180, new int[] { 8,7,6,5,4,3,2,1,0} },
        { 270, new int[] { 6,3,0,7,4,1,8,5,2 } }
    };

    private Transform _transform;
    private EventTrigger eventTrigger;

    private Vector3 offset;

    public int rotation;
    public int index;
    private void Awake()
    {
        _transform = transform;
        for (int i = 0; i < pieces.Length; i++)
        {
            Image piece = _transform.GetChild(i).GetComponent<Image>();
            pieces[i] = piece.enabled;
            piece.color = pieceColor;
        }

        index = _transform.parent.GetSiblingIndex();
    }

    private void TriggerEvent()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry beginDrag = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.BeginDrag
        };
        EventTrigger.Entry drag = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.Drag
        };
        EventTrigger.Entry endDrag = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.EndDrag
        };
        
        beginDrag.callback.AddListener(BeginDrag);
        eventTrigger.triggers.Add(beginDrag);
        
        drag.callback.AddListener(Drag);
        eventTrigger.triggers.Add(drag);
        
        endDrag.callback.AddListener(EndDrag);
        eventTrigger.triggers.Add(endDrag);
    }

    private void BeginDrag(BaseEventData eventData)
    {
        if (!TurnManager.instance.isMyTurn(team)) return;
        
        offset = transform.position - (Vector3)Input.GetTouch(0).position;
        EventManager.instance.ShapeInteraction(team,index);
    }
    
    private void Drag(BaseEventData eventData)
    {
        if (!TurnManager.instance.isMyTurn(team)) return;
        
        Vector3 touchPos = Input.GetTouch(0).position;
        
        EventManager.instance.ShapeDragging(team,   touchPos + offset);
    }
    
    private void EndDrag(BaseEventData eventData)
    {
        if (!TurnManager.instance.isMyTurn(team)) return;
        
        EventManager.instance.EndShapeDragging();
    }

    public void Initialize(int _team, int rot, bool _isMine)
    {
        isMine = _isMine;
        if (isMine || GameManager.instance.gameMode == GameMode.LocalMultiplayer)
            TriggerEvent();
        
        team = _team;
        Rotation(rot);
        if(GameManager.instance.isOnline && !isMine)
            Rotation(180);
    }

    private void Rotation(int rot)
    {
        if(rot == 0) return;

        rotation = rot;
        for (int i = 0; i < 9; i++)
        {
            _transform.GetChild(i).GetComponent<Image>().enabled = false;
        }

        List<int> newPieces = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (pieces[i])
            {
                newPieces.Add(rotations[rot][i]);
                pieces[i] = false;
            }
        }

        for (int i = 0; i < newPieces.Count; i++)
        {
            _transform.GetChild(newPieces[i]).GetComponent<Image>().enabled = true;
            pieces[newPieces[i]] = true;
        }
    }

    public void TurnBack()
    {
        StartCoroutine(IETurnBack());
    }

    private IEnumerator IETurnBack()
    {
        for (int i = 0; i < 101; i++)
        {
            _transform.localPosition = Vector3.Lerp(_transform.position, Vector3.zero, 100f/(float)i);
            yield return new WaitForSeconds(0.001f);
        }
    }
    
    
}
