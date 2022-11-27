using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : Actor
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] moveSound;
    [SerializeField] AudioClip alertSound;
    [SerializeField] AudioClip caught;
    bool walkPlaying = false;

    private float rayLength = .75f;
    private float rayX = 1;
    private float wallX = .7f;
    public float offSet = .55f;
    private LayerMask groundLayer;
    private float groundRay = 0.55f;
    public bool isOnGround = false;
    private LayerMask IgnoreMe;

    bool playerFound;
    private GameObject player;

    [SerializeField] private float speed = 1f;
    public float detectDistance = 5f;
    [SerializeField] float postTime;
    [SerializeField] float wallPostTime;
    [SerializeField] public bool posted = false;
    [SerializeField] private bool gameActive;
    [SerializeField] GameObject alertMarker;
    [SerializeField] public bool facingRight;
    [SerializeField] public bool usingMovementDistance = false;
    public float startPos;
    [SerializeField][Range(0,10)] public float movementDist;
    [SerializeField]public bool alert = false;
    public bool isActive = true;

    public bool distracted = false;
    public bool becomeAlert = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
        alertMarker = transform.Find("AlertMarker")?.gameObject;
        startPos = transform.position.x;
        Speed = speed;
        //InitDirection();
        groundLayer = LayerMask.GetMask("Ground");
        IgnoreMe = LayerMask.GetMask("Interactable");
        facingRight = transform.localScale.x < 0;
    }

    // Update is called once per frame
    void Update()
    {
        facingRight = transform.localScale.x < 0;
        GetIsGameActive();
        if (gameActive && isActive  && !distracted && isOnGround)
        {
            Move();
            DetectPlayer();
        }
        if(becomeAlert)
            posted = true;
    }

    private void FixedUpdate()
    {
        DetectGround();
    }
    public override void Move()
    {
        DetectEdge();
        if (!posted) {
            transform.Translate(Speed * Time.deltaTime * Vector3.right);
            if (!walkPlaying && !alert)
                StartCoroutine(PlayWalkingSound());
            if (!walkPlaying  && alert)
                StartCoroutine(PlayRunningSound());
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
            audioSource.PlayOneShot(alertSound);
            audioSource.PlayOneShot(caught);
            GameManager.Instance.SetKey("");
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
            facingRight = transform.localScale.x < 0;
            detectDistance = -detectDistance;
        }
    }
    public void InitDirection()
    {
        if (facingRight)
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
            audioSource.PlayOneShot(alertSound);
            StartCoroutine(SuddenShock());
            if ((facingRight && player.transform.position.x < transform.position.x) ||
                (!facingRight && player.transform.position.x > transform.position.x))
            {
                StartCoroutine(TurnAround(1f));
            }

            Speed = Speed * 2f;
            //postTime = postTime / 2f;
            alertMarker.SetActive(true);
            StartCoroutine(CalmDown());
        }

    }

    IEnumerator CalmDown()
    {
        yield return new WaitForSeconds(10);
        alert = false;
        Speed /= 2f;

        //postTime = postTime * 2f;
        alertMarker.SetActive(false);
    }

    private IEnumerator BeingDistracted(float distractionTime)
    {
        distracted = true;
        yield return new WaitForSeconds(distractionTime);
        distracted = false;
    }

    public void BecomeDistracted(float distractionTime)
    {
        StartCoroutine(BeingDistracted(distractionTime));
    }

    IEnumerator PlayRunningSound()
    {
        walkPlaying = true;
        AudioClip walkingClip = moveSound[Random.Range(0, moveSound.Length)];
        audioSource.PlayOneShot(walkingClip);

        yield return new WaitForSeconds(.35f);

        walkPlaying = false;
    }

    IEnumerator PlayWalkingSound()
    {
        walkPlaying = true;
        AudioClip walkingClip = moveSound[Random.Range(0, moveSound.Length)];
        audioSource.PlayOneShot(walkingClip);

        yield return new WaitForSeconds(.7f);

        walkPlaying = false;
    }

    IEnumerator SuddenShock()
    {
        becomeAlert = true;
        yield return new WaitForSeconds(2);
        becomeAlert = false;
        posted = false;
    }

    private void DetectGround()
    {
        //draws ray to ground
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, groundRay, groundLayer);
        Debug.DrawRay(transform.position, direction * groundRay, Color.yellow);

        isOnGround = hit.collider != null;
    }
}
