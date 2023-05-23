using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public Transform enemyContainer;
    public List<GameObject> enemies;
    public GameObject goombaPrefab;
    public GeneticAlgorithm properties;
    public TextMeshProUGUI currentGenerationHUD;
    public TextMeshProUGUI moveCountHUD;
    public TextMeshProUGUI populationHUD;
    public TextMeshProUGUI fitnessHUD;

    private void Awake() {
      enemyContainer = GameObject.Find("Enemies").transform;
      goombaPrefab = Resources.Load<GameObject>("Prefabs/Goomba");

      foreach (Transform enemy in enemyContainer) {
        enemies.Add(enemy.gameObject);
      }

      properties = GameManager.Instance.geneticAlgorithm;
    }

    public void Reset() {
      List<GameObject> newEnemies = new List<GameObject>();

      foreach (GameObject enemy in enemies) {
        Vector2 position = enemy.GetComponent<Goomba>().position;
        newEnemies.Add(Instantiate(goombaPrefab, position, new Quaternion(0, 0, 0, 0), enemyContainer.transform));
        Destroy(enemy);
      }
      
      enemies = newEnemies;
    }

  public void UpdateHUD() {
    currentGenerationHUD.text = $"GENS: {properties.currentGeneration}";
    moveCountHUD.text = $"MOVES: {properties.moveCount}";
    populationHUD.text = $"POPULATION: {properties.populationSize}";
    fitnessHUD.text = $"BEST FITNESS: {String.Format("{0:0.000}", properties.bestFitness)}";
  }

}
