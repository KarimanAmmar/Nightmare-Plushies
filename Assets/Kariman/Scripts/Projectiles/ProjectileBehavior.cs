using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    Transform targetPoint;
    Vector3 direction;
    [SerializeField] int speed;
    [SerializeField] int waitTimeNum = 3;
    WaitForSeconds waitTime;

    private void Start()
    {
        waitTime = new WaitForSeconds(waitTimeNum);
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (direction != Vector3.zero)
        {
            //Logging.Log("Moving in Direction: " + direction);
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        else
        {
            //Logging.Log("No Direction Set");
        }
    }

    public void Initialize(Transform target, int moveSpeed)
    {
        targetPoint = target;
        speed = moveSpeed;
        direction = (targetPoint.position - transform.position).normalized;
        StartCoroutine(DeactivateObject());
    }

    IEnumerator DeactivateObject()
    {
        yield return waitTime;
        this.gameObject.SetActive(false);
    }
   
}
