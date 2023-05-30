using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _losePanel;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _escPanel;
    [SerializeField] GameObject _tutorialPanel;
    [SerializeField] GameObject _kickCircleCooldownOBJ;
    [SerializeField] Image _kickCircleCooldownIMG;
    [SerializeField] TextMeshProUGUI _nbActualBullets;
    [SerializeField] TextMeshProUGUI _nbTotalBullets;
    [SerializeField] GameObject _player;

    private void Awake()
    {
        //if (_enemy == null) return;
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Start()
    {
        _playerHealthScript = _player.GetComponent<PlayerHealth>();
        _simpleShootScript = _player.GetComponentInChildren<SimpleShoot>();
        _playerInputScript = GetComponent<PlayerInput>();
        _enemyInventoryScript = GameObject.Find("InventoryManager").GetComponent<EnemyInventory>();
        if (_enemy == null) return;
        _enemyBehaviourScript = _enemy.GetComponent<EnemyBehaviour>();
        StartCoroutine(ShowTutorial());

#if DEVELOPMENT_BUILD

        Debug.LogError("Force the build console open..."); 
        if (_playerHealthScript == null)
	    {
            Debug.Log("PLAYER HEALTH IS NULL");
	    }
#endif

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isEscMenuOpen)
            {
                _escPanel.SetActive(true);
                _isEscMenuOpen = true;
            }
            else
            {
                _escPanel.SetActive(false);
                _isEscMenuOpen = false;
            }
        }
        ReloadScene();
        UpdateNbBullets();
        //ShowLosePanel();
        if (!_playerInputScript.CanKick)
        {
            _isUpdatingKickCooldown = true;
        }
        //CursorUpdate();
        //ShowWinPanel();
        //if (_enemy == null) return;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void FixedUpdate()
    {
        if (_isUpdatingKickCooldown)
        {
            UpdateKickCooldown();
        }
    }

    private void UpdateNbBullets()
    {
        _nbActualBullets.text = _simpleShootScript.CurrentNbBullets.ToString();
        _nbTotalBullets.text = " / " + _simpleShootScript.CurrentNbBulletsInMagazine.ToString();
    }

    private void UpdateKickCooldown()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(new Vector3(_player.transform.position.x, _player.transform.position.y + 2.1f, _player.transform.position.z));
        _kickCircleCooldownOBJ.SetActive(true);
        _kickCircleCooldownIMG.color = Color.white;
        _kickCircleCooldownIMG.fillAmount += 1 / _playerInputScript.KickCooldownTime * Time.deltaTime;
        _kickCircleCooldownOBJ.transform.position = pos;
        if (_kickCircleCooldownIMG.fillAmount >= 0.99999999999f)
        {
            _kickCircleCooldownIMG.fillAmount = 0;
            _isUpdatingKickCooldown = false;
            _kickCircleCooldownOBJ.SetActive(false);
        }
    }

    private void CursorUpdate()
    {
        if (_enemyInventoryScript.IsLootMenuOpen || Input.GetButton("TAB"))
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    private void ShowWinPanel()
    {
        if (_enemyBehaviourScript.NbEnemies <= 0)
        {
            _winPanel.SetActive(true);
        }
    }

    private void ShowLosePanel()
    {
        if (_playerHealthScript.IsPlayerDead)
        {
            _losePanel.SetActive(true);
        }
    }

    private void ReloadScene()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator ShowTutorial()
    {
        _tutorialPanel.SetActive(true);
        yield return new WaitForSeconds(20f);
        _tutorialPanel.SetActive(false);
    }

    PlayerHealth _playerHealthScript;
    PlayerInput _playerInputScript;
    bool _isUpdatingKickCooldown;
    bool _isEscMenuOpen;
    EnemyBehaviour _enemyBehaviourScript;
    EnemyInventory _enemyInventoryScript;
    GameObject _enemy;
    SimpleShoot _simpleShootScript;
}
