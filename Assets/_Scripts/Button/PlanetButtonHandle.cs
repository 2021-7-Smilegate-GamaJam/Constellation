using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetButtonHandle : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startTouchPos;
    private Vector2 currentTouchPos;
    private Vector2 currentAtkPos;

    public void OnPointerDown(PointerEventData _eventData)
    {
        startTouchPos = _eventData.position;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        currentTouchPos = _eventData.position;

        if (currentTouchPos.y > startTouchPos.y)
        {
            Debug.Log("����");
        }
        else if (currentTouchPos.y < startTouchPos.y)
        {
            Debug.Log("�����̵�");
        }
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        Debug.Log("������");
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        currentAtkPos = _eventData.position;

        if((currentAtkPos - startTouchPos).sqrMagnitude < 1f)
        {
            Debug.Log("����");
        }
    }
}
