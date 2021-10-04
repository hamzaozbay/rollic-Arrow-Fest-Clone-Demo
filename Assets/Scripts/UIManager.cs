using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private TextMeshProUGUI _coinValue;
    [SerializeField] private GameObject _bgUI;
    [SerializeField] private GameObject _arrowCountUI;
    [SerializeField] private GameObject _levelCompletedUI;
    [SerializeField] private TextMeshProUGUI _collectedCoinText;
    [SerializeField] private GameObject _levelFailedUI;

    [Header("Upgrades UI")]
    [Space(20f)]
    [SerializeField] private TextMeshProUGUI _arrowUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI _arrowUpgradeCostText;
    [SerializeField] private TextMeshProUGUI _coinUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI _coinUpgradeCostText;




    private void Start() {
        GameManager.instance.SetUIManager(this);

        _levelName.text = "Level: " + GameManager.instance.Level.ToString();
        UpdateCoin(GameManager.instance.Coin);
        GameManager.instance.UpgradesUpdateUI();
    }


    public void LevelCompleted() {
        _bgUI.SetActive(true);
        _levelCompletedUI.SetActive(true);

        _collectedCoinText.text = GameManager.instance.FinishCollectedCoin.ToString() + " C";
    }

    public void LevelPassed() {
        _arrowCountUI.SetActive(false);
    }

    public void LevelFailed() {
        _bgUI.SetActive(true);
        _levelFailedUI.SetActive(true);
    }



    public void UpdateCoin(int value) {
        _coinValue.text = value + " C";
    }




    public void ArrowUpgradeButton() {
        GameManager.instance.ArrowUpgrade();
    }

    public void ArrowUpgradeUpdateUI(int level, int cost) {
        _arrowUpgradeLevelText.text = "Level: " + level.ToString();
        _arrowUpgradeCostText.text = cost.ToString() + " C";
    }

    public void CoinUpgradeButton() {
        GameManager.instance.CoinUpgrade();
    }

    public void CoinUpgradeUpdateUI(int level, int cost) {
        _coinUpgradeLevelText.text = "Level: " + level.ToString();
        _coinUpgradeCostText.text = cost.ToString() + " C";
    }


    public void StartGame() {
        GameManager.instance.StartGame();
    }


    public void NextLevelButton() {
        GameManager.instance.NextLevel();
    }

    public void RestartLevel() {
        GameManager.instance.RestartLevel();
    }




}
