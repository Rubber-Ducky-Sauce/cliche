using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShowDistances : MonoBehaviour
{
    Enemy enemy;
    HearPlayer enemyHearing;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyHearing = GetComponent<HearPlayer>();
    }
    void Update()
    {

        if(enemy.usingMovementDistance)
        DrawMovementDistance();
        DrawVisualDistance();
    }

    private void DrawMovementDistance()
    {
        Vector2 direction = new Vector2(enemy.movementDist, 0);
        if (enemy.usingMovementDistance)
        {
            Debug.DrawRay(transform.position + new Vector3(0, -.33f, 0), direction, Color.gray);
            Debug.DrawRay(transform.position + new Vector3(0, -.33f, 0), -direction, Color.gray);
        }
    }

    private void DrawVisualDistance()
    {
        Vector2 direction = new Vector2(enemy.detectDistance, 0);
        Debug.DrawRay(transform.position + new Vector3(enemy.offSet, 0, 0), direction, Color.red);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, .3f);
        Gizmos.DrawSphere(transform.position, enemyHearing.hearingDistance);
    }
}
