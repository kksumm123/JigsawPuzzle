using System;
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

        // 올바른 위치 드랍되면
        if (IsRighPosition(eventData.pointerDrag))
        {
            var draggedImage = eventData.pointerDrag.GetComponent<Image>();
            // 반짝이게
            StartCoroutine(BlickCo(draggedImage));

            // 점수 증가
            //GameManager.Instance.AddScore(100);

            // Piece 컴포넌트 끄기
            eventData.pointerDrag.GetComponent<Piece>().enabled = false;
        }
    }

    bool IsRighPosition(GameObject pointerDrag)
    {
        if (pointerDrag.name == name)
            return true;

        return false;
    }

    float blinkTime = 0.1f;
    IEnumerator BlickCo(Image image)
    {
        image.color = Color.red;
        yield return new WaitForSeconds(blinkTime);
        image.color = Color.white;

        // 드래그된 것 못 움직이게
        image.raycastTarget = false;
    }
}
