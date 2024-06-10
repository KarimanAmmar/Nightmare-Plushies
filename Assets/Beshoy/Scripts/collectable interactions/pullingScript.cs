using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingScript : MonoBehaviour
{
    [SerializeField] float collectingRange = 5f;
    [SerializeField] float attractionForce = 10f;

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collectingRange, 1 << GameConstant.MagneticLayer);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                PullObject(rb);
            }
        }
    }

    private void PullObject(Rigidbody rb)
    {
        Vector3 direction = (transform.position - rb.position).normalized;
        rb.MovePosition(Vector3.Lerp(rb.position, transform.position, attractionForce * Time.fixedDeltaTime));
    }
}
