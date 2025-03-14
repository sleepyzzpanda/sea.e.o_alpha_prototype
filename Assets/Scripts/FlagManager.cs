using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private int scene_index;
    public GameObject suit_trigger;
    public GameObject player, sceneflag3, sceneflag4, sceneflag5, sceneflag6, sceneflag7, sceneflag8;
    // Start is called before the first frame update
    void Start()
    {
        scene_index = 2; // inits to 0 
        suit_trigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(scene_index){
            case 0:
                // start screen
                break;
            case 1: // exposition
                /*MC leaves underwater vehicle, goes up ladder to entrance on first floor (labs)*/
                break;
            case 2: 
                // EMP greets mc:
                // some introductory dialogue
                // will give them an item (datapad) to give to player which will be used as their menu screen.
                // “to record your research and thoughts on”
                // check if scene2 gameobject is inactive
                // if inactive, set scene2Done to true
                // if(GameObject.Find("scene2").activeSelf == false){
                //     scene2Done = true;
                // }
                string scene2Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene2Done")).value;
                if(scene2Done == "true"){
                    // find scene3 gameobject and set it to active
                    sceneflag3.SetActive(true);
                    scene_index = 3;
                }
                break;
            case 3: 
                // tutorial
                // when player opens menu item ==> intra will show up on screen and introduce itself
                // talks abt menu options
                // tutorial and welcome from company
                // asks EMP to give mc a tour

                string scene3Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene3Done")).value;
                if(scene3Done == "true"){
                    // find scene4 gameobject and set it to active
                    sceneflag4.SetActive(true);
                    scene_index = 4;
                }
                break;
            case 4:
                // tour
                //as theyre walking around, EMP or intra can explain whats going on (brief)
                // “we’ll start with where you’ll be staying”
                // players asks what EMP does

                string scene4Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene4Done")).value;
                if(scene4Done == "true"){
                    sceneflag5.SetActive(true);
                    scene_index = 5;
                }
                break;
            case 5:
                // go to second floor first where living area is (show/explain how to go to second floor)
                // use elevator
                // MC can walk around different areas and short cutscenes to introduce each rooms (1-3 lines)
                // in each room, multiple interactables (but unnecessary if not highlighted/indicated)
                // EMP follows around

                // things to interact with
                // 1. dining area -> observation log / data packet
                // 2. bedrooms -> push boxes, find usb (need screwdriver from electrical room on 1st floor)
                // 3. infirmary -> find first aid kit
                // 4. showers/washroom -> find containment suit
                // string varName = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("test_var")).value;
                // string observation_deck = bool.Parse(((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("observation_deck")).value);
                // bool bedrooms = bool.Parse(((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("bedrooms")).value);
                // bool infirmary = bool.Parse(((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("infirmary")).value);
                // bool showers = bool.Parse(((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("showers")).value);
                // bool dining_hall = bool.Parse(((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("dining_hall")).value);
                // bool area_done = bool.Parse(((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene5Done")).value); // check if all prev objectives done
                // if(observation_deck && bedrooms && infirmary && showers && dining_hall){
                //     ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene5Done")).value = "true";
                // }
                string observation_deck = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("observation_deck")).value;
                string bedrooms = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("bedroom")).value;
                string infirmary = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("infirmary")).value;
                string showers = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("showers")).value;
                string dining_hall = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("dining_hall")).value;
                string area_done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene5Done")).value;
                if(observation_deck == "true" && bedrooms == "true" && infirmary == "true" && showers == "true" && dining_hall == "true"){
                    ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("s5objsDone")).value = "true";
                    ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene5Done")).value = "true";
                }
                if(area_done == "true"){
                    sceneflag6.SetActive(true);
                    scene_index = 6;
                }
                break;
            case 6: 
                // print for testing
                // cutscene:
                // go back to first floor
                // INTRA takes over tour regarding science research
                // player can thank or ignore EMP, who heads for repair room/electrical 
                // (tells players to find him there if needed)
                string scene6Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene6Done")).value;
                if(scene6Done == "true"){
                    sceneflag7.SetActive(true);
                    scene_index = 7;
                }
                break;
            case 7:
                // cutscene
                // explore second floor w INTRA
                // explains they’re in process of decommissioning station
                // need MC help to sort whats useful research, backup digital data to INTRA
                string scene7Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene7Done")).value;
                if(scene7Done == "true"){
                    sceneflag8.SetActive(true);
                    scene_index = 8;
                }
                break;
            case 8:
                // 1st floor stuff
                // there is a lot going on here
                // VAR moonpool = "false"
                // VAR admin = "false"
                // VAR chem_mixer = "false"
                // VAR chem_identifier = "false"
                // VAR marine_room_wires = "false"
                // VAR marine_animal = "false"
                // VAR electrical_data = "false"
                // VAR screwdriver = "false"
                // VAR flashlight = "false"
                string moonpool = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("moonpool")).value;
                string admin = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("admin")).value;
                string chem_mixer = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("chem_mixer")).value;
                string chem_identifier = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("chem_identifier")).value;
                string marine_room_wires = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("marine_room_wires")).value;
                string marine_animal = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("marine_animal")).value;
                string electrical_data = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("electrical_data")).value;
                string screwdriver = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("screwdriver")).value;
                string flashlight = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("flashlight")).value;
                if(moonpool == "true" && admin == "true" && chem_mixer == "true" && chem_identifier == "true" && marine_room_wires == "true" && marine_animal == "true" && electrical_data == "true" && screwdriver == "true" && flashlight == "true"){
                    scene_index = 9;
                }
                break;
            case 9:
                //print for testing
                Debug.Log("scene 9");
                // cutscene
                // once everything is explored, intra asks mc to deliver a sample outside 
                // (deliver what was made from lab task)
                string scene9Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene9Done")).value;
                if(scene9Done == "true"){
                    suit_trigger.SetActive(true);
                    scene_index = 10;
                }
                break;
            case 10:
                // MC goes to changeroom to put on containment suit
                // then moon pool 
                // heads outside
                break;
            case 11:
                // puzzle
                // fix and send one drone off
                // needs to do puzzle to confirm drone movement, drone leaves
                // find missing piece, retrieve other drone to station
                break;
            case 12:
                // cutscene
                // monster chases to entrance of pool, door closes off before monster can get in
                break;
            case 13:
                // inside now
                // ai plays a fool, pretends not to know whats happening
                // tells mc to eliminate if sees again (could be source of virus)
                // INTRA forbids player from going outside again unless permitted “for safety”
                // mc doesn’t care and wants to go back to investigate scene
                // need EMP to temporarily disable AI screens/cameras outside
                break;
            case 14:
                // EMP disables and leaves, mc goes outside
                // discover sealed off vent (“it’s sealed off”)
                // MC wonders where it goes in station
                break;
            case 15:
                // MC goes back and recruits EMP for help (EMP waiting in repair room or smth?)
                // needs a map of the vents
                // EMP is curious now too, but cautious
                // EMP disables again (but for longer), go to vent on first floor, enter
                // warns INTRA will be suspicious
                break;
            case 16:
                // secret lab floor
                // lots of stuff happens here
            case 17:
                // climax
                // containment room
                // cutscene
                break;
            case 18:
                // puzzle
                break;
            case 19:
                // cutscene
                break;
            case 20:
                // smash the screen, go against INTRA -> scene 24
                // go along with INTRA and company in destroying evidence, 
                // turning blind eye, reach corporate success -> scene 21
                break;
            case 21:
                // INTRA releases a monster, kills EMP
                // player kills monster
                // kill all the monsters in tubes
                break;
            case 22:
                // head back up to first floor
                // help INTRA set station to self destruct
                // leave station via returned drone?
                break;
            case 23:
                // cutscene
                // renegade ending
                // go to game over screen
                break;
            case 24:
                // not gonna listen to intra, intra starts shutting doors, lights, trying to kill mc and emp, monsters from containment let out
                // EMP unlocks doors
                // MC needs to hold off monsters
                // MC and emp escapes from more monsters
                // head back up to 1st floor
                break;
            case 25:
                // options to kill monsters or just run - can use claw once as a weapon
                // Arlo gets thru security doors while players distract monsters
                break;
            case 26:
                // finally reach first floor
                // if EMP survived, can lock off area for mc so they can make a cure
                // make cure
                break;
            case 27:
                // go to AI room
                // destroy INTRA
                break;
            case 28:
                // spread cure
                break;
            case 29:
                // ending - based on choices
                break;
            
        }
    }
}
