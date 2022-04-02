using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    
    /// <summary>
    /// Cargamos la escena 1
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("Scene1");
    }

    /// <summary>
    /// Salimos del juego
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

}
