using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3[] spawn_points;
    void Start()
    {
        // spawn in at a random spawn point
        transform.position = spawn_points[Random.Range(0, spawn_points.Length)];        
    }

}
