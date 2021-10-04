using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour {

    [SerializeField] private Transform _target;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Vector3 _offset;

    private Camera _camera;



    private void Start() {
        _camera = Camera.main;
    }


    private void Update() {
        Vector3 screenPos = _camera.WorldToScreenPoint(_target.position);
        transform.position = new Vector3(screenPos.x + _offset.x, screenPos.y + _offset.y);
    }

}
