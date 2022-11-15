using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearPlayer : MonoBehaviour
{
    PlayerController player;
    Enemy enemy;
    public float hearingDistance;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if((player.transform.position - transform.position).magnitude <= hearingDistance && player.isMoving && !player.isCrouching)
        {
            enemy.BecomeAlert();
        }
    }


}
