using UnityEngine;

public class SideScrolling : MonoBehaviour
{
  private Transform player;

  private Vector3 cameraPosition;
  public float cameraSpeed = 5;
  private float inputAxis;

  void Awake() {
    cameraPosition = this.transform.position;
  }

  private void Update() {
    inputAxis = Input.GetAxis("Camera Horizontal");

    cameraPosition.x += inputAxis * cameraSpeed * Time.deltaTime;

    transform.position = cameraPosition;
  }

  // private void Awake() {
  //   player = GameObject.FindWithTag("Player").transform;
  // }

  // private void LateUpdate() {
  //   Vector3 cameraPosition = transform.position;
  //   cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
    
  //   transform.position = cameraPosition;
  // }
}
