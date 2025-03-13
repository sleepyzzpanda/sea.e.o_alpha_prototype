using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject player, camera;
    public LayerMask teleportLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if player overlaps with teleport flag item, teleport player to the location specified by the flag
        // flags are on TELEPORT layer
        if(Physics2D.OverlapCircle(player.transform.position, 0.15f, teleportLayer)){
            TeleportFlag flag = Physics2D.OverlapCircle(player.transform.position, 0.15f, teleportLayer).GetComponent<TeleportFlag>();
            player.transform.position = flag.newPlayerPos.position; // set player pos
            // player move point too
            player.GetComponent<player_behavior>().move_point.position = flag.newPlayerPos.position;
            camera.transform.position = flag.cameraPos.position;
            
        }


        
    }
}
