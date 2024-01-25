using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public event Action OnGameWin;
    [SerializeField] private ImageBar laughMeter;
    [SerializeField] private int maxProgress;
    [SerializeField] private int startingProgress;
    [SerializeField] private string nextScene;
    private bool _ended;
    private float _progress;

    public void Start()
    {
        GameEventManager.addScore += AddProgress;
        _progress = startingProgress;
        laughMeter.SetMaxValue(maxProgress);
    }

    public void AddProgress(float points)
    {
        if (_ended) return;
        _progress += points;
        laughMeter.SetValue(_progress);
        if (_progress >= maxProgress)
        {
            StartCoroutine(Win());
        }
        else if (_progress <= 0)
        {
            StartCoroutine(Lose());
        }
    }

    private IEnumerator Lose()
    {
        _ended = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator Win()
    {
        _ended = true;
        OnGameWin?.Invoke();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextScene);
    }
}
