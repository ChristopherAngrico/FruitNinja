using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject UI_GameOver;
    public void SetEnableGameOver()
    {
        StartCoroutine(EnableGameOver());
    }
    private IEnumerator EnableGameOver()
    {
        yield return new WaitForSeconds(2);
        UI_GameOver.SetActive(true);
    }
}
