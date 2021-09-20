using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void LoadLevel()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
}
