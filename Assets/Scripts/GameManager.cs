using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private LevelManager _levelManager;
    private UIManager _uiManager;
    private PlayerMovement _playerMovement;
    private ArrowManager _arrowManager;


    private int _level = 1;
    private bool _levelPassed = false;
    private bool _levelCompleted = false;
    private int _coinValue = 0;
    private int _finishCollectedCoin = 0;

    private int _arrowUpgrade = 1;
    private int _arrowUpgradeCost = 300;
    private int _coinUpgrade = 1;
    private int _coinUpgradeCost = 300;





    private void Awake() {
        #region  Singleton
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        #endregion


    }



    public void LevelCompleted() {
        if (_levelCompleted) return;

        _levelCompleted = true;
        _uiManager.LevelCompleted();

        _levelManager.Stop();
        _playerMovement.Stop();
        _level++;
    }

    public void LevelPassed() {
        _playerMovement.Stop();
        _levelManager.SpeedUp(2f);
        _uiManager.LevelPassed();

        _levelPassed = true;
    }

    public void LevelFailed() {
        _uiManager.LevelFailed();

        _levelManager.Stop();
    }

    public void RestartLevel() {
        _finishCollectedCoin = 0;
        _levelPassed = false;
        _levelCompleted = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel() {
        AddCoinUpdateUI(_finishCollectedCoin);
        _finishCollectedCoin = 0;
        _levelCompleted = false;
        _levelPassed = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    public void AddCoinUpdateUI(int value) {
        _coinValue += value;
        _uiManager.UpdateCoin(_coinValue);
    }

    public void AddFinishCoin(int value) {
        _finishCollectedCoin += value;
    }


    public void ArrowUpgrade() {
        if (_coinValue < _arrowUpgradeCost) return;

        _coinValue -= _arrowUpgradeCost;
        _uiManager.UpdateCoin(_coinValue);
        _arrowManager.AddArrow(1);
        _arrowUpgrade++;
        _arrowUpgradeCost = _arrowUpgrade * 300;

        UpgradesUpdateUI();
    }

    public void CoinUpgrade() {
        if (_coinValue < _coinUpgradeCost) return;

        _coinValue -= _coinUpgradeCost;
        _uiManager.UpdateCoin(_coinValue);
        _coinUpgrade++;
        _coinUpgradeCost = _coinUpgrade * 300;

        UpgradesUpdateUI();
    }

    public void UpgradesUpdateUI() {
        _uiManager.ArrowUpgradeUpdateUI(_arrowUpgrade, _arrowUpgradeCost);
        _uiManager.CoinUpgradeUpdateUI(_coinUpgrade, _coinUpgradeCost);
    }


    public void StartGame() {
        _levelManager.Begin();
        _playerMovement.Begin();
    }



    public void SetPlayerMovement(PlayerMovement playerMovement) {
        _playerMovement = playerMovement;
    }

    public void SetUIManager(UIManager uiManager) {
        _uiManager = uiManager;
    }

    public void SetLevelManager(LevelManager levelManager) {
        _levelManager = levelManager;
    }

    public void SetArrowManager(ArrowManager arrowManager) {
        _arrowManager = arrowManager;
    }


    public bool IsLevelCompleted { get { return _levelCompleted; } }
    public int Coin { get { return _coinValue; } }
    public int FinishCollectedCoin { get { return _finishCollectedCoin; } }
    public int Level { get { return _level; } }
    public bool IsLevelPassed { get { return _levelPassed; } }

    public int GetCoinUpgrade() { return _coinUpgrade; }
    public int GetArrowUpgrade() { return _arrowUpgrade; }

}
