using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public Transform enemyContainer;
    public List<GameObject> enemies;
    public GameObject goombaPrefab;
    
    private void Awake() {
      enemyContainer = GameObject.Find("Enemies").transform;
      goombaPrefab = Resources.Load<GameObject>("Prefabs/Goomba");

      foreach (Transform enemy in enemyContainer) {
        enemies.Add(enemy.gameObject);
      }
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

}
