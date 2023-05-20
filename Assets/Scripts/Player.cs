using UnityEngine;

public class Player : MonoBehaviour
{
  public PlayerGeneticAlgorithm playerProperties;
  public PlayerMovement playerMovement;

  public GameObject player;

  private void Awake() {
    player = gameObject;
  }

  public void Reset() {
    player.SetActive(true);
    playerProperties.Reset();
    playerMovement.Reset();
    player.transform.SetLocalPositionAndRotation(new Vector3(2, 2, 0), new Quaternion(0, 0, 0, 0));
  }
}
