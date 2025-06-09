using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveLoadManager : MonoBehaviour
{
    [Header("Scene Management")]
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private Animation _doorAnimation;

    private bool _isSceneLoaded = false;
    public bool IsSceneLoaded { get { return _isSceneLoaded; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBehaviour>())
        {
            if (!_isSceneLoaded)
            {
                StartCoroutine(LoadSceneAdditive(_sceneToLoad));
            }
            else
            {
                UnloadScene(_sceneToLoad);
            }
        }
    }
    private void CloseDoorAnimation()
    {
        _doorAnimation.clip = _doorAnimation.GetClip("Close");

        _doorAnimation.Play();
    }

    private void OpenDoorAnimation(AsyncOperation asyncOp)
    {
        _doorAnimation.clip = _doorAnimation.GetClip("Open");

        _doorAnimation.Play();
    }

    private IEnumerator LoadSceneAdditive(string sceneName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        asyncOp.completed += OpenDoorAnimation;

        while (!asyncOp.isDone)
        {
            yield return null;
        }

        _isSceneLoaded = true;
    }

    private IEnumerator UnloadScene(string sceneName)
    {
        CloseDoorAnimation();

        yield return new WaitForSeconds(_doorAnimation.clip.length);

        AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(sceneName);  

        _isSceneLoaded = false;
    }
}
