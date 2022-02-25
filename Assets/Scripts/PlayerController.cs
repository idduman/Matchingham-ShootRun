using System.Collections;
using System.Collections.Generic;
using ShootRun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _steerSpeed = 5f;
    [SerializeField] private float _responsiveness = 10f;
    [SerializeField] private float _clampX = 3f;

    private bool _started;
    private float _offsetX;
    
    // Start is called before the first frame update
    void Start()
    {
        _started = false;
        OnLevelLoaded();
    }

    private void Update()
    {
        var pos = transform.position;
        pos.z += _moveSpeed * Time.deltaTime;
        pos.x = Mathf.Lerp(pos.x, _offsetX, _responsiveness * Time.deltaTime);
        transform.position = pos;
    }

    private void OnLevelLoaded()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        InputController.Instance.Pressed += OnPressed;
        InputController.Instance.Moved += OnMoved;
    }

    private void Unsunscribe()
    {
        InputController.Instance.Pressed -= OnPressed;
        InputController.Instance.Moved -= OnMoved;
    }
    
    private void OnPressed(Vector3 pos)
    {
        if (_started)
            return;

        _started = true;
    }

    private void OnMoved(Vector3 inputDelta)
    {
        _offsetX = Mathf.Clamp(_offsetX + inputDelta.x * _steerSpeed, -_clampX, _clampX);
    }
}