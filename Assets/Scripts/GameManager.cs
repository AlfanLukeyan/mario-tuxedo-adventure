using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public Transform enemies;
    
    public GameObject goombaPrefab;
    

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        enemies = GameObject.Find("Enemies").transform;
        goombaPrefab = Resources.Load<GameObject>("Prefabs/Goomba");
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void Reset() {
        GameObject enemyContainer = new GameObject("Enemies");
        foreach (Transform enemy in enemies) {
          Vector2 position = enemy.gameObject.GetComponent<Goomba>().position;

          Instantiate(goombaPrefab, position, new Quaternion(0, 0, 0, 0), enemyContainer.transform);
        }
        Destroy(enemies.gameObject);
        enemies = enemyContainer.transform;
    }


}