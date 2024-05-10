using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpcionesMenu : MonoBehaviour
{
    public void Comenzar(string juego)
    {
        SceneManager.LoadScene(juego);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
