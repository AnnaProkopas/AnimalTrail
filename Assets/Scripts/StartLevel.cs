using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad (gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        gameObject.SetActive (false);
    }

    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        if (scene. name == "HomeScene") {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy (gameObject);
        } else {
            gameObject.SetActive(true);
        }
    }
}
