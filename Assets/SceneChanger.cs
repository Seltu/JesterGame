using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(SceneChange(sceneName));
    }
    private IEnumerator SceneChange(string sceneName)
    {
        GameEventManager.EndSceneTrigger();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
