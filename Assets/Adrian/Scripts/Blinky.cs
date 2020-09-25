using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : EnemyBehaviour
{
    public override Vector2 Target
    {
        get
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            return player.transform.position;
        }
    }
}
