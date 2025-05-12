using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        AsyncLoadManager.Instance.LoadScene(sceneName);
    }
}
