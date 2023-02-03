using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private Car car;
    [SerializeField]
    private GameObject worldBoundary;
    [SerializeField]
    private Transform spawnPoint;

    
    private Car currentCar;
    private Vector2 maxPoint;

    // Start is called before the first frame update
    void Start()
    {
        PolygonCollider2D _polygonCollider = worldBoundary.GetComponent<PolygonCollider2D>();
        foreach (var point in _polygonCollider.points)
        {
            maxPoint.x = Mathf.Max(maxPoint.x, point.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCar == null) {
            currentCar = Instantiate(car, spawnPoint.position, Quaternion.identity);

        } else if (currentCar.transform.position.x  > maxPoint.x) {
            Destroy(currentCar.gameObject);
        }
    }
}
