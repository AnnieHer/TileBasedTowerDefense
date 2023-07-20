using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _speed, _smoothing;
    [SerializeField] private Vector2 _positionLimit;
    [SerializeField] private Transform _cameraRoot;
    private float _targetPosition;
    private float _input;
    [SerializeField] private float y;
    public static CameraRotation Instance;
    private void Awake() {
        Instance = this;
        _targetPosition = transform.position.y;
        y = transform.position.y;
    }
    private void HandleInput() {
        _input = Input.mouseScrollDelta.y;
    }
    private void Zoom() {
        if (_input != 0)
        _targetPosition = transform.position.y - _input * _speed;
        
        y = Mathf.Lerp(y, _targetPosition, Time.deltaTime * _smoothing);
        y = Mathf.Clamp(y, _positionLimit.x, _positionLimit.y);

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        transform.LookAt(_cameraRoot);
    }
    private void Update() {
        HandleInput();
        Zoom();
    }
    public void SetNewLimit(float limitMax) {
        Debug.Log(_positionLimit.y + " new: " + limitMax*5);
        _positionLimit.y = limitMax * 5;
    }
}
