import UnityEngine as ue

playerPrefab = ue.Resources.Load("Prefabs/Mario")

properties = ue.GameObject.Find("Genetic Algorithm").GetComponent("GeneticAlgorithm")

player_container = ue.GameObject.Find("Players")

def init_players(population):
  return [ue.GameObject.Instantiate(
    playerPrefab, ue.Vector3(2, 2, 0),
    ue.Quaternion(0, 0, 0, 0), player_container.transform)
  
    for _ in range(population)
  ]

properties.players = init_players(properties.populationSize)

properties.isRunning = True;