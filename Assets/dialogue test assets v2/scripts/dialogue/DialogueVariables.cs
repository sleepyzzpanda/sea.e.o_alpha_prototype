//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
//using System.IO;

public class DialogueVariables 
{
    //dictionary to map string (variable name) to ink.runtime.object (value of variable) by reading globals ink file
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set;}

    /*save system
    
    private Story globalVariablesStory;
    private const string saveVariablesKey = "INK_VARIABLES";
    */

    //construct json file of globals since it isnt done automatically
    //public DialogueVariables(string globalsFilePath)
    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        //string inkFileContents = File.ReadAllText(globalsFilePath); //read contents of File
        //Ink.Compiler compiler = new Ink.Compiler(inkFileContents); 
        //Story globalVariablesStory = compiler.Compile(); //convert string to story obj
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);

        /*
        //save method to persist variables in multiple playthroughs
        if(PlayerPrefs.HasKey(saveVariablesKey))
        {
            string jsonState = PlayerPrefs.GetString(saveVariablesKey);
            globalVariablesStory.state.LoadJson(jsonState);
        }
        */

        //initialize dicitonary to be new dictionary, loop through variables in globals file
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name); //get current value and add entry to dictionary
            variables.Add(name, value);
            Debug.Log("initialized global dialogue variable: " + name + " = " + value); //check 
        }
    }

    /*
    //save method to persist variables in multiple playthroughs
    public void SaveVariables() 
    {
        if(globalVariablesStory != null)
        {
            //load the cur state of all variables to globals story
            VariablesToStory(globalVariablesStory);
            
            //replace with actual save/load method rather than using playerprefs
            PlayerPrefs.SetString(saveVariablesKey, globalVariablesStory.state.ToJson());
        }
    }
    */

    public void StartListening(Story story)
    {
        //need to assign variables observer before listener or else error
        VariablesToStory(story);
        //add function variable changed as listener to when variable in story changed
        story.variablesState.variableChangedEvent += VariableChanged; 
    }

    public void StopListening(Story story) 
    {
        //remove listener so not in bg
        story.variablesState.variableChangedEvent -= VariableChanged; 
    }
    
    //called when variable changed
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        //update dictionary with variables that were alr intialized
        if(variables.ContainsKey(name)) 
        {
            //update ones that were initialized
            //variables.Remove(name);
            //variables.Add(name, value);
            variables[name] = value;
        }
        Debug.Log("Variable changed: " + name + " = " + value);
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
