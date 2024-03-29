import UnityEngine as ue

playerPrefab = ue.Resources.Load("Prefabs/Mario")
properties = ue.GameObject.Find("Game Manager").GetComponent("GeneticAlgorithm")
player_container = ue.GameObject.Find("Players")


def init_players(population):
  return [ue.GameObject.Instantiate(
    playerPrefab, ue.Vector3(2, 2, 0),
    ue.Quaternion(0, 0, 0, 0), player_container.transform).GetComponent("Player")
  
    for _ in range(population)
  ]

# Initializing population and starting the Genetic Algorithm
if __name__ == "__main__":
  properties.players = init_players(properties.populationSize)
  properties.isRunning = True;