using UnityEngine;
using System.Collections.Generic;
using Constants;

public class PlayerGeneticAlgorithm : MonoBehaviour
{
  public List<Move> moves;
  public float fitness;

  public int collisionCount;
  public GeneticAlgorithm geneticAlgorithm;

  private void Awake() {
    geneticAlgorithm = GameObject.Find("Genetic Algorithm").GetComponent<GeneticAlgorithm>();

    moves = new List<Move>(geneticAlgorithm.moveSaved);
    for (int i = geneticAlgorithm.moveSavedCount; i < geneticAlgorithm.moveCount; i++) {
      moves.Add((Move) Random.Range(0, 4));
    }

    Reset();
  }

  public void Reset() {
    fitness = 0;
    collisionCount = 0;
  }
}
