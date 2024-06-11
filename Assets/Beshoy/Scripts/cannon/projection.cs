using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projection : MonoBehaviour
{
    [SerializeField] Transform firePoint; // Transform for projectile spawn location
    [SerializeField] Transform directionPoint; // Transform to control the direction of the projectile
    [SerializeField] GameObject projectilePrefab; // Prefab of the projectile to be fired
    [SerializeField] Transform target;
    [SerializeField] float maxDistance;
    [SerializeField] float launchAngle = 45.0f;

    private void Start()
    {
        StartCoroutine(Cannon());
    }
    IEnumerator Cannon()
    {
       
        FireProjectile(target.position);
        yield return new WaitForSeconds(3);
        StartCoroutine(Cannon());
    }
    public void FireProjectile(Vector3 targetPosition)
    {
        Vector3 cannonPosition = firePoint.position;
        Vector3 direction = targetPosition - cannonPosition;
        float distance = Vector3.Distance(cannonPosition, targetPosition);

        // Calculate the angle in radians
        float angle = launchAngle*(distance/maxDistance) * Mathf.Deg2Rad;

        // Calculate the horizontal distance
        float horizontalDistance = new Vector2(direction.x, direction.z).magnitude;

        // Calculate the initial velocity required to reach the target
        float g = Mathf.Abs(Physics.gravity.y);
        float initialVelocity = Mathf.Sqrt(horizontalDistance * g / Mathf.Sin(2 * angle));

        // Time to reach the target
        float timeOfFlight = horizontalDistance / (initialVelocity * Mathf.Cos(angle));

        // Vertical component of the initial velocity
        float verticalVelocity = initialVelocity * Mathf.Sin(angle);

        // Correct the forward direction to be horizontal
        direction.y = 0;
        direction.Normalize();

        // Rotate directionPoint to look at the target
        directionPoint.LookAt(targetPosition);

        // Calculate the initial velocity vector
        Vector3 initialVelocityVector = direction * initialVelocity;
        initialVelocityVector.y = verticalVelocity;

        // Instantiate projectile at firePoint with directionPoint's rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, directionPoint.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectileRb.isKinematic = false;
            projectileRb.velocity = initialVelocityVector; // Set the velocity
        }
        else
        {
            Debug.LogError("Projectile prefab does not have a Rigidbody component!");
        }
    }
}
