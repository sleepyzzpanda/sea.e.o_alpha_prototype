using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower_behavior : MonoBehaviour
{
    public Transform player;  // Reference to the player object
    private float followSpeed = 2.5f;
    public float stoppingDistance = 0.5f; // Distance to maintain from the player

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference not set in FollowerBehavior!");
            return;
        }

        // Move towards the player's position if it's farther than the stopping distance
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
        // if the distance is more than 10, teleport to the player's position + 5x + 5y
        if (Vector3.Distance(transform.position, player.position) > 20)
        {
            transform.position = player.position + new Vector3(5, 5, 0);
        }
    }
}
