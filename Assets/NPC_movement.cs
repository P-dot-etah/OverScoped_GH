using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_movement : MonoBehaviour
{
    public GameObject player;
    public GameObject npc;
    public float speed;

    private float distance;
    public float distance_unit;

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if(distance < distance_unit)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, npc.transform.position, speed * Time.deltaTime);
        }
    }
}
