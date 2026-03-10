using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Score : MonoBehaviour
{

    public static Score instance;

    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    private int _score;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _currentScoreText.text = _score.ToString() + " / 20";

        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();

    }

    private void UpdateHighScore()
    {
        if(_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            _highScoreText.text = _score.ToString();
        }
    }

    public void UpdateScore()
    {
        _score++;
        _currentScoreText.text = _score.ToString() + " / 20";
        UpdateHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
