using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMenu : MonoBehaviour
{
  public Button[] button;

  private void Start() {
    button[0].onClick.AddListener(delegate { GameManager.Instance.SetLevel(1, 1); });
    button[1].onClick.AddListener(delegate { GameManager.Instance.SetLevel(1, 2); });
  }
}
