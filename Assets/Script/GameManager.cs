using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _congratsCanvas;

    private bool _gameEnded = false; // ✅ Cegah double trigger

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _congratsCanvas.SetActive(false);
        _gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1f;
    }

    public void GameOver()
    {

        if (_gameEnded) return;
        _gameEnded = true;
        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Congrats()
    {
        StarCoinManager.instance.GiveStarCoin(1); // ✅ Kasih StarCoin

        // ✅ Tidak set _gameEnded = true, biar game bisa lanjut setelah continue
        _congratsCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueGame() // ✅ Method baru untuk tombol Continue
    {
        _congratsCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public bool IsGameEnded() // ✅ Untuk cek di FlappyBirdMovement
    {
        return _gameEnded;
    }

    public void RestartGame()
    {
        _gameEnded = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
