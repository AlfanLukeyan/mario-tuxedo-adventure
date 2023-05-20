using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public CapsuleCollider2D capsuleCollider { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public bool dead => deathAnimation.enabled;

    public PlayerGeneticAlgorithm playerProperties;
    public PlayerMovement playerMovement;

    public GameObject player;

    public void Reset() {
        player.SetActive(true);
        playerProperties.Reset();
        playerMovement.Reset();
        player.transform.SetLocalPositionAndRotation(new Vector3(2, 2, 0), new Quaternion(0, 0, 0, 0));
    }
    
    public void Hit()
    {
        if (!dead)
        {
            Death();
        }
    }
    private void Awake()
    {
        player = gameObject;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
    }

    public void Death()
    {
        smallRenderer.enabled = false;
        deathAnimation.enabled = true;
    }


}

