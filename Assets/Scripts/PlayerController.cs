using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    private new Rigidbody2D rigidbody;
    [SerializeField]private float jumpForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    public override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.left * -horizontal * Time.deltaTime * speed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
