using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupCaller : MonoBehaviour
{
    [SerializeField]Enemy backupEnemy;
    [SerializeField] Enemy spawnedEnemy;
    Enemy baseEnemy;
    PlayerController player;

    [SerializeField] float offset = 2f;
    [SerializeField] bool backupSpawned = false;
    [SerializeField] float despawnTime = 5f;
    [SerializeField] float readyTime = 1f;

    [SerializeField] AudioClip spawnSound;
    [SerializeField] AudioClip despawnSound;
    [SerializeField] AudioClip spawnSound2;
    [SerializeField] AudioClip callBackupSound;
    private GameObject smokeBomb;
    private ParticleSystem smokeBombPS;
    private bool callingBackup = false;

    private void Start()
    {
        smokeBomb = transform.Find("Smoke Bomb")?.gameObject;
        smokeBombPS = smokeBomb.GetComponent<ParticleSystem>();
        player = FindObjectOfType<PlayerController>();
        baseEnemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (baseEnemy.alert && !backupSpawned)
        {
            StartCoroutine(CallBackup());
        }
        if(callingBackup)
            baseEnemy.posted = true;
    }
    private IEnumerator CallBackup()
    {
        backupSpawned = true;
        baseEnemy.posted = true;
        callingBackup = true;
        GameManager.Instance.PlaySound(spawnSound2);
        yield return new WaitForSeconds(2);
        callingBackup = false;
        GameManager.Instance.PlaySound(callBackupSound);
        StartCoroutine(SpawnBackup(backupEnemy));
    }
    
    private IEnumerator SpawnBackup(Enemy enemy)
    {
        yield return new WaitForSeconds(2f);
        //baseEnemy.posted = false;
        Vector3 SpawnPointOffset = player.transform.position.x < transform.position.x ? new Vector3(offset, 0, 0): new Vector3(-offset, 0, 0);
        if (spawnedEnemy == null)
        {
            spawnedEnemy = Instantiate(enemy, transform.position + SpawnPointOffset, enemy.transform.rotation);
            spawnedEnemy.isActive = false;
        }

        else
        {
            spawnedEnemy.gameObject.SetActive(true);
            spawnedEnemy.isActive = false;
            spawnedEnemy.transform.position = transform.position + SpawnPointOffset;
        }
        spawnedEnemy.startPos = spawnedEnemy.transform.position.x;
        smokeBomb.transform.position = spawnedEnemy.transform.position;
        smokeBomb.SetActive(true);
        GameManager.Instance.PlaySound(spawnSound);
        GameManager.Instance.PlaySound(spawnSound2);
        smokeBombPS.Play();
        StartCoroutine(ReadyEnemy());
        StartCoroutine(UnReadyEnemy());
        StartCoroutine(PrepareDespawn());

        FinishCall();

    }

    private void DespawnBackup()
    {
        backupSpawned = false;
        GameManager.Instance.PlaySound(spawnSound2);
        GameManager.Instance.PlaySound(despawnSound);
        smokeBomb.transform.position = spawnedEnemy.transform.position;
        smokeBombPS.Play();
        spawnedEnemy.gameObject.SetActive(false);

    }

    IEnumerator PrepareDespawn()
    {
        yield return new WaitForSeconds(despawnTime);
        DespawnBackup();
    }

    IEnumerator ReadyEnemy()
    {
        yield return new WaitForSeconds(readyTime);
        spawnedEnemy.isActive = true;
    }

    IEnumerator UnReadyEnemy()
    {
        yield return new WaitForSeconds(despawnTime - 1f);
        spawnedEnemy.isActive = false;
    }

    void FinishCall()
    {
        callingBackup = false;
        baseEnemy.posted = false;
    }
}
