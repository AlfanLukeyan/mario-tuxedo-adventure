using UnityEngine;
using TMPro;

public class FinishedMenu : MonoBehaviour
{
  public TextMeshProUGUI generationsField;
  public TextMeshProUGUI populationField;
  public TextMeshProUGUI movesField;
  public TextMeshProUGUI bestFitnessField;

  private void Awake() {
    generationsField.text = GameManager.Instance.geneticAlgorithm.currentGeneration.ToString();
    populationField.text = GameManager.Instance.geneticAlgorithm.populationSize.ToString();
    movesField.text = GameManager.Instance.geneticAlgorithm.moveSavedCount.ToString();
    bestFitnessField.text = GameManager.Instance.geneticAlgorithm.bestFitness.ToString();
  }

  public void RunBestFitness() {
    GameManager.Instance.LoadLevelFinished();
  }

  public void copyMovesToClipboard() {
    GUIUtility.systemCopyBuffer = string.Join(", ", GameManager.Instance.geneticAlgorithm.moveSaved);
  }
}
