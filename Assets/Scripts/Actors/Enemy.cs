using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    private float rayLength = .75f;
    private float rayX = 1;
    private float wallX = .7f;
    private float offSet = .55f;
    private LayerMask groundLayer;
    private LayerMask playerLayer;
    private LayerMask IgnoreMe;

    [SerializeField] private float speed = 1f;
    [SerializeField] float detectDistance = 5f;
    [SerializeField] private bool gameActive;
    [SerializeField] GameObject alertMarker;
    [SerializeField] bool facingRight;


    // Start is called before the first frame update
    void Start()
    {
        facingRight = transform.localScale.x < 0;
        Speed = speed;
        InitDirection();
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");
        IgnoreMe = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        GetIsGameActive();
        if (gameActive)
        {
            Move();
            DetectPlayer();
        }
    }
    public override void Move()
    {
        DetectEdge();
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
    }

    private void DetectEdge()
    {
        //draws ray angled to ground
        Vector2 direction = new Vector2(rayX,-1);
        Vector2 wallDir = new Vector2(1, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, groundLayer);
        Debug.DrawRay(transform.position, direction * rayLength, Color.yellow);
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, wallDir, wallX, groundLayer);
        Debug.DrawRay(transform.position - new Vector3(0, .1f, 0), wallDir * wallX, Color.green);

        if (hit.collider == null || wallHit.collider != null)
        {
            TurnAround();
        }
    }

    private void DetectPlayer()
    {
        //draws ray to ground
        Vector2 direction = new Vector2(detectDistance, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(offSet,0,0), direction, Mathf.Abs(detectDistance), ~IgnoreMe);
        Debug.DrawRay(transform.position + new Vector3(offSet, 0, 0), direction, Color.red);

        if (hit.collider != null  && hit.collider.tag == "Player" && !IsPlayerHiding(hit.collider.gameObject))
        {
            Debug.Log("Player Found! Straight to Jail!");
            GameManager.Instance.SetIsGameActive(false);
            hit.collider.GetComponent<PlayerController>().gameActive = false;
            alertMarker.SetActive(true);
            StartCoroutine(GameManager.Instance.ReloadScene(2f));
        }
           

    }

    private bool IsPlayerHiding(GameObject playerObject)
    {
        PlayerController player = playerObject.GetComponent<PlayerController>();
        return player.isHiding;
    }

    private void GetIsGameActive()
    {
        gameActive = GameManager.Instance.gameIsActive;
    }

    public void TurnAround()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        wallX = -wallX;
        rayX = -rayX;
        offSet = -offSet;
        detectDistance = -detectDistance;
        Speed = -Speed;
    }
    public void InitDirection()
    {
        Debug.Log(Speed);
        if (!facingRight)
        {
            wallX = -wallX;
            rayX = -rayX;
            offSet = -offSet;
            detectDistance = -detectDistance;
            Speed = -Speed;
        }
        Debug.Log(Speed);
    }
}
