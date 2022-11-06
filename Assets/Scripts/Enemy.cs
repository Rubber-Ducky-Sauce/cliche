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

    [SerializeField] float detectDistance = 5f;
    private bool gameActive = true;
    [SerializeField] GameObject alertMarker;


    // Start is called before the first frame update
    void Start()
    {
        Speed = 1f;
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            wallX = -wallX;
            rayX = -rayX;
            offSet = -offSet;
            detectDistance = -detectDistance;
            Speed = -Speed;
        }
    }

    private void DetectPlayer()
    {
        //draws ray to ground
        Vector2 direction = new Vector2(detectDistance, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(offSet,0,0), direction, Mathf.Abs(detectDistance));
        Debug.DrawRay(transform.position + new Vector3(offSet, 0, 0), direction, Color.red);

        if (hit.collider != null  && hit.collider.tag == "Player" && !IsPlayerHiding(hit.collider.gameObject))
        {
            Debug.Log("Player Found! Straight to Jail!");
            gameActive = false;
            hit.collider.GetComponent<PlayerController>().gameActive = false;
            alertMarker.SetActive(true);
            StartCoroutine(GameManager.Instance.ReloadScene());
        }
           

    }

    private bool IsPlayerHiding(GameObject playerObject)
    {
        PlayerController player = playerObject.GetComponent<PlayerController>();
        return player.isHiding;
    }
}
