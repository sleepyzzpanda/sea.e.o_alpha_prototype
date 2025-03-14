using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class TestGlobalsNPC : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color chosenColor1 = Color.red;
    [SerializeField] private Color chosenColor2 = Color.blue;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        //typecast to value ex. this one is string, but also theres bool, float, int
        string varName = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("test_var")).value; //value to get string value of variable

        switch(varName)
        {
            case "":
                spriteRenderer.color = defaultColor;
                break;
            case "Yes":
                spriteRenderer.color = chosenColor1;
                break;
            case "No":
                spriteRenderer.color = chosenColor2;
                break;
            default:
                Debug.LogWarning("var name not handled by switch statement: " + varName );
                break;
        }
    }
}
