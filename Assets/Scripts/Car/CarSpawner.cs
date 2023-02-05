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
    
    private Car? currentCar = null;
    private float maxPointX;

    void Start()
    {
        PolygonCollider2D _polygonCollider = worldBoundary.GetComponent<PolygonCollider2D>();
        foreach (var point in _polygonCollider.points)
        {
            maxPointX = Mathf.Max(maxPointX, point.x);
        }

        maxPointX += 3.0f;
    }

    void Update()
    {
        if (currentCar == null || currentCar.transform.position.x  > maxPointX) 
        {
            if (currentCar != null) Destroy(currentCar.gameObject);
            currentCar = Instantiate(car, spawnPoint.position, Quaternion.identity);
        }
    }
}
