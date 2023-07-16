using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log(gameObject.name);
        SceneManager.LoadScene(sceneName);
        // Ensure the game is not paused
        Time.timeScale = 1;
    }
}
