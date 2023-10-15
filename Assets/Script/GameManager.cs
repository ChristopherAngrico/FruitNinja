using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public bool fadeIn, ShowGameOverUI;
    [HideInInspector] public int point;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Blade blade;
    private void Awake()
    {
        Instance = this;
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
