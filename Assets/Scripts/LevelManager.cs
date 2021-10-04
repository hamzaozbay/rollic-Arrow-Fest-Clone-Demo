using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] private float _speed = 4f;

    private bool _canMove = true;



    private void Start() {
        GameManager.instance.SetLevelManager(this);

        Stop();
    }


    private void Update() {
        if (!_canMove) return;

        transform.position += Vector3.back * _speed * Time.deltaTime;
    }


    public void SpeedUp(float multiplier) {
        _speed *= multiplier;
    }


    public void Stop() {
        _canMove = false;
    }

    public void Begin() {
        _canMove = true;
    }


}
