using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedMenu : MonoBehaviour
{
  public void RunBestFitness() {
    GameManager.Instance.LoadLevelFinished();
  }
}
