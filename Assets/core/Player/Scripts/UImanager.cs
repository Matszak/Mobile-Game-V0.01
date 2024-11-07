using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
     
    [SerializeField] private GameObject restartButton; 
    // Start is called before the first frame update
    void Start()
    {
        restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowButtons()
    {
        restartButton.SetActive(true);

        
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}