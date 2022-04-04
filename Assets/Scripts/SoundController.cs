using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    
    [SerializeField] private AudioClip windBeach;
    [SerializeField] private AudioClip templeWind;
    private static SoundController current;
    private AudioSource sound;
    private Scene scene;

    private void Awake()
    {
        if (current == null)
        {
            DontDestroyOnLoad(this.gameObject);
            current = this;
            sound = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            return;
        }

        Destroy(this.gameObject);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.scene = scene;

        if (scene.name == "Playa" || scene.name == "Playa2")
        {
            sound.clip = windBeach;
            sound.loop = true;
            sound.Play();
            return;
        }

        if (scene.name == "Templo")
        {
            sound.clip = templeWind;
            sound.Play();
            return;
        }

    }
}
