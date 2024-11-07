using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

  public void StartGameNow ()
  {
    SceneManager.LoadScene("SampleScene");
  } 

  public void QuitGame()
  {
    Application.Quit();
  }
}
