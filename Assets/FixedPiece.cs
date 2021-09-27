using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedPiece : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.position = transform.position;

        string log = $"{eventData.pointerDrag.name} 오브젝트가 {name}에 드랍됨";
        Debug.Log(log, transform);

        if (eventData.pointerDrag.name == name)
            eventData.pointerDrag.GetComponent<Piece>().enabled = false;
    }
}
