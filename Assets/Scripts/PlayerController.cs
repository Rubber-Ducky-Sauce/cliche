using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    private new Rigidbody2D rigidbody;
    [SerializeField]private float jumpForce = 10f;
    public bool hiding = false;

    private bool isOnGround;
    [SerializeField]private float groundRay = 1f;
    private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        speed = 5f;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Hide();
        if (hiding)
            Debug.Log("hiding");
    }

    private void FixedUpdate()
    {
        //draws ray to ground
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, groundRay, groundLayer);
        Debug.DrawRay(transform.position, direction * groundRay, Color.yellow);

        isOnGround = hit.collider != null;
    }
    public override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(!hiding)
        transform.Translate(Vector3.left * -horizontal * Time.deltaTime * speed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isOnGround && !hiding)
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Hide()
    {
        _ = Input.GetAxis("Vertical") > 0 ? hiding = true : hiding = false;
    }
}
