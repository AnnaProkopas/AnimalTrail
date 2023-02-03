using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] food;
    [SerializeField]
    public Transform[] spawnPoints;
    [SerializeField]
    public int maxSpawns;

    public float spawnStartTime;
    private float timeBetweenSpawn;
    private List<GameObject> currentFood;

    private void Start() {
        timeBetweenSpawn = spawnStartTime;
        currentFood = new List<GameObject>();
    }

    private void Update() {
        if (timeBetweenSpawn <= 0) {
            if (currentFood.Count > 0) {
                currentFood = currentFood.Where(val => val != null).ToList();
            }

            if (currentFood.Count < maxSpawns) {
                int foodInd = Random.Range(0, food.Length);
                int positionInd = Random.Range(0, spawnPoints.Length);
                currentFood.Add(Instantiate(food[foodInd], spawnPoints[positionInd].transform.position, Quaternion.identity));
            }
            Debug.Log(currentFood[0]);

            timeBetweenSpawn = spawnStartTime;
        } else {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }
}
