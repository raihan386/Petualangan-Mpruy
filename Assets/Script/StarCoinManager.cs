using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StarCoinManager : MonoBehaviour
{
    public static StarCoinManager instance;

    [SerializeField] private TextMeshProUGUI _starCoinText;
    private const string STAR_COIN_KEY = "StarCoin";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ✅ Hanya jalan di Unity Editor, tidak di build game
    #if UNITY_EDITOR
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    #endif
    }

    private void Start()
    {
        UpdateUI();
    }

    // ✅ Reset saat keluar dari aplikasi (build game)
    private void OnApplicationQuit()
    {
        ResetStarCoin();
    }

    // ✅ Reset saat keluar dari Play Mode di Unity Editor
    #if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            ResetStarCoin();
            Debug.Log("StarCoin di-reset karena keluar dari Play Mode");
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }
    }
    #endif

    private void ResetStarCoin()
    {
        PlayerPrefs.SetInt(STAR_COIN_KEY, 0);
        PlayerPrefs.Save();
        UpdateUI();
        Debug.Log("StarCoin di-reset!");
    }

    public void GiveStarCoin(int amount)
    {
        int current = PlayerPrefs.GetInt(STAR_COIN_KEY, 0);
        current += amount;
        PlayerPrefs.SetInt(STAR_COIN_KEY, current);
        PlayerPrefs.Save();

        UpdateUI();
        Debug.Log($"Star Coin diberikan! Total: {current}");
    }

    public int GetStarCoin()
    {
        return PlayerPrefs.GetInt(STAR_COIN_KEY, 0);
    }

    private void UpdateUI()
    {
        if (_starCoinText != null)
            _starCoinText.text = "" + GetStarCoin().ToString();
    }
}