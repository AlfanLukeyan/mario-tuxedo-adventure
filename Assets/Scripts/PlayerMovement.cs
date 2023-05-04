using UnityEngine;
using UnityEditor.Scripting.Python;
using Constants;

public class PlayerMovement : MonoBehaviour {
  private new Camera camera;
  private new Rigidbody2D rigidbody;

  private Vector2 velocity;
  private float inputAxis;

  public float moveSpeed = 8f;
  public float maxJumpHeight = 4f;
  public float maxJumpTime = 1f;
  public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
  public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

  public bool isGrounded { get; private set; }
  public bool isJumping { get; private set; }
  public bool isRunning => Mathf.Abs(velocity.x) > 0.25f;
  public bool isSliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
  public bool finishedMoving = false;
  public int moveCounter = 0;
  public bool pressedJump = false;
  
  public PlayerGeneticAlgorithm playerGeneticAlgorithm;
  public GeneticAlgorithm geneticAlgorithm;

  private void Awake() {
    rigidbody = GetComponent<Rigidbody2D>();
    geneticAlgorithm = GameObject.Find("Genetic Algorithm").GetComponent<GeneticAlgorithm>();
    camera = Camera.main;
  }

  private void Update() {
    if (geneticAlgorithm.isRunning) {
      if (moveCounter < geneticAlgorithm.moveCount) {
        Move move = playerGeneticAlgorithm.moves[moveCounter];
        if (move == Move.RIGHT) inputAxis = 1;
        if (move == Move.DEFAULT) inputAxis = 0;
        if (move == Move.LEFT) inputAxis = -1;
        if (move == Move.JUMP) pressedJump = true;
        else pressedJump = false;
      } else {
        inputAxis = 0;
        if (isGrounded && Mathf.Abs(velocity.x) < 0.1 && !finishedMoving) {
          finishedMoving = true;
          geneticAlgorithm.finishedCount++;
        }
      }
    }

    isGrounded = rigidbody.Raycast(Vector2.down);
    
    HorizontalMovement();

    if (isGrounded) {
      GroundedMovement();
    }

    ApplyGravity();
  }

  private void HorizontalMovement() {
    velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

    if (rigidbody.Raycast(Vector2.right * velocity.x)) {
      playerGeneticAlgorithm.collisionCount++;
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
    bool isFalling = velocity.y < 0f || !pressedJump;
    float multiplier = isFalling ? 2f : 1f;

    velocity.y += gravity * multiplier * Time.deltaTime;

    // Terminal velocity in y axis
    velocity.y = Mathf.Max(velocity.y, gravity / 2f);

  }

  private void FixedUpdate() {
    Vector2 position = rigidbody.position;
    position += velocity * Time.fixedDeltaTime;

    // Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
    // Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    // position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

    rigidbody.MovePosition(position);
    if (moveCounter < geneticAlgorithm.moveCount) {
      moveCounter++;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")) {
      if (transform.DotTest(collision.transform, Vector2.up)) {
        playerGeneticAlgorithm.collisionCount++;
        velocity.y = 0f;
      }
    }
  }

  public void Reset() {
    moveCounter = 0;
    finishedMoving = false;
  }
}
