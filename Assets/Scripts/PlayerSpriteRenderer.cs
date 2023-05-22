using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  private PlayerMovement movement;

  public Sprite idle;
  public Sprite jump;
  public Sprite slide;
  public AnimatedSprite run;

  private void Awake() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    movement = GetComponentInParent<PlayerMovement>();
  }
  private void LateUpdate() {
    run.enabled = movement.isRunning;
    
    if (movement.isJumping) {
      spriteRenderer.sprite = jump;
    } else if (movement.isSliding) {
      spriteRenderer.sprite = slide;
    } else if (!movement.isRunning) {
      spriteRenderer.sprite = idle;
    }
  }

  private void OnEnable()
  {
    spriteRenderer.enabled = true;
  }
  private void OnDisable()
  {
    spriteRenderer.enabled = false;
    run.enabled = false;
  }
}
