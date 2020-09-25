using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : EnemyBehaviour
{
    private GameObject player;
    private Vector2 playerVelocity;

    public override Vector2 Target
    {
        get
        {
            GetPlayerVelocity();

            return (Vector2)player.transform.position + (playerVelocity.normalized * 4);
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
