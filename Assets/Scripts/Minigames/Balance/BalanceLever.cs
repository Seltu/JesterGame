using System;
using UnityEngine;

public class BalanceLever : MonoBehaviour
{
    [SerializeField] private BalancePlayer player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D leverCollider;
    [SerializeField] private MinigameManager minigameManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite flippedSprite;
    private ObjectSpawner _objectSpawner;

    private float TiltScale => player.GetBalancePlayerStats().GetTiltScale();

    private void Start()
    {
        _objectSpawner = FindObjectOfType<ObjectSpawner>();
        if(minigameManager!=null)
        minigameManager.OnGameWin += FlipOver;
        player.OnMovement += AddInclination;
    }

    private void FlipOver()
    {
        leverCollider.enabled = false;
        spriteRenderer.sprite = flippedSprite;
    }

    private void AddInclination(float movement)
    {
        rb.angularVelocity += movement * TiltScale;
    }

}
