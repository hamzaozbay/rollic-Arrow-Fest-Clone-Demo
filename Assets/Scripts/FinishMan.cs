using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMan : MonoBehaviour {

    [SerializeField] private int _value;
    [SerializeField] private int _coinValue;


    public int GetValue() { return _value; }
    public int GetCoinValue() { return _coinValue; }

}
