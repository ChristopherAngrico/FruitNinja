using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        text.text = GameManager.Instance.point.ToString();
    }
}
