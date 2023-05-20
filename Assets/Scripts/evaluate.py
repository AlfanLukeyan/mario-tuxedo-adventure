import math
import UnityEngine as ue
import random

playerPrefab = ue.Resources.Load("Prefabs/Mario")
player_container = ue.GameObject.Find("Players")

properties = ue.GameObject.Find("Genetic Algorithm").GetComponent("GeneticAlgorithm")

flag_position = ue.GameObject.Find("FlagPole")

def newPlayer():
  return ue.GameObject.Instantiate(
    playerPrefab, 
    ue.Vector3(2, 2, 0), 
    ue.Quaternion(0, 0, 0, 0), 
    player_container.transform).GetComponent("Player")
  

def fitness(players):
  for player in players:
    player_properties = player.playerProperties
    distance = ue.Vector3.Distance(flag_position.transform.position, player.transform.position)
    left_penalty = player_properties.moves.count(properties.MOVESLIST[0])
    collision_penalty = player_properties.collisionCount * 5
    death_penalty = 9999 if player_properties.isDead else 0
    player_properties.fitness = distance + left_penalty + collision_penalty + death_penalty
  return players

def selection(players):
  temp = [(player, player.playerProperties) for player in players]

  temp = sorted(temp, key=lambda player: player[1].fitness)

  players = [player for player, _ in temp]

  for index, player in enumerate(players):
    if (index >= properties.selectionCount):
      ue.Object.Destroy(player.gameObject)

  return players[:properties.selectionCount]

def crossover(players):
  offspring = []

  for _ in range(int(math.ceil(properties.populationSize - properties.selectionCount)/2)):
    parent1 = random.choice(players)
    parent2 = random.choice(players)

    child1 = newPlayer()
    child2 = newPlayer()

    split = random.randint(properties.moveSavedCount + 1, properties.moveCount)
    
    for i in range(properties.moveSavedCount + 1, split):
      child1.playerProperties.moves[i] = parent1.playerProperties.moves[i]
      child2.playerProperties.moves[i] = parent2.playerProperties.moves[i]
    
    for i in range(split, properties.moveCount):
      child1.playerProperties.moves[i] = parent2.playerProperties.moves[i]
      child2.playerProperties.moves[i] = parent1.playerProperties.moves[i]

    offspring.append(child1)
    offspring.append(child2)
  
  players.extend(offspring)

  return players

def mutation(players):
  for player in players:
    for idx in range(properties.moveSavedCount + 1, properties.moveCount):
      if random.uniform(0.0, 1.0) <= properties.mutationRate:
        player.playerProperties.moves[idx] = properties.MOVESLIST[random.randint(0, 2)]
  return players

def reset(players):
  for player in players:
    player.Reset()
  return players

def increase_moves():
  properties.moveSavedCount = properties.moveCount
  properties.moveCount += properties.moveIncreaseAmount
  players = fitness(properties.players)
  best_player = selection(players)[0]
  properties.moveSaved = best_player.playerProperties.moves
  for player in players:
    ue.Object.Destroy(player.gameObject)
  properties.players = [newPlayer() for _ in range(properties.populationSize)]
  

def genetic_algorithm():
  players = fitness(properties.players)
  players = selection(players)
  players = crossover(players)
  players = mutation(players)
  players = reset(players)
  properties.players = players

if __name__ == "__main__":
  if properties.currentGeneration >= 10 and properties.currentGeneration % properties.generationPerMoveIncrease == 0:
    increase_moves()
  else:
    genetic_algorithm()
  
  properties.currentGeneration += 1
  properties.finishedCount = 0
  properties.isRunning = True