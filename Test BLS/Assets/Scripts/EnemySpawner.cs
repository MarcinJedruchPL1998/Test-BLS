using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPlane;
    [SerializeField] float spawnEnemiesTime;
    [SerializeField] Vector2[] enemiesFlyPath;

    [SerializeField] GameplayManager gameplayManager;
    [SerializeField] GameObject canvas;

    Vector2 screenBoundary;

    private void Start()
    {
        CalculateEnemiesFlyCourse();
        StartCoroutine(SpawnEnemies());
    }
    public void CalculateEnemiesFlyCourse()
    {
        screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        for (int i = 0; i < 7; i++)
        {
            enemiesFlyPath[i] = new Vector2(screenBoundary.x + 1f, screenBoundary.y - (i + 2));
        }

    }

    IEnumerator SpawnEnemies()
    {
        int randomEnemyCount = Random.Range(2, 6);

        for (int i = 0; i < randomEnemyCount; i++)
        {
            int currentRandomEnemyPath = Random.Range(1, 6);
            int lastRandomEnemyPath = currentRandomEnemyPath;

            while (currentRandomEnemyPath == lastRandomEnemyPath)
            {
                currentRandomEnemyPath = Random.Range(1, 6);
            }

            GameObject enemy = Instantiate(enemyPlane, enemiesFlyPath[currentRandomEnemyPath], Quaternion.identity, canvas.transform);
            enemy.GetComponent<EnemyPlane>().gameplayManager = gameplayManager;
        }
        yield return new WaitForSeconds(spawnEnemiesTime);
        StartCoroutine(SpawnEnemies());
    }
}
