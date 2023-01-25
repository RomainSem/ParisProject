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

    private void Awake()
    {
        
    }

    void Start()
    {
        
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
        SceneManager.LoadScene("Apartment");
    }

    #endregion

    #region Private & Protected

    #endregion
}
