using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Expose

    [SerializeField] Animator _animPlayer;
    [SerializeField] Transform _playerTransform;

    #endregion

    #region Unity Lyfecycle

    private void Start()
    {
        _playerMoney = GameObject.Find("Player").GetComponent<PlayerMoney>();

    }

    void Update()
    {
        Vector3 _mousePosition = Camera.main.ScreenToWorldPoint((Input.mousePosition));
        Debug.DrawRay(transform.position, _mousePosition, Color.yellow);
    }

    
    #endregion

    #region Methods

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        _playerMoney.Money.NbOfResource = 0;
        _playerMoney.NbOfKeys.NbOfResource = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisplayOptions()
    {

    }

    #endregion

    #region Private & Protected

    PlayerMoney _playerMoney;

    #endregion
}
