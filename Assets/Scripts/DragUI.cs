using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    [SerializeField] private bool isDraggable = true;
    [SerializeField] private float draggingZCoord = 10f;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();  

        if(canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(!isDraggable) return;

        startPosition = transform.position;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData){
        if(!isDraggable) return;

        if(GetComponentInParent<Canvas>())
            rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
        else{
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, draggingZCoord);
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        if(!isDraggable) return;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private void CheckDropPosition(){
        //if(!IsValidDropPosition()){
        //    transform.position = startPosition;
        //}
    }

    public void SetDraggable(bool draggable){
        isDraggable = draggable;
    }
}
