using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] _hearts;
    [SerializeField] private Sprite _damageHeart;

    private int _damageCount = 0;

    private void Start()
    {
        _damageCount = 0;
        GameEventManager.onTakeDamage += AddDamageCounter;
        Debug.Log(_damageCount);
        GameOverManager._atualScene = SceneManager.GetActiveScene().name;
    }

    private void OnDestroy()
    {
         GameEventManager.onTakeDamage -= AddDamageCounter;
    }

    private void AddDamageCounter()
    {
        if(_damageCount < _hearts.Length)
        {
            _hearts[_damageCount].sprite = _damageHeart;
            _damageCount++;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }   
}
