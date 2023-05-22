using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;
using Constants;
using System.Collections.Generic;
using System;

public class GeneticAlgorithm : MonoBehaviour
{
  public static GeneticAlgorithm Instance;
  public int generationMax;
  public int populationSize;
  public float mutationRate;
  public int moveCount;
  public int selectionCount;
  public int moveIncreaseAmount;
  public int generationPerMoveIncrease;
  public int currentGeneration = 1;

  public int finishedCount = 0;
  public bool isRunning = false;
  public int moveSavedCount;
  public List<Move> moveSaved;
  public Player[] players;
  public Move[] MOVESLIST;

  public int moveCounter = 0;

  [MenuItem("Python/Genetic Algorithm")]
  public static void RunGeneticAlgorithm() {
    PythonRunner.RunFile($"{Application.dataPath}/Scripts/genetic_algorithm.py", "__main__");
  }

  private void Awake() {
    if (Instance != null) {
      DestroyImmediate(gameObject);
    } else {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }

    MOVESLIST = (Move[]) Enum.GetValues(typeof(Move));
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
        GameManager.Instance.Reset();
      }
    }
  }
}
