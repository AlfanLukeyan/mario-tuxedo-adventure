using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;
using Constants;
using System.Collections.Generic;

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


  public int finishedCount = 0;
  public bool isRunning = false;
  public int moveSavedCount;
  public List<Move> moveSaved;
  public GameObject[] players;
  public Move[] MOVESLIST = {Move.DEFAULT, Move.LEFT, Move.RIGHT, Move.JUMP};

  [MenuItem("Python/Genetic Algorithm")]
  public static void RunGeneticAlgorithm() {
    PythonRunner.RunFile($"{Application.dataPath}/Scripts/genetic_algorithm.py", "__main__");
  }

  private void Update() {
    if (isRunning) {
      if (finishedCount == populationSize) {
        isRunning = false;
        PythonRunner.RunFile($"{Application.dataPath}/Scripts/evaluate.py", "__main__");
      }
    }
  }
}
