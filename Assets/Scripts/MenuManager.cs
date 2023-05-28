using UnityEngine;

public class MenuManager : MonoBehaviour
{
  public GameObject[] menus;

  public void ShowMenu(string menuName) {
    foreach (GameObject menu in menus) {
      if (menu.name == menuName) {
        menu.SetActive(true);
      } else {
        menu.SetActive(false);
      }
    }
  }

}
