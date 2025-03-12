using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    /*
    [Header("Game Elements")]


    [Header("UI Elements")]
    [SerializedField] private GameObject nextPuzzleButton;
    */
    public GameObject nextPuzzleButton;
    public int numPieces;

    public PiecesCollisionChecker[] slots;
    public Moveable[] pieces;

    private int piecesCorrect;

    // Start is called before the first frame update
    void Start()
    {
        //hide UI
        //levelSelectPanel.gameObject.SetActive(false);

        piecesCorrect = 0;
        nextPuzzleButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //only occur after mouse is released so doesnt update all the time
        if(Input.GetMouseButtonUp(0)) {
            //Debug.Log("mouse down");
            
                int numCorrect = 0;
                int curLoop = 0;

                //Debug.Log(pieces.Length);

                //check if all pieces are in correct slot
                foreach(PiecesCollisionChecker obj in slots) {
                    //Debug.Log("curLoop: " + curLoop + " state: " + obj.correctState);
                    //Debug.Log("curLoop: " + curLoop + " state: " + pieces[curLoop].correctState);

                    //check if the piece is in the correct slot
                    if(obj.correctState && pieces[curLoop].correctState) {
                        //Debug.Log("changed");
                        numCorrect++;
                    }
                    curLoop++;
                }

                piecesCorrect = numCorrect;
                //Debug.Log(piecesCorrect);
        //}    
        }
        

        if(piecesCorrect == numPieces) {

            if(nextPuzzleButton.activeSelf == false) {
                nextPuzzleButton.SetActive(true);

                //prevent pieces from being moved once complete
                foreach(Moveable obj in pieces) {
                    obj.disabled = true;
                }
            }
        }
    }

    public void NextLevel() {
        /*
        //remove pieces
        foreach(Transform piece in pieces) {
            Destroy(piece.gameObject);
        }
        pieces.Clear();

        nextPuzzleButton.setActive(false);
        */
    }
    
}
