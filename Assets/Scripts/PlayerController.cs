using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    private new Rigidbody2D rigidbody;
    private new LightController light;
    [SerializeField]private float jumpForce = 5f;

    public bool isHiding = false;
    public bool isCrouching = false;
    private float crouchSpeed;
    private float movementSpeed;
    private float hideSpeed;

    private bool isOnGround;
    [SerializeField]private float groundRay = 1f;
    private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Player Light"))
        light = GameObject.Find("Player Light").GetComponent<LightController>();

        Speed = 5f;
        movementSpeed = Speed;
        crouchSpeed = Speed / 3;
        hideSpeed = Speed / 6;
        groundLayer = LayerMask.GetMask("Ground");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementSpeed = HandleSpeed();
        Move();
        Jump();
        Hide();
        Crouch();
    }

    private void FixedUpdate()
    {
        if(light != null)
        light.hiding = isHiding || isCrouching;
        DetectGround();
    }
    public override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //if(!isHiding)
        transform.Translate(Vector3.left * -horizontal * Time.deltaTime * movementSpeed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isOnGround && !isHiding)
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Hide()
    {
        _ = Input.GetAxis("Vertical") > 0 ? isHiding = true : isHiding = false;
    }

    private void Crouch()
    {
        _ = Input.GetAxis("Vertical") < 0 ? isCrouching = true : isCrouching = false;
    }

    private void DetectGround()
    {
        //draws ray to ground
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, groundRay, groundLayer);
        Debug.DrawRay(transform.position, direction * groundRay, Color.yellow);

        isOnGround = hit.collider != null;
    }

    private float HandleSpeed()
    {
        float result;
        if (isHiding)
            result = hideSpeed;
        else if (isCrouching)
            result = crouchSpeed;
        else result = Speed;
        return result;
    }
}
