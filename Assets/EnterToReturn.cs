using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterToReturn : MonoBehaviour
{
    [SerializeField] private string targetScene;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Continue());
        }
    }

    private IEnumerator Continue()
    {
        GameEventManager.EndSceneTrigger();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(targetScene);
    }
}
