using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for movement for entities like goomba 
public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private GeneticAlgorithm properties;

    private void Awake()
    {
        properties = GameManager.Instance.geneticAlgorithm;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Movement handling
    private void FixedUpdate()
    {
        if (!properties.isRunning) return;
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        // Change direction when near a wall
        if (rigidbody.EnemyRaycast(direction, 0.375f)) {
            direction = -direction;
        }

        // Set y velocity to 0 when on ground
        if (rigidbody.EnemyRaycast(Vector2.down, 0.375f)) {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }

        // Set direction the Goomba is facing
        if (direction.x > 0f) {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        } else if (direction.x < 0f) {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
