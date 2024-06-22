using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reversPullingScript : MonoBehaviour
{
    // The speed at which the object will move towards the target
    public float speed = 5f;
    // Coroutine reference for stopping the movement when target exits trigger area
    private Coroutine moveCoroutine;
    [SerializeField] private Transform parentTransform;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is on the target layer
        if (other.gameObject.layer==GameConstant.MagneticLayer)
        {
           
            // Start moving towards the target object if not already moving
            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(MoveTowards(other.transform));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that exited the trigger is on the target layer
        if (other.gameObject.layer == GameConstant.MagneticLayer)
        {
            
            
            // Stop moving towards the target object
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
        }
    }

    private IEnumerator MoveTowards(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            // Calculate the direction to move towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Calculate the distance to move
            float distance = speed * Time.deltaTime;

            // Move the object towards the target
            parentTransform.position += direction * distance;

            // Yield until the next frame
            yield return null;
        }

        // Stop the coroutine once the target position is reached
        moveCoroutine = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.84f);
    }
}


