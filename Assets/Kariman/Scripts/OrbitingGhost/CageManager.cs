using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageManager : MonoBehaviour
{
    [SerializeField] GameObject cage;
    [SerializeField] GameObject ghost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.ProjectileTag))
        {
            cage.SetActive(false);
            ghost.SetActive(true);
        }
    }
}
