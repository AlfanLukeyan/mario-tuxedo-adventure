using UnityEngine;

// Class for camera side scrolling
public class SideScrolling : MonoBehaviour
{
  public new Camera camera;
  private Vector3 cameraPosition;
  public float cameraSpeed;
  private float inputAxis;

  public float leftLimit;
  public float rightLimit;
  public float offset;

  void Awake() {
    cameraPosition = transform.position;
    camera = UnityEngine.Camera.main;
    offset = (camera.orthographicSize * camera.aspect);
  }

  private void LateUpdate() {
    inputAxis = Input.GetAxis("Camera Horizontal");

    cameraPosition.x += inputAxis * cameraSpeed * Time.deltaTime;

    cameraPosition.x = Mathf.Clamp(cameraPosition.x, leftLimit + offset, rightLimit - offset);
    transform.position = cameraPosition;
  }
}
