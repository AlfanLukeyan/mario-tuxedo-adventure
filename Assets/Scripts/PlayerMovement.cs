using UnityEngine;
using UnityEditor.Scripting.Python;
using Constants;
using System.Linq;

public class PlayerMovement : MonoBehaviour {
  private new Rigidbody2D rigidbody;
  private new Collider2D collider;

  private Vector2 velocity;
  private float inputAxis;

  public float moveSpeed = 8f;
  public float maxJumpHeight = 5f;
  public float maxJumpTime = 1f;
  public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
  public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

  public bool isGrounded { get; private set; }
  public bool isJumping { get; private set; }
  public bool isRunning => Mathf.Abs(velocity.x) > 0.25f;
  public bool isSliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
  public bool isFalling => velocity.y < 0f || !pressedJump;
  public bool finishedMoving { get; private set; }
  public int moveCounter { get; private set; }
  public bool pressedJump { get; private set; }
  
  public Player player;
  public GeneticAlgorithm properties;
  private void Awake() {
    rigidbody = GetComponent<Rigidbody2D>();
    collider = GetComponent<Collider2D>();
    properties = GameObject.Find("Genetic Algorithm").GetComponent<GeneticAlgorithm>();
  }

  private void OnEnable() {
    rigidbody.isKinematic = false;
    collider.enabled = true;
    velocity = Vector2.zero;
    isJumping = false;
    finishedMoving = false;
    pressedJump = false;
  }

  private void OnDisable() {
    rigidbody.isKinematic = true;
    collider.enabled = false;
    velocity = Vector2.zero;
    isJumping = false;
    if (!finishedMoving) {
      finishedMoving = true;
      properties.finishedCount++;
    }
  }

  private void Update() {
    isGrounded = rigidbody.Raycast(Vector2.down, 0.375f);
    
    HorizontalMovement();

    ApplyGravity();

    if (isGrounded) {
      GroundedMovement();
    }
  }

  private void HorizontalMovement() {
    velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

    if (rigidbody.Raycast(Vector2.right * velocity.x, 0.15f)) {
      player.collisionCount++;
      velocity.x = 0f;
    }

    if (velocity.x > 0f) {
      transform.eulerAngles = Vector3.zero;
    } else if (velocity.x < 0f) {
      transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
  }

  private void GroundedMovement() {
    velocity.y = Mathf.Max(velocity.y, 0f);
    isJumping = velocity.y > 0f;

    if (pressedJump) {
      velocity.y = jumpForce;
      isJumping = true;
    }
  }

  private void ApplyGravity() {
    float multiplier = isFalling ? 2f : 1f;

    velocity.y += gravity * multiplier * Time.fixedDeltaTime;

    // Terminal velocity in y axis
    velocity.y = Mathf.Max(velocity.y, gravity / 2f);

  }

  private void FixedUpdate() {
    Vector2 position = rigidbody.position;
    position += velocity * Time.fixedDeltaTime;

    rigidbody.MovePosition(position);
    if (moveCounter < properties.moveCount) {
      moveCounter++;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (transform.DotTest(collision.transform, Vector2.up)) {
      player.collisionCount++;
      velocity.y = 0f;
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.CompareTag("Enemy")) {
      // bounce off enemy head
      if (transform.DotTest(other.transform, Vector2.down))
      {
        Physics2D.IgnoreCollision(
          other, 
          GetComponentsInChildren<Collider2D>().First(c => c.gameObject != gameObject));
        velocity.y = jumpForce / 2f;
        isJumping = true;
      }
    }
    
  }

  public void ProcessMove(Move move) {
    if (move == Move.RIGHT) inputAxis = 1;
    if (move == Move.LEFT) inputAxis = -1;
    if (move == Move.JUMP) pressedJump = true;
    else pressedJump = false;
  }

  public void StopMove() {
    inputAxis = 0;
    pressedJump = false;
    if (isGrounded && Mathf.Abs(velocity.x) < 0.1 && !finishedMoving) {
      finishedMoving = true;
      properties.finishedCount++;
    }
  }
}
