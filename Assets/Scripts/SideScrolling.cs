using UnityEngine;

// Class for camera side scrolling
public class SideScrolling : MonoBehaviour
{
  public new Camera camera;
  private Transform player;
  public float cameraSpeed;
  private float inputAxis;

  private bool isFollowingPlayer;

  public float leftLimit;
  public float rightLimit;
  public float offset;

  void Awake() {
    camera = UnityEngine.Camera.main;
    offset = (camera.orthographicSize * camera.aspect);
  }

  private void LateUpdate() {
    Vector3 cameraPosition = transform.position;
    if (isFollowingPlayer) {
      cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
    } else {
      inputAxis = Input.GetAxis("Camera Horizontal");
      cameraPosition.x += inputAxis * cameraSpeed * Time.deltaTime;
      cameraPosition.x = Mathf.Clamp(cameraPosition.x, leftLimit + offset, rightLimit - offset);
    }
    transform.position = cameraPosition;
  }

  public void followPlayer() {
    isFollowingPlayer = true;
    player = GameObject.FindWithTag("Player").transform;
  }
}
