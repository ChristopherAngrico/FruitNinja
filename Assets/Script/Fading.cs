using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    private CanvasGroup _group;
    private float threshold = 0.1f;
    private bool fadeOut;
    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Update()
    {
        if (GameManager.Instance.fadeIn == true)
        {
            _group.alpha += Time.deltaTime;
            if ((1 - _group.alpha) < threshold)
            {
                _group.alpha = 1;
                GameManager.Instance.fadeIn = false;
                fadeOut = true;
                GameManager.Instance.Reset();
            }
        }
        if (fadeOut == true)
        {
            _group.alpha -= Time.deltaTime;
            if ((_group.alpha) < threshold)
            {
                _group.alpha = 0;
                fadeOut = false;
                GameManager.Instance.NewGame();
            }
        }
    }

}
