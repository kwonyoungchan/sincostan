using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        PlayerMove player = other.GetComponent<PlayerMove>();
        if(player)
        {
            player.state=PlayerMove.MoveStates.Climb;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        PlayerMove player = other.GetComponent<PlayerMove>();
        if(player)
        {
            player.state=PlayerMove.MoveStates.Exit;
        }
    }
    
}
