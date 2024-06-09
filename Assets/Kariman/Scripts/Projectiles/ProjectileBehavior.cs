using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    Transform targetPoint;
    [SerializeField] float speed;
    [SerializeField] int waitTimeNum;
    WaitForSeconds waitTime;
    private void Start()=> waitTime = new WaitForSeconds(waitTimeNum);
    private void Update() => Move();
    void Move()
    {
        if (targetPoint != null)
        {
            Vector3 direction = (targetPoint.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
    public void Initialize(Transform target, float moveSpeed)
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
