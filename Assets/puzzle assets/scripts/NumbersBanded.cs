using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersBanded : Connectors
{
    public int numBands;
    public int curBands; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(numBands == curBands) {
            correctState = true;
        }
    }
}
