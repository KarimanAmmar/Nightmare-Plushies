using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingObjects : MonoBehaviour
{
    [SerializeField] GameObject[] rotatingGhosts;
    [SerializeField] GameObject player;
    [SerializeField] GameObject ghost;
    [SerializeField] GameEvent ghostClaimed;
    [SerializeField] int maxGhosts = 5;
    [SerializeField] int currentGhosts = 0;
    [SerializeField] float radius;
    [SerializeField] float rotationSpeed;

    private Vector3 lastPosition;
    private void OnEnable()
    {
        ghostClaimed.GameAction += PositionGhosts;
    }
    private void OnDisable()
    {
        ghostClaimed.GameAction -= PositionGhosts;
    }
    void Start()
    {
        lastPosition = transform.position;
        rotatingGhosts = new GameObject[maxGhosts];

        // Initialize the rotating objects
        for (int i = 0; i < maxGhosts; i++)
        {
            GameObject newObj = Instantiate(ghost);
            newObj.transform.parent = player.transform;
            newObj.transform.localPosition = Vector3.zero;
            rotatingGhosts[i] = newObj;
            newObj.SetActive(false);
        }
    }
    void Update()
    {
        // Rotate the objects around the player
        for (int i = 0; i < currentGhosts; i++)
        {
            rotatingGhosts[i].transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    void PositionGhosts()
    {
        if(currentGhosts < maxGhosts)
        {
            currentGhosts++;
        }

        float angleStep = 360.0f / currentGhosts;

        for (int i = 0; i < currentGhosts; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float z = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            rotatingGhosts[i].transform.localPosition = new Vector3(x, 0, z);
            rotatingGhosts[i].SetActive(true);
        }
    }
}
