using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inky : EnemyBehaviour
{
    private GameObject player;
    private Vector2 playerVelocity;

    public override Vector2 Target
    {
        get
        {
            GetPlayerVelocity();

            //float 
            Vector2 pivot = (Vector2)player.transform.position + (playerVelocity.normalized * 2);

            //GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject blinky = GameObject.FindGameObjectWithTag("Blinky");

            Vector2 delta = pivot - (Vector2)blinky.transform.position;
            return blinky.transform.position * -1;
        }
    }

    private void GetPlayerVelocity()
    {
        playerVelocity = player.GetComponent<PlayerController>().GetTempDirection();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerVelocity = Vector2.right;
        GetPlayerVelocity();
    }
}
