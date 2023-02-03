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
    [SerializeField]
    private GameObject worldBoundary;
    [SerializeField]
    private float spawnStartTime;

    private float timeBetweenSpawn;
    private List<GameObject> currentFood;

    private Vector2 minPoint;
    private Vector2 maxPoint;

    private void Start() {
        timeBetweenSpawn = spawnStartTime;
        currentFood = new List<GameObject>();
        PolygonCollider2D _polygonCollider = worldBoundary.GetComponent<PolygonCollider2D>();
        foreach (var point in _polygonCollider.points)
        {
            minPoint.x = Mathf.Min(minPoint.x, point.x);
            minPoint.y = Mathf.Min(minPoint.y, point.y);
            maxPoint.x = Mathf.Max(maxPoint.x, point.x);
            maxPoint.y = Mathf.Max(maxPoint.y, point.y);
        }
    }

    private void Update() {
        if (timeBetweenSpawn <= 0) {
            if (currentFood.Count > 0) {
                currentFood = currentFood.Where(val => val != null).ToList();
                foreach (var item in currentFood)
                {
                    Vector2 position = item.transform.position;
                    if (position.x < minPoint.x || position.y < minPoint.y || position.x > maxPoint.x || position.y > maxPoint.y) {
                        Destroy(item);
                    }
                }
            }

            if (currentFood.Count < maxSpawns) {
                int foodInd = Random.Range(0, food.Length);
                int positionInd = Random.Range(0, spawnPoints.Length);
                currentFood.Add(Instantiate(food[foodInd], spawnPoints[positionInd].transform.position, Quaternion.identity));
            }

            timeBetweenSpawn = spawnStartTime;
        } else {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }
}
