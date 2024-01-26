using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static event BalanceObjectSpawn balanceObjectSpawn;
    public delegate void BalanceObjectSpawn();
    public static event AddScore addScore;
    public delegate void AddScore(float score);
    public static event EndScene endScene;
    public delegate void EndScene();


    public static void BalanceObjectSpawnTrigger()
    {
        balanceObjectSpawn?.Invoke();
    }

    public static void AddScoreTrigger(float score)
    {
        addScore?.Invoke(score);
    }
    public static void EndSceneTrigger()
    {
        endScene?.Invoke();
    }
}
