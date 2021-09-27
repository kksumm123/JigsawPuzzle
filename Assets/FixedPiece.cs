using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedPiece : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        string log = $"{name}에 드랍됨 : {eventData.pointerDrag.name}";
        Debug.Log(log, transform);
    }
}
