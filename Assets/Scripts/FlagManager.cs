using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private int scene_index;
    public GameObject suit_trigger, teleportIN_trigger, ocean_monster, vent_trigger, SF2_trigger;
    public GameObject player, sceneflag3, sceneflag4, sceneflag5, 
    sceneflag6, sceneflag7, sceneflag8, sceneflag9, sceneflag11,
    sceneflag12, sceneflag13, sceneflag14;
    // Start is called before the first frame update
    void Start()
    {
        scene_index = 13; // inits to 0 
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
                // things to interact with
                // 1. dining area -> observation log / data packet
                // 2. bedrooms -> push boxes, find usb (need screwdriver from electrical room on 1st floor)
                // 3. infirmary -> find first aid kit
                // 4. showers/washroom -> find containment suit
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
                    sceneflag9.SetActive(true);
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
                    teleportIN_trigger.SetActive(true);
                    scene_index = 10;
                }
                break;
            case 10:
                // MC goes to changeroom to put on containment suit
                // then moon pool 
                // heads outside
                Debug.Log("scene 10");
                string containment_suit = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("containment_suit")).value;
                if(containment_suit == "true"){
                    sceneflag11.SetActive(true);
                    scene_index = 11;
                }
                break;
            case 11:
                // puzzle
                // fix and send one drone off
                // needs to do puzzle to confirm drone movement, drone leaves

                Debug.Log("scene 11");
                string s11_puzzle = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("s11_puzzle")).value;
                if(s11_puzzle == "true"){
                    sceneflag12.SetActive(true);
                    scene_index = 12;
                }
                break;
            case 12:
                // cutscene
                // monster chases to entrance of pool, door closes off before monster can get in
                Debug.Log("scene 12");
                string monster_chase = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("monster_chase")).value;
                if(monster_chase == "true"){
                    ocean_monster.SetActive(true);
                }
                string scene12Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene12Done")).value;
                if(scene12Done == "true"){
                    scene_index = 13;
                    sceneflag13.SetActive(true);
                    ocean_monster.SetActive(false);
                    teleportIN_trigger.SetActive(false);
                }
                break;
            case 13:
                Debug.Log("scene 13");
                // find and talk to Arlo
                string scene13Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene13Done")).value;
                if(scene13Done == "true"){
                    vent_trigger.SetActive(true);
                    sceneflag14.SetActive(true);
                    scene_index = 14;
                }
                break;
            case 14:
                Debug.Log("scene 14");
                string scene14Done = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("scene14Done")).value;
                if(scene14Done == "true"){
                    scene_index = 15;
                }
                break;
            case 15:
                // secret floor lvl 1
                Debug.Log("scene 15");
                string conferenceKey = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("conferenceKey")).value;
                string conferenceScreen = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("conferenceScreen")).value;
                string lab1card = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("lab1card")).value;
                string lab2log = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("lab2log")).value;
                string lab3ingredientList = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("lab3ingredientList")).value;
                string harpoon = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("harpoon")).value;
                string weaponsKey = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("weaponsKey")).value;
                if(conferenceKey == "true" && conferenceScreen == "true" && 
                lab1card == "true" && lab2log == "true" && lab3ingredientList == "true" && 
                harpoon == "true" && weaponsKey == "true")
                {
                    scene_index = 16;
                    SF2_trigger.SetActive(true);
                }
                break;
            case 16:
                // secret floor lvl 2
                Debug.Log("scene 16");
                string mutant = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("mutant")).value;
                string disposalKey = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("disposalKey")).value;
                string brokenClaw = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("brokenClaw")).value;
                string unknownSample = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("unknownSample")).value;
                string bloodyReport = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("bloodyReport")).value;
                if(mutant == "true" && disposalKey == "true" && 
                brokenClaw == "true" && unknownSample == "true" && bloodyReport == "true")
                {
                    scene_index = 17;
                }
                break;
            case 17:
                Debug.Log("scene 17");
                // scene 17
                break;
            
        }
        string gamover = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("gameover")).value;
        if(gamover == "true"){
            Debug.Log("game over");
            scene_index = -1;
        }
    }
}
