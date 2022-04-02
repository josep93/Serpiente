using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] private GameObject drpResolution;
    private TMP_Dropdown drpDown;
    private bool isFull = false;
    private int resolution;
    private int[,] resolutions = new int[,] { {1920, 1080}, { 1280, 720 }, { 640, 420 } };

    private void Awake()
    {
        drpDown = drpResolution.GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        fillDropDown();
    }

    /// <summary>
    /// Alternamos entre pantalla completa o no, mostrando o no a su vez el desplegable de resoluciones
    /// </summary>
    /// <param name="fullScreen"></param>
    public void ChangeFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        isFull = fullScreen;
        drpResolution.SetActive(!fullScreen);
    }

    /// <summary>
    /// Modificamos la resolución del juego
    /// </summary>
    public void ChangeResolution(int drpResolution)
    {
        Screen.SetResolution(resolutions[drpResolution, 0], resolutions[drpResolution, 1], Screen.fullScreen);
    }

    /// <summary>
    /// Guardamos los datos de configuración
    /// </summary>
    private void SaveSettings()
    {
        PlayerPrefs.SetInt("fullScreen", isFull ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Cargamos los datos guardadosz
    /// </summary>
    private void LoadSetting()
    {
        Screen.fullScreen = PlayerPrefs.GetInt("fullScreen", 1) == 1 ? true : false;
        resolution = PlayerPrefs.GetInt("resolution", 0);
        ChangeResolution(resolution);
    }

    /// <summary>
    /// Rellenamos el dropdown de la lista de resoluciones
    /// </summary>
    private void fillDropDown()
    {
        drpDown.ClearOptions();
        List<string> listResolutions = new List<string>();

        for (int i = 0; i < resolutions.Length / 2; i ++)
        {
            listResolutions.Add(resolutions[i,0] + "x" + resolutions[i,1]);
        }
        drpDown.AddOptions(listResolutions);
    }

}
