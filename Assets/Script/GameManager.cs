using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public bool fadeIn;
    [HideInInspector] public int point;
    private Spawner spawner;
    private Blade blade;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        spawner = FindObjectOfType<Spawner>();
        blade = FindObjectOfType<Blade>();
    }
    public void Fading()
    {
        fadeIn = true;
    }
    public void ClearScene()
    {
        spawner.enabled = false;
        blade.enabled = false;
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (GameObject allGameobject in gameObject)
        {
            Destroy(allGameobject);
        }
    }
    public void NewGame()
    {
        spawner.enabled = true;
        blade.enabled = true;
    }
    public void Reset()
    {
        ClearScene();
        point = 0;
    }
}
