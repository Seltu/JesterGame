using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] _hearts;

    private int _damageCount = 0;

    private void Start()
    {
        GameEventManager.onTakeDamage += AddDamageCounter;
    }

    private void AddDamageCounter()
    {
        if(_damageCount < _hearts.Length)
        {
            _hearts[_damageCount].color = Color.black;
            _damageCount++;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }   
}