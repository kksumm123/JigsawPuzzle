using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector3 offset;
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - new Vector3(eventData.position.x, eventData.position.y);
        GetComponent<Image>().raycastTarget = false;

        //하이어라키 순서변경
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.position += offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
    }
}
