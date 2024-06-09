using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pullingScript : MonoBehaviour
{
    [SerializeField] float collectingRange;
    [SerializeField] float attractionForce;
    Vector3 pullPos;
    private void OnTriggerEnter(Collider other)
    {       //Logging.Log($"{other.gameObject.name}");
        if (other.gameObject.layer == GameConstant.MagneticLayer)
        {
            PullObject(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
            if (other.gameObject.layer == GameConstant.MagneticLayer)
            {
                PullObject(other);
            }
        }
    }
    private void PullObject(Collider other)
    {
        if (other.gameObject.layer==GameConstant.MagneticLayer&&
            Physics.OverlapSphere(transform.position,collectingRange).Contains(other)) 
        {
            pullPos = transform.position;
            Vector3 dir = (pullPos - other.transform.position).normalized;
            other.attachedRigidbody.AddForce(dir * attractionForce, ForceMode.Impulse);
        }

        //pullPos = transform.position;
        //Vector3 dir = (pullPos - other.transform.position).normalized;
        //other.gameObject.TryGetComponent<Collider>(out Collider component);
        //component.attachedRigidbody.AddForce(dir * 10f, ForceMode.Impulse);
    }
}
