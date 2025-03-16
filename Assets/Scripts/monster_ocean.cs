using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_ocean : MonoBehaviour
{
    private float speed;
    public Transform move_point; // this will be the player's position
    public LayerMask stop_movement;
    // Start is called before the first frame update
    void Start()
    {
        speed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, move_point.position, speed * Time.deltaTime);
        // make it so that the stop_movement layermask is the only thing that can stop the monster
        
    }
}
