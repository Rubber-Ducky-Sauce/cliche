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

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        baseEnemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (baseEnemy.alert && !backupSpawned)
        {
            SpawnBackup(backupEnemy);
        }
    }

    private void SpawnBackup(Enemy enemy)
    {
        backupSpawned = true;
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
        GameManager.Instance.PlaySound(spawnSound);
        StartCoroutine(ReadyEnemy());
        StartCoroutine(UnReadyEnemy());
        StartCoroutine(PrepareDespawn());
    }

    private void DespawnBackup()
    {
        backupSpawned = false;
        GameManager.Instance.PlaySound(spawnSound);
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
}
