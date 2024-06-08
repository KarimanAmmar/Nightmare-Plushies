using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    Transform targetPoint;
    [SerializeField] float speed;
    WaitForSeconds waitTime;

    private void Start()=> waitTime = new WaitForSeconds(2);
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
