using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    private float rayLength = .75f;
    private float rayX = 1;
    private float wallX = .7f;
    public float offSet = .55f;
    private LayerMask groundLayer;
    private LayerMask playerLayer;
    private LayerMask IgnoreMe;

    bool playerFound;

    [SerializeField] private float speed = 1f;
    public float detectDistance = 5f;
    [SerializeField] float postTime;
    [SerializeField] float wallPostTime;
    [SerializeField] bool posted = false;
    [SerializeField] private bool gameActive;
    [SerializeField] GameObject alertMarker;
    [SerializeField] bool facingRight;
    [SerializeField] public bool usingMovementDistance = false;
    [SerializeField] float startPos;
    [SerializeField][Range(0,10)] public float movementDist;
    [SerializeField]public bool alert = false;
    public bool isActive = true;


    // Start is called before the first frame update
    void Start()
    {
        alertMarker = transform.Find("AlertMarker").gameObject;
        startPos = transform.position.x;
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
        if (gameActive && isActive)
        {
            Move();
            DetectPlayer();
        }
    }
    public override void Move()
    {
        DetectEdge();
        if (!posted) {
            transform.Translate(Speed * Time.deltaTime * Vector3.right);
        }

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

        if (hit.collider == null || wallHit.collider != null || ReachedMaxDistance())
        {
            ResetMaxDistX();
            StartCoroutine(TurnAround(wallHit.collider != null ? wallPostTime: postTime));
        }
    }

    private void DetectPlayer()
    {
        //draws ray to ground
        Vector2 direction = new Vector2(detectDistance, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(offSet,0,0), direction, Mathf.Abs(detectDistance), ~IgnoreMe);

        if (hit.collider != null  && hit.collider.tag == "Player" && !IsPlayerHiding(hit.collider.gameObject))
        {
            playerFound = true;
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

    public IEnumerator TurnAround(float postTime)
    {
        posted = true;
        wallX = -wallX;
        rayX = -rayX;
        Speed = -Speed;
        yield return new WaitForSeconds(postTime);
        if (!playerFound)
        {
            posted = false;


            offSet = -offSet;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            detectDistance = -detectDistance;
        }
    }
    public void InitDirection()
    {
        if (!facingRight)
        {
            wallX = -wallX;
            rayX = -rayX;
            offSet = -offSet;
            detectDistance = -detectDistance;
            Speed = -Speed;
        }
    }

    private bool ReachedMaxDistance()
    {
        if(usingMovementDistance)
        return (Mathf.Abs(startPos - transform.position.x) > movementDist);
        return false;
    }

    private void ResetMaxDistX()
    {
        if (usingMovementDistance && ReachedMaxDistance())
        {
            transform.position = new Vector3(transform.position.x > startPos ? startPos + movementDist : startPos - movementDist, transform.position.y, transform.position.z);
        }
    }

    public void BecomeAlert()
    {
        if (!alert)
        {

            alert = true;
            Speed = Speed * 2f;
            Debug.Log(Speed);
            postTime = postTime / 2f;
            alertMarker.SetActive(true);
            StartCoroutine(CalmDown());
        }

    }
    IEnumerator CalmDown()
    {
        yield return new WaitForSeconds(5);
        alert = false;
        Speed = Speed / 2f;

        postTime = postTime * 2f;
        alertMarker.SetActive(false);
    }
}
