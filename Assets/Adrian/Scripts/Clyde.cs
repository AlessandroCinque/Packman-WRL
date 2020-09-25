using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde : EnemyBehaviour
{
    public override Vector2 Target
    {
        get
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            Vector2 delta = player.transform.position - transform.position;

            if (delta.magnitude <= GetComponent<Enemy>().ClydeMinDistance)
            {
                return player.transform.position * -1;
            }

            return player.transform.position;
        }
    }
}
