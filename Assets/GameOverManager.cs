using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private float gameOverTimer;
    [SerializeField] private TextMeshProUGUI continueText;
    private bool _cancelled;
    private void Update()
    {
        if (_cancelled) return;
        if (gameOverTimer > 0)
        {
            gameOverTimer -= Time.deltaTime;
            continueText.text = "Continue?..." + ((int)gameOverTimer).ToString() + "\n<size=%70><pos=%285> (Press Enter)";
        }
        else
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Continue());
            _cancelled = true;
        }
    }

    private IEnumerator Continue()
    {
        GameEventManager.EndSceneTrigger();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("IntroScene");
    }
}
