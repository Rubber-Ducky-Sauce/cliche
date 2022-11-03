using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.left * -horizontal * Time.deltaTime * speed);
    }
}
