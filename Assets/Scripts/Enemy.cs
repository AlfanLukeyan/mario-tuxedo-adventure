using UnityEngine;

public class Enemy : MonoBehaviour
{
  // Save position of Goomba
  public Vector2 position;

  private void Awake() {
    position = transform.position;
  }
}
