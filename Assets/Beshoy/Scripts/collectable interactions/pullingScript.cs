using System.Collections;
using UnityEngine;

public class PullingScript : MonoBehaviour
{
 
    [SerializeField] private float collectingRange = 5f;
    [SerializeField] private float attractionForce = 10f;
    [SerializeField] private float updateInterval = 0.1f; // Update every 0.1 seconds
    [SerializeField] private LayerMask magneticLayerMask; // Layer mask for the magnetic objects

    private Collider[] colliders;
    private const int maxColliders = 20; // Maximum number of colliders to check; adjust as needed

    private void Awake()
    {
        // Allocate the collider array once and reuse it
        colliders = new Collider[maxColliders];
    }

    private void OnEnable()
    {
        StartCoroutine(PullObjectsCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(PullObjectsCoroutine());
    }

    private IEnumerator PullObjectsCoroutine()
    {
        while (true)
        {
            // Non-allocating version of OverlapSphere
            int numColliders = Physics.OverlapSphereNonAlloc(transform.position, collectingRange, colliders, magneticLayerMask);

            for (int i = 0; i < numColliders; i++)
            {
                if (colliders[i].TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    PullObject(rb);
                }
            }

            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void PullObject(Rigidbody rb)
    {
        Vector3 direction = (transform.position - rb.position).normalized;
        Vector3 newPosition = Vector3.MoveTowards(rb.position, transform.position, attractionForce * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the collecting range sphere in the editor for debugging
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectingRange);
    }
}

