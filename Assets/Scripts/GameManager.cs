using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int test = 0;
    public int world { get; private set; }
    public int stage { get; private set; }

    public GeneticAlgorithm geneticAlgorithm;
    private void Awake()
    {
      if (Instance != null) {
        DestroyImmediate(gameObject);
      } else {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
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
      Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SetLevel(int world, int stage) {
      this.world = world;
      this.stage = stage;
      Debug.Log($"Set world to {world}-{stage}");
    }
    
    public void LoadLevel(int generations, int population, float mutationRate, int moves, int selection, int moveIncrease, int genPerMoveIncrease)
    {
      StartCoroutine(AsyncLoadLevel(generations, population, mutationRate, moves, selection, moveIncrease, genPerMoveIncrease));
    }

    private IEnumerator AsyncLoadLevel(int generations, int population, float mutationRate, int moves, int selection, int moveIncrease, int genPerMoveIncrease) {
      AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync($"{world}-{stage}", LoadSceneMode.Single);
      while (!asyncLoadLevel.isDone) {
        Debug.Log($"Loading {world}-{stage}");
        yield return null;
      }
      geneticAlgorithm.SetProperties(generations, population, mutationRate, moves, selection, moveIncrease, genPerMoveIncrease);
      geneticAlgorithm.StartGeneticAlgorithm();
    }



}