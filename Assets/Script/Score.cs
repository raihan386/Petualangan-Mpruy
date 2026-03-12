using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif


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

        // ✅ Hanya jalan di Unity Editor, tidak di build game
        #if UNITY_EDITOR
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        #endif
    }

    // ✅ Method ini hanya ada saat di Unity Editor
    #if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // Dipanggil tepat sebelum Editor berhenti
        if(state == PlayModeStateChange.ExitingPlayMode)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.Save();
            Debug.Log("HighScore di-reset karena keluar dari Play Mode");

            // Unsubscribe agar tidak memory leak
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }
    }
    #endif
    
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
