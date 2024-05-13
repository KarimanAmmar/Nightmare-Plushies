using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    [SerializeField] int _speed;
    private void Update() => move();
    void move() => transform.Translate(Vector3.right * _speed * Time.deltaTime);
}
