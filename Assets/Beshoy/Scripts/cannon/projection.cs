using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projection : MonoBehaviour
{
    [SerializeField] Transform firePoint; // Transform for projectile spawn location
    [SerializeField] GameObject projectilePrefab; // Prefab of the projectile to be fired
    [SerializeField] float gravity; // Gravity value affecting the projectile (adjust based on your scene)

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameConstant.PlayerLayer)
        {
            FireCannon(other.transform.localPosition);
        }
    }
    public void FireCannon(Vector3 targetPosition)
     {
        // Get cannon position from the firePoint transform
        Vector3 cannonPosition = firePoint.position;

        // Get target position from the targetTransform
        Vector3 targettPosition = targetPosition;

        // Calculate direction vector from cannon to target
        Vector3 direction = targetPosition - cannonPosition;

        // Calculate distance between cannon and target
        float distance = Vector3.Distance(cannonPosition, targetPosition);

        // Calculate a fixed launch force (adjust as needed)
        float launchForce = 10000.0f; // Adjust launch force based on your projectile and scene

        // Calculate a fixed launch angle for upward trajectory (adjust as needed)
        float launchAngle = 45.0f; // Adjust launch angle for desired trajectory

        // Rotate firePoint to look at target
        firePoint.LookAt(targetPosition, Vector3.up);

        // Calculate launch velocity based on distance and launch force
        float launchVelocity = Mathf.Sqrt(2f * launchForce / distance);

        // Instantiate projectile at firePoint with calculated velocity
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = direction.normalized * launchVelocity;
    }
}
