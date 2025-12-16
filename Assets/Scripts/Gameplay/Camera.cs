using System;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _zOffset;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _arrivalThreshold;

    private Cat _cat;
    private Vector3 _velocity = Vector3.zero;
    private bool _cameraReady;

    public event Action CameraReady;

    public void Construct(Cat cat)
    {
        _cat = cat;
    }
    void Update()
    {
        Vector3 desiredPosition = new Vector3(_cat.Position.x + _offset.x, _cat.Position.y + _offset.y, _zOffset);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothTime);

        if (!_cameraReady && Vector3.Distance(transform.position, desiredPosition) < _arrivalThreshold)
        {
            _cameraReady = true;
            CameraReady?.Invoke();
        }
    }
}

