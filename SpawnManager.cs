using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Creates gameobjects that can be assigned prefabs in the inspector
    [SerializeField]
    private GameObject _enemySpawn;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerups;




    // Variable to set condition of the spawn routine and check for player death
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        //Initializes the coroutine for the method below upon startup of the game
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    //Coroutine controlling the spawn behaviour
    private IEnumerator SpawnEnemyRoutine()
    {
        //Creates delay for enemies to start spawning
        yield return new WaitForSeconds(2.0f);

        while (_stopSpawning == false)
        {
            //Sets the spawn position of new enemy objects on a random x value and starts them at top of screen
            Vector3 spawnPosition = new Vector3(Random.Range(-9.0f, 9.0f), 7.0f, 0);

            //Creates new enemy objects with enemy prefab at above spawn position variable
            GameObject newEnemy = Instantiate(_enemySpawn, spawnPosition, Quaternion.identity);

            //Sets the parent transform to a child object of the spawn manager for tidyness within the Unity Scene UI
            newEnemy.transform.parent = _enemyContainer.transform;

            //Determines the spawn rate of enemy objects and how often the coroutine is executed
            yield return new WaitForSeconds(2.5f);
        }
    }

    //Couroutine controlling spawn of the powerups
    private IEnumerator SpawnPowerupRoutine()
    {
        //Creates delay for powerups to spawn
        yield return new WaitForSeconds(2.0f);

        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9.0f, 9.0f), 7.0f, 0);

            //Randomly chooses a powerup from the array to spawn at the chosen interval
            int randomPowerup = Random.Range(0, 3);

            Instantiate(_powerups[randomPowerup], spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(12.0f, 15.0f));

        }
    }

    public void OnPlayerDeath()
    {
        // Sets the spawn variable to true, which stops the coroutine for spawning enemies when the player dies and calls this method
        _stopSpawning = true;
    }
}
