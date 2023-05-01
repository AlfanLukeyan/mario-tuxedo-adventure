using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;

public class GeneticAlgorithm : MonoBehaviour
{
  [MenuItem("Python/Genetic Algorithm")]
  static void RunGeneticAlgorithm() {
    PythonRunner.RunFile($"{Application.dataPath}/Scripts/genetic_algorithm.py");
  }
}
