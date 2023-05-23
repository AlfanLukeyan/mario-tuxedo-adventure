using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public class Player : MonoBehaviour
{
  // Components
  public PlayerSpriteRenderer spriteRenderer;
  public CapsuleCollider2D capsuleCollider { get; private set; }
  public DeathAnimation deathAnimation { get; private set; }

  // Objects
  public PlayerMovement playerMovement;
  public GameObject player;
  public GeneticAlgorithm properties;

  public List<Move> moves;

  // Fitness
  public float fitness;
  public int collisionCount;
  public bool isDead => deathAnimation.enabled || !player.activeSelf;

  // Initialize player
  private void Awake() {
    player = gameObject;
    capsuleCollider = GetComponent<CapsuleCollider2D>();
    deathAnimation = GetComponent<DeathAnimation>();
    properties = GameManager.Instance.geneticAlgorithm;

    // Generate moves for Mario
    moves = new List<Move>(properties.moveSaved);
    for (int i = properties.moveSavedCount; i < properties.moveCount; i++) {
      moves.Add((Move) Random.Range(1, 4));
    }
  }
  
  // Handle if Mario is hit by an enemy
  public void Hit() {
    if (!isDead)
    {
      spriteRenderer.enabled = false;
      deathAnimation.enabled = true;
    }
  }
}

