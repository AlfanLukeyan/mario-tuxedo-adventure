using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;
using Constants;
using System.Collections.Generic;
using System;

public class GeneticAlgorithm : MonoBehaviour
{
  private LevelManager levelManager;

  // Properties
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
  public bool isDone = false;

  public int moveSavedCount;
  public List<Move> moveSaved;

  public Player[] players;
  public Move[] MOVESLIST;

  public int moveCounter = 0;

  [MenuItem("Python/Run Genetic Algorithm")]
  public static void GeneticAlgorithmMenu() {
    PythonRunner.RunFile($"{Application.dataPath}/Scripts/genetic_algorithm.py", "__main__");
  }

  private void Awake() {
    MOVESLIST = (Move[]) Enum.GetValues(typeof(Move));
  }

  // For starting the genetic algorithm
  public void StartGeneticAlgorithm() {
    PythonRunner.RunFile($"{Application.dataPath}/Scripts/genetic_algorithm.py", "__main__");
  }

  // Sets the properties of the genetic algorithm
  public void Initialize(int generations, int population, float mutationRate, int moves, int selection, int moveIncrease, int genPerMoveIncrease) {
    this.generationMax = generations;
    this.populationSize = population;
    this.mutationRate = mutationRate;
    this.moveCount = moves;
    this.selectionCount = selection;
    this.moveIncreaseAmount = moveIncrease;
    this.generationPerMoveIncrease = genPerMoveIncrease;
    
    levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    levelManager.UpdateHUD();
  }

  // Handles player move processing and evaluation
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
      if (finishedCount == populationSize || isDone == true) {
        moveCounter = 0;
        isRunning = false;
        PythonRunner.RunFile($"{Application.dataPath}/Scripts/evaluate.py", "__main__");
        levelManager.Reset();
        levelManager.UpdateHUD();
      }

      if (isDone == true) {
        GameManager.Instance.LoadMenu();
      }
    }
  }

}
