using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowGate : MonoBehaviour {

    [SerializeField] private ArrowProcess _process;
    [SerializeField] private int _value;
    [SerializeField] private TextMeshProUGUI _text;



    private void Awake() {
        _text.text = GetProcessSymbol() + _value.ToString();
    }



    private string GetProcessSymbol() {
        if (_process == ArrowProcess.ADDITION) {
            return "+";
        }
        else if (_process == ArrowProcess.DIVISION) {
            return "÷";
        }
        else if (_process == ArrowProcess.MULTIPLICATION) {
            return "x";
        }
        else
            return "-";
    }



    public ArrowProcess GetProcess() { return _process; }
    public int GetValue() { return _value; }

}

public enum ArrowProcess {
    ADDITION,
    SUBTRACTION,
    MULTIPLICATION,
    DIVISION
}
