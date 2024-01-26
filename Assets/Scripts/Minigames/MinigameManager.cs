using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public event Action OnGameWin;
    [SerializeField] private ImageBar laughMeter;
    [SerializeField] private int maxProgress;
    [SerializeField] private int startingProgress;
    [SerializeField] private string nextScene;
    [SerializeField] private Animator canvasAnimator;
    private bool _ended;
    private float _progress;

    private void OnEnable()
    {
        GameEventManager.addScore += AddProgress;
    }

    private void OnDisable()
    {
        GameEventManager.addScore -= AddProgress;
    }

    private void Start()
    {
        _progress = startingProgress;
        laughMeter.SetMaxValue(maxProgress);
    }

    private void AddProgress(float points)
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
        canvasAnimator.SetTrigger("Lost");
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 1f;
    }

    private IEnumerator Win()
    {
        _ended = true;
        OnGameWin?.Invoke();
        yield return new WaitForSeconds(2f);
        GameEventManager.EndSceneTrigger();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene);
    }
}
