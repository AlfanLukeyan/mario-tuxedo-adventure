using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer spriteRenderer;
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }
    public bool isDead => deathAnimation.enabled || !player.activeSelf;
    public List<Move> moves;
    public int collisionCount;
    public float fitness;
    public PlayerMovement playerMovement;
    public GameObject player;
    public GeneticAlgorithm properties;

    private void Awake()
    {
      player = gameObject;
      capsuleCollider = GetComponent<CapsuleCollider2D>();
      deathAnimation = GetComponent<DeathAnimation>();
      properties = GameObject.Find("Genetic Algorithm").GetComponent<GeneticAlgorithm>();

      moves = new List<Move>(properties.moveSaved);
      for (int i = properties.moveSavedCount; i < properties.moveCount; i++) {
        moves.Add((Move) Random.Range(1, 4));
      }
    }
    
    public void Hit()
    {
      if (!isDead)
      {
        Death();
      }
    }

    public void Death()
    {
      spriteRenderer.enabled = false;
      deathAnimation.enabled = true;
    }
}

