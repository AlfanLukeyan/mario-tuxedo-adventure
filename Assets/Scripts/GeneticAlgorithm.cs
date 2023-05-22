using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.Scripting.Python;
using Constants;
using System.Collections.Generic;
using System;
using TMPro;

public class GeneticAlgorithm : MonoBehaviour
{
  public int generationMax;
  public int populationSize;
  public float mutationRate;
  public int moveCount;
  public int selectionCount;
  public int moveIncreaseAmount;
  public int generationPerMoveIncrease;
  public int currentGeneration = 1;
  public float bestFitness = 0f;

  public int finishedCount = 0;
  public bool isRunning = false;
  public int moveSavedCount;
  public List<Move> moveSaved;
  public Player[] players;
  public Move[] MOVESLIST;

  public TextMeshProUGUI currentGenerationHUD;
  public TextMeshProUGUI moveCountHUD;
  public TextMeshProUGUI populationHUD;
  public TextMeshProUGUI fitnessHUD;

  public LevelManager levelManager;

  public int moveCounter = 0;

  [MenuItem("Python/Genetic Algorithm")]
  public static void RunGeneticAlgorithm() {
    PythonRunner.RunFile($"{Application.dataPath}/Scripts/genetic_algorithm.py", "__main__");
  }

  private void Awake() {
    levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

    MOVESLIST = (Move[]) Enum.GetValues(typeof(Move));
    UpdateHUD();
  }

  private void Update() {
    
    if (isRunning) {
      if (moveCounter < moveCount) {
        foreach (Player player in players) {
          player.playerMovement.ProcessMove(player.moves[moveCounter]);
        }
        moveCounter++;
      } else {
        foreach (Player player in players) {
          player.playerMovement.StopMove();
        }
        
      }
      if (finishedCount == populationSize) {
        moveCounter = 0;
        isRunning = false;
        PythonRunner.RunFile($"{Application.dataPath}/Scripts/evaluate.py", "__main__");
        levelManager.Reset();
        UpdateHUD();
      }
    }
  }

  private void UpdateHUD() {
    currentGenerationHUD.text = $"GENS: {currentGeneration}";
    moveCountHUD.text = $"MOVES: {moveCount}";
    populationHUD.text = $"POPULATION: {populationSize}";
    fitnessHUD.text = $"BEST FITNESS: {String.Format("{0:0.000}", bestFitness)}";
  }
}
