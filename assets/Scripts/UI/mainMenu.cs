using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void IniciarJogo()
    {
        SceneManager.LoadScene("Level_01");
    } 

    public void SairJogo()
    {
        Application.Quit();
    }
}
