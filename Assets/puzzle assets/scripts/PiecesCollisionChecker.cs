using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiecesCollisionChecker : PiecesParent, IDropHandler
{

    protected void Update() {
        base.Update();

        //Debug.Log(correctState);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Item Dropped on Slot");

        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            // Snap the dragged object to this slot
            RectTransform draggedRectTransform = draggedObject.GetComponent<RectTransform>();
            draggedRectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            //say this slot is active
            string colour2 = draggedObject.GetComponent<Moveable>().colour;
            //Debug.Log(colour2);
            //Debug.Log(draggedObject.GetComponent<Moveable>().orientation == orientation);

            if(draggedObject.GetComponent<Moveable>().colour == colour &&
               draggedObject.GetComponent<Moveable>().orientation == orientation) {
                   draggedObject.GetComponent<Moveable>().changeCorrectState();
                   Debug.Log("dragged obj: " + draggedObject.GetComponent<Moveable>().correctState);
                   correctState = true;
               }

            else correctState = false;
        }

    }

    /*
    void OnCollisionEnter2D(Collision2D other) {
        activeState = true;
    }

    void OnCollisionStay2D(Collision2D other) {
        activeState = true;
    }

    void OnCollisionExit2D(Collision2D other) {
        activeState = false;
    }
    */
}
