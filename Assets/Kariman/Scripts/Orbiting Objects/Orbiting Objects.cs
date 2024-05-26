using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingObjects : MonoBehaviour
{
    public GameObject player;  // Reference to the player object
    public GameObject objectPrefab;  // Prefab for the rotating objects
    public int maxObjects = 5;  // Maximum number of objects to rotate
    public float radius = 5.0f;  // Radius of the circle around the player
    public float rotationSpeed = 50.0f;  // Speed of rotation

    private GameObject[] rotatingObjects;  // Array to hold the rotating objects

    void Start()
    {
        rotatingObjects = new GameObject[maxObjects];

        // Initialize the rotating objects
        for (int i = 0; i < maxObjects; i++)
        {
            GameObject newObj = Instantiate(objectPrefab);
            newObj.transform.parent = player.transform;  // Set the player as the parent
            newObj.transform.localPosition = Vector3.zero;
            rotatingObjects[i] = newObj;
        }

        PositionObjects();
    }

    void Update()
    {
        // Rotate the objects around the player
        for (int i = 0; i < maxObjects; i++)
        {
            rotatingObjects[i].transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void PositionObjects()
    {
        // Position the objects at equal angles around the player
        float angleStep = 360.0f / maxObjects;

        for (int i = 0; i < maxObjects; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float z = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            rotatingObjects[i].transform.localPosition = new Vector3(x, 0, z);
        }
    }
}
