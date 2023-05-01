import UnityEngine

playerPrefab = UnityEngine.Resources.Load("Prefabs/Mario")

instance = UnityEngine.GameObject.Instantiate(playerPrefab)

instance.transform.position = UnityEngine.Vector3(2, 2, 0)