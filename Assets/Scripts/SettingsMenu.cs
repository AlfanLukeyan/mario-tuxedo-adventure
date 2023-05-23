using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
  public TMP_InputField generationsField;
  public TMP_InputField populationField;
  public TMP_InputField mutationRateField;
  public TMP_InputField initialMovesField;
  public TMP_InputField selectionField;
  public TMP_InputField moveIncreaseField;
  public TMP_InputField genPerMoveIncreaseField;

  private int generations;
  private int population;
  private float mutationRate;
  private int initialMoves;
  private int selection;
  private int moveIncrease;
  private int genPerMoveIncrease;

  public Button start;

  public void StartHandleClick() {
    if (!CheckValid()) {
      Debug.Log("Please enter valid values!");
      return;
    }

    GameManager.Instance.LoadLevel(generations, population, mutationRate, initialMoves, selection, moveIncrease, genPerMoveIncrease);
  }

  private bool CheckValid() {
    if (string.IsNullOrWhiteSpace(generationsField.text) || !int.TryParse(generationsField.text, out generations)) {
      return false;
    }
    if (string.IsNullOrWhiteSpace(populationField.text) || !int.TryParse(populationField.text, out population)) {
      return false;
    }
    if (string.IsNullOrWhiteSpace(mutationRateField.text) || !float.TryParse(mutationRateField.text, out mutationRate)) {
      return false;
    }
    if (string.IsNullOrWhiteSpace(initialMovesField.text) || !int.TryParse(initialMovesField.text, out initialMoves)) {
      return false;
    }
    if (string.IsNullOrWhiteSpace(selectionField.text) || !int.TryParse(selectionField.text, out selection)) {
      return false;
    }
    if (string.IsNullOrWhiteSpace(moveIncreaseField.text) || !int.TryParse(moveIncreaseField.text, out moveIncrease)) {
      return false;
    }
    if (string.IsNullOrWhiteSpace(genPerMoveIncreaseField.text) || !int.TryParse(genPerMoveIncreaseField.text, out genPerMoveIncrease)) {
      return false;
    }
    return true;
  }

}
