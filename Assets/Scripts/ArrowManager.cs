using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowManager : MonoBehaviour {

    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _arrowPoolParent;
    [SerializeField] private int _currentArrowCount = 1;
    [SerializeField] private TextMeshProUGUI _arrowCountText;




    private void Awake() {
        for (int i = 0; i < _arrowPoolParent.childCount; i++) {
            _arrowPoolParent.GetChild(i).gameObject.SetActive(false);
        }
    }


    private void Start() {
        GameManager.instance.SetArrowManager(this);

        _currentArrowCount = GameManager.instance.GetArrowUpgrade();
        for (int i = 0; i < Mathf.Min(_arrowPoolParent.childCount, _currentArrowCount); i++) {
            _arrowPoolParent.GetChild(i).gameObject.SetActive(true);
        }

        _arrowCountText.text = _currentArrowCount.ToString();
    }



    public void AddArrow(int addValue) {
        if (_currentArrowCount >= _arrowPoolParent.childCount) {
            _currentArrowCount += addValue;
            _arrowCountText.text = _currentArrowCount.ToString();
            return;
        }

        for (int i = _currentArrowCount; i < Mathf.Min(_currentArrowCount + addValue, _arrowPoolParent.childCount); i++) {
            _arrowPoolParent.GetChild(i).gameObject.SetActive(true);
        }

        _currentArrowCount = _currentArrowCount += addValue;
        _arrowCountText.text = _currentArrowCount.ToString();
    }

    public void SubtractArrow(int subtractValue) {
        if (_currentArrowCount < 0) return;

        if (_currentArrowCount > _arrowPoolParent.childCount) {
            for (int i = _arrowPoolParent.childCount - 1; i >= Mathf.Max(_currentArrowCount - subtractValue, 0); i--) {
                _arrowPoolParent.GetChild(i).gameObject.SetActive(false);
            }

            _currentArrowCount -= subtractValue;
            _arrowCountText.text = _currentArrowCount.ToString();
            return;
        }


        for (int i = _currentArrowCount; i >= Mathf.Max(_currentArrowCount - subtractValue, 0); i--) {
            _arrowPoolParent.GetChild(i).gameObject.SetActive(false);
        }

        _currentArrowCount = Mathf.Max(_currentArrowCount - subtractValue, 0);
        _arrowCountText.text = _currentArrowCount.ToString();


        if (_currentArrowCount <= 0 && !GameManager.instance.IsLevelPassed) {
            GameManager.instance.LevelFailed();
        }
        else if (_currentArrowCount <= 0 && GameManager.instance.IsLevelPassed) {
            GameManager.instance.LevelCompleted();
        }
    }


    public void MultiplyArrow(int multiplyValue) {
        if (_currentArrowCount >= _arrowPoolParent.childCount) {
            _currentArrowCount *= multiplyValue;
            _arrowCountText.text = _currentArrowCount.ToString();
            return;
        }

        for (int i = _currentArrowCount; i < Mathf.Min(_currentArrowCount * multiplyValue, _arrowPoolParent.childCount); i++) {
            _arrowPoolParent.GetChild(i).gameObject.SetActive(true);
        }

        _currentArrowCount = _currentArrowCount *= multiplyValue;
        _arrowCountText.text = _currentArrowCount.ToString();
    }

    public void DivideArrow(int divideValue) {
        if (_currentArrowCount <= 0) return;

        for (int i = _currentArrowCount; i > Mathf.Max(_currentArrowCount / divideValue, 0); i--) {
            _arrowPoolParent.GetChild(i).gameObject.SetActive(false);
        }

        _currentArrowCount = Mathf.Max(_currentArrowCount / divideValue, 0);
        _arrowCountText.text = _currentArrowCount.ToString();
    }


    [ContextMenu("Create Arrows")]
    private void CreateArrows() {
        for (int i = 0; i < _arrowPoolParent.childCount; i++) {
            DestroyImmediate(_arrowPoolParent.GetChild(i));
        }

        for (int layer = 1; layer < 9; layer++) {
            for (int slice = 0; slice < layer * 7; slice++) {
                float angle = slice * Mathf.PI * 2 / (layer * 7);
                Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * (0.075f * layer);

                Vector3 desiredPos = new Vector3(pos.x, pos.y, pos.z + Random.Range(-0.075f, 0.075f));

                GameObject arrow = Instantiate(_arrowPrefab, _arrowPoolParent);
                arrow.transform.localPosition = desiredPos;
            }
        }
    }

}
