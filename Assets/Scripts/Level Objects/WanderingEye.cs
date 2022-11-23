using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEye : MonoBehaviour
{
    [SerializeField] AudioClip alertSound;
    [SerializeField] AudioClip caught;
    [SerializeField] float scanDistance = 45f;
    [SerializeField] float scanSpeed;
    [SerializeField] float postTime = 2f;
    [SerializeField] bool posted;
    [SerializeField] bool playerHit;
    private LayerMask ignoreMe;

    private float rotation;


    private void Start()
    {
        ignoreMe = LayerMask.GetMask("Enemy");
    }
    private void Update()
    {
        if (GameManager.Instance.gameIsActive)
        {
            DetectPlayer();
            CatchPlayer();
            ScanArea();
        }
    }

    void CatchPlayer()
    {
        if (playerHit)
        {
            Debug.Log("player found");
            //GameManager.Instance.PlaySound(alertSound);
            //GameManager.Instance.PlaySound(caught);
            GameManager.Instance.SetKey("");
            GameManager.Instance.SetIsGameActive(false);
            StartCoroutine(GameManager.Instance.ReloadScene(2f));
        }
    }

    private void DetectPlayer()
    {
        //draws ray downward
        Vector2 direction = -transform.up;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10f, ~ignoreMe);
        Debug.DrawRay(transform.position, direction * 10f, Color.yellow);

        playerHit = hit.collider != null && hit.collider.CompareTag("Player");
        Debug.Log(playerHit);
    }

    void ScanArea()
    {
        
        transform.Rotate(0f, 0f, scanSpeed * Time.deltaTime);
        
        if (ReachScanDistance())
        {
            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (rotation > 0 ? scanDistance : -scanDistance));
            scanSpeed = -scanSpeed;
        }
            
    }

    bool ReachScanDistance()
    {
        if (transform.eulerAngles.z <= 180f)
        {
            rotation = transform.eulerAngles.z;
        }
        else
        {
            rotation = transform.eulerAngles.z - 360f;
        }
        //the closer rotation is to scandistance, slow down
        //go back up approaching 0
        return (Mathf.Abs(rotation) > Mathf.Abs(scanDistance));
    }
}
