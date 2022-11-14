using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class HearPlayer : MonoBehaviour
{
    PlayerController player;
    [SerializeField] float hearingDistance;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if((player.transform.position - transform.position).magnitude <= hearingDistance && player.isMoving && !player.isCrouching)
        {
            Debug.Log("I hear something...");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, .3f);
        Gizmos.DrawSphere(this.transform.position, hearingDistance);
    }
}
