using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameOver UIGameOver;
    private void Awake()
    {
        UIGameOver = FindObjectOfType<GameOver>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Fading();
            UIGameOver.SetEnableGameOver(); 
        }
    }
    
}
