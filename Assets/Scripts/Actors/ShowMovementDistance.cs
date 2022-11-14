using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShowMovementDistance : MonoBehaviour
{
    Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void Update()
    {
        if(enemy.usingMovementDistance)
        DrawMovementDistance();
    }

    private void DrawMovementDistance()
    {
        Vector2 direction = new Vector2(enemy.movementDist, 0);
        if (enemy.usingMovementDistance)
        {
            Debug.DrawRay(transform.position + new Vector3(0, -.33f, 0), direction - new Vector2(transform.position.x, 0), Color.gray);
            Debug.DrawRay(transform.position + new Vector3(0, -.33f, 0), -direction - new Vector2(transform.position.x, 0), Color.gray);
        }
    }
}
