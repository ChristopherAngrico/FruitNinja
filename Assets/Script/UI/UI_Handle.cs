using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class UI_Handle : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
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
    public void Restart()
    {
        anim.SetTrigger("OnTrigger");
        StartCoroutine(DisableGameOver());
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
    }
    private IEnumerator DisableGameOver()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        GameManager.Instance.NewGame();
    }
}
