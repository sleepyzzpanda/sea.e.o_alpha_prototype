using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Moveable : PiecesParent, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public bool disabled; //if preplaced
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private void Awake()
    {
        if(!disabled) {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>(); // Reference the parent canvas
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        if(!disabled) {
            canvasGroup.alpha = 0.6f; // Make item slightly transparent
            canvasGroup.blocksRaycasts = false; // Allow drop zones to detect events
            correctState = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!disabled) rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        if(!disabled) {
            canvasGroup.alpha = 1f; // Reset transparency
            canvasGroup.blocksRaycasts = true; // Restore raycast blocking
        }
    }

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();

        tagOther = "PieceCollisionCheck";

    }

    public void changeCorrectState() {
        correctState = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
