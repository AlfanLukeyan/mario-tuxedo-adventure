import UnityEngine as ue
import random

playerPrefab = ue.Resources.Load("Prefabs/Mario")
player_container = ue.GameObject.Find("Players")

properties = ue.GameObject.Find("Genetic Algorithm").GetComponent("GeneticAlgorithm")

flag_position = ue.GameObject.Find("FlagPole")

def newPlayer():
  player = ue.GameObject.Instantiate(playerPrefab, ue.Vector3(2, 2, 0), ue.Quaternion(0, 0, 0, 0), player_container.transform)
  return (player, player.GetComponent("PlayerGeneticAlgorithm"))

def fitness(players):
  for player in players:
    player_properties = player.GetComponent("PlayerGeneticAlgorithm")
    distance = ue.Vector3.Distance(flag_position.transform.position, player.transform.position)
    left_penalty = player_properties.moves.count(properties.MOVESLIST[1])
    default_penalty = player_properties.moves.count(properties.MOVESLIST[0])
    collision_penalty = player_properties.collisionCount * 5
    death_penalty = 9999 if player_properties.isDead else 0
    player_properties.fitness = distance + left_penalty + default_penalty + collision_penalty + death_penalty
  return players

def selection(players):
  temp = [(player, player.GetComponent("PlayerGeneticAlgorithm")) for player in players]

  temp = sorted(temp, key=lambda player: player[1].fitness)
  
  players = [player for player, _ in temp]

  for index, player in enumerate(players):
    if (index >= properties.selectionCount):
      ue.Object.Destroy(player)

  return players[:properties.selectionCount]

def crossover(players):
  offspring = []
  temp = [player.GetComponent("PlayerGeneticAlgorithm") for player in players]
  for _ in range((properties.populationSize - properties.selectionCount) // 2):
    parent1 = random.choice(temp)
    parent2 = random.choice(temp)

    child1 = newPlayer()
    child2 = newPlayer()

    split = random.randint(0, properties.moveCount)
    
    for i in range(0, split):
      child1[1].moves[i] = parent1.moves[i]
      child2[1].moves[i] = parent2.moves[i]
    
    for i in range(split, properties.moveCount):
      child1[1].moves[i] = parent2.moves[i]
      child2[1].moves[i] = parent1.moves[i]

    offspring.append(child1[0])
    offspring.append(child2[0])
  
  players.extend(offspring)

  return players

def mutation(players):
  for player in players:
    temp = player.GetComponent("PlayerGeneticAlgorithm")
    for idx in range(properties.moveCount):
      if random.uniform(0.0, 1.0) <= properties.mutationRate:
        temp.moves[idx] = properties.MOVESLIST[random.randint(0, 3)]
  return players

def reset(players):
  for player in players:
    player.SetActive(True)
    player_movement = player.GetComponent("PlayerMovement")
    player.transform.SetLocalPositionAndRotation(ue.Vector3(2, 2, 0),ue.Quaternion(0, 0, 0, 0))
    player_movement.Reset()
  return players;

def increase_moves():
  properties.moveSavedCount = properties.moveCount
  properties.moveCount += properties.moveIncreaseAmount
  players = fitness(properties.players)
  best_player = selection(players)[0].GetComponent("PlayerGeneticAlgorithm")
  properties.moveSaved = best_player.moves
  for player in players:
    ue.Object.Destroy(player)
  properties.players = [newPlayer()[0] for _ in range(properties.populationSize)]
  

def genetic_algorithm():
  players = fitness(properties.players)
  players = selection(players)
  players = crossover(players)
  players = mutation(players)
  players = reset(players)
  properties.players = players

if __name__ == "__main__":
  if properties.currentGeneration % properties.generationPerMoveIncrease == 0:
    increase_moves()
  else:
    genetic_algorithm()
  
  properties.currentGeneration += 1
  properties.finishedCount = 0;
  properties.isRunning = True;