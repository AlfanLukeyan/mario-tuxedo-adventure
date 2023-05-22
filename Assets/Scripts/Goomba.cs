using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
  public Vector2 position;

  private void Awake() {
    position = transform.position;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Player player = other.gameObject.GetComponent<Player>();

      if (!other.transform.DotTest(transform, Vector2.down)) {    
          player.Hit();
      }
    }
  }
}
