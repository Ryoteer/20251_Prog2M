using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class AsyncLoadManager : MonoBehaviour
{
    #region Singleton
    public static AsyncLoadManager Instance;

    private void Awake()
    {
        if (!Instance) //-> Instance == null
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Image _loadingBarBG;
    [SerializeField] private Image _loadingBarFill;
    [SerializeField] private RawImage _loadingBarImage;

    private bool _isSceneLoading = false;

    private void Start()
    {
        _loadingBarImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);        

        _loadingBarBG.gameObject.SetActive(false);
        _loadingBarFill.enabled = false;
        _loadingBarImage.enabled = false;
        _loadingText.enabled = false;
    }

    public void LoadScene(string sceneName)
    {
        if (!_isSceneLoading)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        _isSceneLoading = true;

        _loadingBarBG.gameObject.SetActive(true);
        _loadingBarFill.enabled = true;
        _loadingBarImage.enabled = true;
        _loadingText.enabled = true;

        _loadingBarFill.color = Color.red;

        _loadingText.text = $"Loading...";

        float t = 0.0f;

        while(t < 1.0f)
        {
            t += Time.deltaTime / 0.5f;

            _loadingBarImage.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0.0f, 1.0f, t));

            yield return null;
        }

        _loadingBarImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        t = 0.0f;

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;

        while(asyncOp.progress < 0.9f)
        {
            _loadingBarFill.fillAmount = asyncOp.progress / 0.9f;

            yield return null;
        }

        _loadingBarFill.fillAmount = 1.0f;
        _loadingBarFill.color = Color.green;
        _loadingText.text = $"Press any key to continue.";

        while (!Input.anyKey)
        {
            yield return null;
        }

        asyncOp.allowSceneActivation = true;

        _loadingBarBG.gameObject.SetActive(false);
        _loadingBarFill.enabled = false;        
        _loadingText.enabled = false;

        t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / 0.5f;

            _loadingBarImage.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(1.0f, 0.0f, t));

            yield return null;
        }

        _loadingBarImage.enabled = false;

        _isSceneLoading = false;
    }
}
