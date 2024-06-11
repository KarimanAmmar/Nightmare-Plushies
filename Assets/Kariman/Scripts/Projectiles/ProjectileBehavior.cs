using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    Transform targetPoint;
    [SerializeField] int speed;
    [SerializeField] int waitTimeNum = 3;
    WaitForSeconds waitTime;
    private void Start()=> waitTime = new WaitForSeconds(waitTimeNum);
    private void Update() => Move();
    void Move()
    {
        if (targetPoint != null)
        {
            Logging.Log("Target Not Null");
            Vector3 direction = (targetPoint.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        else
        {
            Logging.Log("Target Null");
        }
    }
    public void Initialize(Transform target, int moveSpeed)
    {
        targetPoint = target;
        speed = moveSpeed;
        StartCoroutine(DeactivateObject());
    }
    IEnumerator DeactivateObject()
    {
        yield return waitTime;
        this.gameObject.SetActive(false);
    }
}
