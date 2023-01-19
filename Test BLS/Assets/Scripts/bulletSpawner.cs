using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float spawnBulletTime;

    [SerializeField] GameObject canvas;

    void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation, canvas.transform);
        yield return new WaitForSeconds(spawnBulletTime);
        StartCoroutine(SpawnBullet());
    }
}
