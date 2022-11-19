using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : Actor
{
    private new Rigidbody2D rigidbody;
    private new LightController light;
    [SerializeField]private float jumpForce = 5f;

    public bool isHiding = false;
    public bool isSneaking = false;
    private float sneakSpeed;
    private float movementSpeed;

    public bool isFacingLeft;
    private bool isOnGround;
    public bool isMoving;
    public bool isClimbing = false;

    //todo?:set player to ladder center
    //public Interactable interactable;
    [SerializeField]private float groundRay = 1f;
    private LayerMask groundLayer;

    public bool gameActive;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Player Light"))
        light = GameObject.Find("Player Light").GetComponent<LightController>();

        Speed = 5f;
        movementSpeed = Speed;
        sneakSpeed = Speed / 3;
        groundLayer = LayerMask.GetMask("Ground");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetIsGameActive();
        if (gameActive)
        {
            movementSpeed = HandleSpeed();
            Move();
            Jump();
            Climb();
            Interact();
            Sneak();
            UseItem();
            //TryLock();
        }

    }

    private void FixedUpdate()
    {
        if(light != null)
        light.hiding = isHiding || isSneaking;
        DetectGround();
    }
    public override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(!isHiding)
        transform.Translate(Vector3.right * horizontal * Time.deltaTime * movementSpeed);

        if (horizontal > 0)
            isFacingLeft = false;
        if (horizontal < 0)
            isFacingLeft = true;

        isMoving = isOnGround && horizontal != 0;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && (isOnGround || isClimbing) && !isHiding)
        {
            isClimbing = false;
            rigidbody.gravityScale = 1;
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Climb()
    {
        float vertical = Input.GetAxis("Vertical");
        if (vertical>0 && !isClimbing && GameManager.Instance.currentInteractable?.GetComponent<Climbable>())
        {
            isClimbing = true;
            rigidbody.velocity = Vector3.zero;
        }

        if (isClimbing && GameManager.Instance.currentInteractable != null && GameManager.Instance.currentInteractable.GetComponent<Climbable>())
        {
            rigidbody.gravityScale = 0;
            transform.Translate(1 * Time.deltaTime * vertical * Vector3.up);
        }

        if (GameManager.Instance.currentInteractable == null && isClimbing || vertical < 0  && isOnGround)
        {
            isClimbing = false;
            rigidbody.gravityScale = 1;
        }

    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentInteractable != null)
            GameManager.Instance.currentInteractable.Interact();
    }

    private void Sneak()
    {
        _ = Input.GetAxis("Vertical") < 0 && !isClimbing? isSneaking = true : isSneaking = false;
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
        return isSneaking?sneakSpeed:Speed;
    }

    private void GetIsGameActive()
    {
        gameActive = GameManager.Instance.gameIsActive;
    }

    private void UseItem()
    {
        if (Input.GetAxis("Vertical") > 0 && Input.GetKeyDown(KeyCode.E))
            GameManager.Instance.UseCurrentItem();
    }
}
