using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPlane;
    [SerializeField] float spawnEnemiesTime;
    [SerializeField] Vector2[] enemiesFlyPath;

    [SerializeField] GameObject canvas;

    [SerializeField] PlayerControll playerControll;

    Vector2 screenBoundary;

    int randomEnemyPath;
    int randomEnemiesCount;
    public List<int> randomNumbers = new List<int>();

    private void Start()
    {
        CalculateEnemiesFlyCourse();
        StartCoroutine(SpawnEnemies());
    }
    public void CalculateEnemiesFlyCourse()
    {
        //Calculate the fly path positions for enemies, depending on the screen size

        screenBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        for (int i = 0; i < 7; i++) //There are 5 paths, but first and last path are borders of the screen (1 - up border, 2,3,4,5,6 - fly paths, 7 - down border)
        {
            enemiesFlyPath[i] = new Vector2(screenBoundary.x + 1f, screenBoundary.y - (i + 2));
        }

    }

    IEnumerator SpawnEnemies()
    {
        randomEnemiesCount = Random.Range(2, 6); //How many enemies in one time interval
        
        for(int i = 1; i < 6; i++)
        {
            randomNumbers.Add(i); //List of available fly paths
        }

        for (int i = 0; i < randomEnemiesCount; i++)
        {
            //Randomize one fly path for one enemy, then remove the fly path from the list (to not repeating)

            int randomIndex = Random.Range(0, randomNumbers.Count);
            randomEnemyPath = randomNumbers[randomIndex];
            randomNumbers.Remove(randomEnemyPath);

            //Spawn enemy planes
            GameObject enemy = Instantiate(enemyPlane, enemiesFlyPath[randomEnemyPath], Quaternion.identity, canvas.transform);
            enemy.GetComponent<EnemyPlane>().playerControll = playerControll;
        }

        randomNumbers.Clear();
        
        yield return new WaitForSeconds(spawnEnemiesTime);
        StartCoroutine(SpawnEnemies());
    }

}
