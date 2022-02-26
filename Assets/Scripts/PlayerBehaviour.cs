using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

namespace ShootRun
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerController _controller;
        // Start is called before the first frame update
        void Awake()
        {
            _controller = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IObstacle>(out var obs))
            {
                Fail();
            }
        }

        private void Fail()
        {
            Debug.Log("Fail");
        }
    }
}

