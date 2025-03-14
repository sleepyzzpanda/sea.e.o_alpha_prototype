using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAudioInfo", menuName = "ScriptableObjects/DialogueAudioInfoSO", order = 1)]
public class DialogueAudioInfoSO : ScriptableObject
{
    public string id; //for identification

    public AudioClip dialogueTypingSoundClip;
    //values to adjust num of characters before sound effect is played
    [Range(1, 8)]
    public int frequencyLevel = 4;
    //values to randomize pitch
    [Range(-3, 3)] //values cuz audio source restricted to this
    public float minPitch = 0.5f;
    [Range(-3, 3)] //values cuz audio source restricted to this
    public float maxPitch = 3f;

    public bool stopAudioSource;

}
