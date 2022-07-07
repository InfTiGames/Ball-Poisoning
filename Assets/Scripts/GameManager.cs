using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsGameActive;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winGamePanel;
    private PlayerController _player;
    public GameObject _goButton;

    public static GameManager singleton { get; private set; }

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        _player = PlayerController.singleton;
    }

    public void StartGame()
    {
        IsGameActive = true;    
        _player.SizeBar.gameObject.SetActive(true);
        _goButton.SetActive(true);
    }

    public void GameOver()
    {
        SetUI();
        _gameOverPanel.SetActive(true);
    }

    public void WinGame()
    {
        SetUI();
        _winGamePanel.SetActive(true);
    }

    private void SetUI()
    {
        IsGameActive = false;
        _player.SizeBar.gameObject.SetActive(false);
        _goButton.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}