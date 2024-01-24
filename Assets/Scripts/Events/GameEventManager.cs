using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static event BalanceObjectWeight balanceObjectWeight;
    public delegate void BalanceObjectWeight(float weight);

    public static void BalanceObjectWeightTrigger(float weight)
    {
        balanceObjectWeight?.Invoke(weight);
    }
}
