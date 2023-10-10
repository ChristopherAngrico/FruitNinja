using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject UI_Menu;
    public void ShowUIMenu()
    {
        UI_Menu.SetActive(true);
        Time.timeScale = 0f;
    }
}
