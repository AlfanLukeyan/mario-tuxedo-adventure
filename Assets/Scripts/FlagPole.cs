using System.Collections;
using UnityEditor.Scripting.Python;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        other.GetComponent<Player>().win = true;
        GetComponent<Collider2D>().isTrigger = false;
        if (!GameManager.Instance.geneticAlgorithm.isDone) {
          GameManager.Instance.geneticAlgorithm.StopPlayerMovement();
          GameManager.Instance.geneticAlgorithm.isRunning = false;
          GameManager.Instance.geneticAlgorithm.isDone = true;
          PythonRunner.RunFile($"{Application.dataPath}/Scripts/evaluate.py", "__main__");
        }
        StartCoroutine(MoveTo(flag, poleBottom.position));
        StartCoroutine(LevelCompleteSequence(other.transform));
      }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
      player.GetComponent<PlayerMovement>().inputAxis = 1;
      player.GetComponent<PlayerMovement>().enabled = false;

      yield return MoveTo(player, poleBottom.position);
      yield return MoveTo(player, player.position + Vector3.right);
      yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
      yield return MoveTo(player, castle.position);

      player.gameObject.SetActive(false);
      
      yield return new WaitForSeconds(2f);
      GameManager.Instance.LoadMenu("FinishedMenu");
    }

    private IEnumerator MoveTo(Transform subject, Vector3 position)
    {
        while (Vector3.Distance(subject.position, position) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, position, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = position;
    }

}
