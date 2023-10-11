using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public bool fadeIn, ShowGameOverUI;
    [HideInInspector] public int point;
    private Spawner spawner;
    private Blade blade;
    private void Awake()
    {
        Instance = this;
        spawner = FindObjectOfType<Spawner>();
        blade = FindObjectOfType<Blade>();
    }
    public void Fading()
    {
        fadeIn = true;
    }
    public void ClearScene()
    {
        point = 0;
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
        blade.enabled = true;
        spawner.enabled = true;
    }
}
