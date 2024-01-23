using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester_Lava_Stats : MonoBehaviour
{
    [SerializeField] private float _jesterSpeed;
    [SerializeField] private float _jumpForce;

    public float GetLavaJesterSpeed()
    {
        return _jesterSpeed;
    }

    public float GetLavaJesterJump()
    {
        return _jumpForce;
    }

}
