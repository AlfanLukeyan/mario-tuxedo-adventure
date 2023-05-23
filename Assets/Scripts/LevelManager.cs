using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GeneticAlgorithm properties;
    
    // Save and reset enemies
    public Transform enemyContainer;
    public List<GameObject> enemies;
    public GameObject goombaPrefab;

    // HUD
    public TextMeshProUGUI currentGenerationHUD;
    public TextMeshProUGUI moveCountHUD;
    public TextMeshProUGUI populationHUD;
    public TextMeshProUGUI fitnessHUD;

    // Initialize level manager
    private void Awake() {
      enemyContainer = GameObject.Find("Enemies").transform;
      goombaPrefab = Resources.Load<GameObject>("Prefabs/Goomba");

      foreach (Transform enemy in enemyContainer) {
        enemies.Add(enemy.gameObject);
      }

      properties = GameManager.Instance.geneticAlgorithm;
      UpdateHUD();
    }

    // Resetting enemy positions
    public void Reset() {
      List<GameObject> newEnemies = new List<GameObject>();

      foreach (GameObject enemy in enemies) {
        Vector2 position = enemy.GetComponent<Enemy>().position;
        newEnemies.Add(Instantiate(goombaPrefab, position, new Quaternion(0, 0, 0, 0), enemyContainer.transform));
        Destroy(enemy);
      }
      
      enemies = newEnemies;
    }

  // Updating the HUD
  public void UpdateHUD() {
    currentGenerationHUD.text = $"GENS: {properties.currentGeneration}";
    moveCountHUD.text = $"MOVES: {properties.moveCount}";
    populationHUD.text = $"POPULATION: {properties.populationSize}";
    fitnessHUD.text = $"BEST FITNESS: {String.Format("{0:0.000}", properties.bestFitness)}";
  }

}
