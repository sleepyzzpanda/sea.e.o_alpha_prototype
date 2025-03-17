using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import math
using System;

public class player_behavior : MonoBehaviour
{
    // variable parking lot -----------------------
    public float speed = 5.0f;
    public Transform move_point;
    public LayerMask stop_movement, BOX;
    public Sprite[] normal_front, normal_back, normal_left, normal_right;
    public Sprite[] diving_front, diving_back, diving_left, diving_right;
    
    public GameObject _sprite;
    public bool is_diving;
    private int counter = 0;
    private float timeAccumulator = 0f;
    private float frameRate = 1f / 6f; // 1/60th of a second (60 FPS)

    public AudioSource audioSource;
    public AudioClip audioClip;
    // ---------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        is_diving = false;
        move_point.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        // increment counter when player moves
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            timeAccumulator += Time.deltaTime;
            while (timeAccumulator >= frameRate)
            {
                counter++;
                timeAccumulator -= frameRate;
                // if audio not playing
                if(!audioSource.isPlaying){
                    audioSource.PlayOneShot(audioClip);
                }
            }
            if(counter >= 7){
                counter = 0;
            }
        }
        //prevent movement if dialoague is playing
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        // move player to move_point
        transform.position = Vector3.MoveTowards(transform.position, move_point.position, speed * Time.deltaTime);
        // if player is at move_point, player can move
        if(Vector3.Distance(transform.position, move_point.position) <= 0.05f){
            player_move();
        }
        
        
    }

    void player_move(){
        
        // if press the arrow keys, move
        if(Math.Abs(Input.GetAxisRaw("Horizontal")) == 1f){
            if(!Physics2D.OverlapCircle(move_point.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.64f, 0f, 0f), 0.15f, stop_movement) && !Physics2D.OverlapCircle(move_point.position + new Vector3(Input.GetAxisRaw("Horizontal") * 0.64f, 0f, 0f), 0.15f, BOX) )
            {
                move_point.position += new Vector3(Input.GetAxisRaw("Horizontal") * 0.64f, 0f, 0f);
                if(is_diving){
                    if(Input.GetAxisRaw("Horizontal") == 1f){
                        _sprite.GetComponent<SpriteRenderer>().sprite = diving_right[counter];
                    } else if(Input.GetAxisRaw("Horizontal") == -1f){
                        _sprite.GetComponent<SpriteRenderer>().sprite = diving_left[counter];
                    }
                } else {
                    if(Input.GetAxisRaw("Horizontal") == 1f){
                        _sprite.GetComponent<SpriteRenderer>().sprite = normal_right[counter];
                    } else if(Input.GetAxisRaw("Horizontal") == -1f){
                        _sprite.GetComponent<SpriteRenderer>().sprite = normal_left[counter];
                    }
                }
            }
        } else if(Math.Abs(Input.GetAxisRaw("Vertical")) == 1f){
            if(!Physics2D.OverlapCircle(move_point.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.64f, 0f), 0.15f, stop_movement) && !Physics2D.OverlapCircle(move_point.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.64f, 0f), 0.15f, BOX)){
                move_point.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 0.64f, 0f);
                if(is_diving){
                    if(Input.GetAxisRaw("Vertical") == 1f){
                        _sprite.GetComponent<SpriteRenderer>().sprite = diving_back[counter];
                    } else if(Input.GetAxisRaw("Vertical") == -1f){
                        _sprite.GetComponent<SpriteRenderer>().sprite = diving_front[counter];
                    }
                } else {
                    if(Input.GetAxisRaw("Vertical") == 1f){
                        _sprite.GetComponent<SpriteRenderer>().sprite = normal_back[counter];
                    } else if(Input.GetAxisRaw("Vertical") == -1f){
                        _sprite.GetComponent<SpriteRenderer>().sprite = normal_front[counter];
                    }
                }
            }
        }  
    }   
}
