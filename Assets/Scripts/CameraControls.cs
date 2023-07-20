using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float _speed, _smoothing;

    private Vector3 _targetPosition;
    private Vector3 _input;
    public static CameraControls Instance;
    private void Awake() {
        _targetPosition = transform.position;
        Instance = this;
    }
    private void HandleInput() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 right = transform.right * x;
        Vector3 forward = transform.forward * z;

        _input = (forward + right).normalized;
    }
    private void Move() {
        Vector3 nextTargetPosition = _targetPosition + _input * _speed;
        _targetPosition = nextTargetPosition;
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothing);
    }
    private void Update() {
        HandleInput();
        Move();
    }
    public void Center(Vector3 center) {
        _targetPosition = center;
    }
}
