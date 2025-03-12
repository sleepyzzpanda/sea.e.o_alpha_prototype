using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesParent : Entity
{

    public bool correctState;
    public bool activeState;
    public string orientation; 
    public string colour;
    public string tagOther;

    // Start is called before the first frame update
    protected void Start()
    {
        correctState = false;
        activeState = false;

        /*
        orientation = "horizontal";
        colour = "RB";
        tagOther = "PieceCollisionCheck";
        */
    }

    // Update is called once per frame
    protected void Update()
    {
        //if(activeState) Debug.Log("active");
        //if(correctState) Debug.Log("correct");
    }

    void OnCollisionEnter2D(Collision2D other) {
        //check if colliding with another piece
        if(other.gameObject.tag.Equals(tagOther) == true) {
            //occupied 
            activeState = true;

            
        }
    }

    void checkCorrect(PiecesParent other) {
        //correctly placed
            if(other.orientation == orientation && other.colour == colour) {
                correctState = true;
            }
    }
}
