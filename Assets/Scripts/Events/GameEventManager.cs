using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static event BalanceObjectSpawn balanceObjectSpawn;
    public delegate void BalanceObjectSpawn();

    public static void BalanceObjectWeightTrigger()
    {
        balanceObjectSpawn?.Invoke();
    }
}
