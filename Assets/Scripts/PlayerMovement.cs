using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float _speedModifier;
    private Touch _touch;

    private ArrowManager _arrowManager;
    private bool _canCollideArrowGate = true;
    private Camera _camera;
    [SerializeField] private bool _canMove = true;




    private void Start() {
        GameManager.instance.SetPlayerMovement(this);

        _camera = Camera.main;
        _arrowManager = GetComponent<ArrowManager>();

        Stop();
    }



    private void Update() {
        if (!_canMove) return;

        if (Input.touchCount > 0) {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved) {
                transform.position += new Vector3(_touch.deltaPosition.x * (_speedModifier / 10f) * Time.deltaTime, 0, 0f);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.75f, 1.75f), transform.position.y, transform.position.z);

                transform.localScale = new Vector3(1f - 0.333f * Mathf.Abs(transform.position.x), 1f, 1f);
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("ArrowGate") && _canCollideArrowGate) {
            _canCollideArrowGate = false;
            ArrowGate arrowGate = other.gameObject.GetComponent<ArrowGate>();

            Process(arrowGate);
        }
        else if (other.gameObject.CompareTag("Man") && !GameManager.instance.IsLevelCompleted) {
            FinishMan man = other.gameObject.GetComponent<FinishMan>();

            if (!GameManager.instance.IsLevelPassed) {
                GameManager.instance.AddCoinUpdateUI(man.GetCoinValue() + (GameManager.instance.GetCoinUpgrade() * 100));
            }
            else {
                GameManager.instance.AddFinishCoin(man.GetCoinValue() + (GameManager.instance.GetCoinUpgrade() * 100));
            }

            _arrowManager.SubtractArrow(man.GetValue());

            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Finish")) {
            GameManager.instance.LevelPassed();

            transform.DOMoveX(0f, 1f);
            transform.DOScaleX(5f, 1f);

            _camera.transform.DOMove(new Vector3(0f, 14f, -9f), 1f);
            _camera.transform.DORotate(new Vector3(30f, 0f, 0f), 1f);
        }
        else if (other.gameObject.CompareTag("Completed")) {
            GameManager.instance.LevelCompleted();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("ArrowGate")) {
            _canCollideArrowGate = true;
        }
    }



    private void Process(ArrowGate gate) {
        if (gate.GetProcess() == ArrowProcess.ADDITION) {
            _arrowManager.AddArrow(gate.GetValue());
        }
        else if (gate.GetProcess() == ArrowProcess.DIVISION) {
            _arrowManager.DivideArrow(gate.GetValue());
        }
        else if (gate.GetProcess() == ArrowProcess.MULTIPLICATION) {
            _arrowManager.MultiplyArrow(gate.GetValue());
        }
        else if (gate.GetProcess() == ArrowProcess.SUBTRACTION) {
            _arrowManager.SubtractArrow(gate.GetValue());
        }
    }


    public void Stop() {
        _canMove = false;
    }

    public void Begin() {
        _canMove = true;
    }


}
