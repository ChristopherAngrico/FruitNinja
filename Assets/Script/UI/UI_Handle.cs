using UnityEngine.SceneManagement;
using UnityEngine;

public class UI_Handle : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
