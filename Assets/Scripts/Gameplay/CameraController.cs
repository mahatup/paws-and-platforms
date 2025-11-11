using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cat;

    private Vector3 _movementPos;

    private void Awake()
    {
        if (!_cat)
        {
            _cat = FindObjectOfType<Cat>().transform;
        }
    }
    private void Update()
    {
        if (GameManager.Instance.State != GameState.Playing)
        {
            return;
        }

        _movementPos = _cat.position;
        _movementPos.z = -10f;

        transform.position = Vector3.Lerp(transform.position, _movementPos, Time.deltaTime);
    }
}

