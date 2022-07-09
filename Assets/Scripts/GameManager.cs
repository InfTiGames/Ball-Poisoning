using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsGameActive;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _winGamePanel;   
    [SerializeField] GameObject _sizeBar;
    [SerializeField] GameObject _winFx;

    public static GameManager singleton { get; private set; }

    void Awake()
    {
        singleton = this;
    }

    public void StartGame()
    {
        IsGameActive = true;    
        _sizeBar.gameObject.SetActive(true);        
    }

    public void GameOver()
    {
        SetUI();
        _gameOverPanel.SetActive(true);
    }

    public void WinGame()
    {
        SetUI();
        GameObject winFx = Instantiate(_winFx, _winFx.transform.position, Quaternion.identity);
        Destroy(winFx, 1f);
        _winGamePanel.SetActive(true);
    }

    void SetUI()
    {
        IsGameActive = false;
        _sizeBar.gameObject.SetActive(false);       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}