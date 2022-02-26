using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float _speed;
    // Update is called once per frame
    void Update()
    {
        transform.position += _speed * Time.deltaTime * Vector3.forward;
    }

    public void Shoot(float speed)
    {
        _speed = speed;
        Destroy(gameObject, 3f);
    }
    
}
