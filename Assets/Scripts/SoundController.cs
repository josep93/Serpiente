using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip selectButton;
    [SerializeField] private AudioClip longSelectButton;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.scene = scene;
        Debug.Log("Event");
        if (scene.name == "Menu")
        {
            sound.pitch = 2;
            return;
        }

        sound.pitch = 1;

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

    public void SelectButton()
    {
        if (sound != null)
        {
            sound.clip = selectButton;
            sound.Play();
        }
    }

    public void LongSelectButton()
    {
        if (sound != null)
        {
            sound.clip = longSelectButton;
            sound.Play();
        }
    }

}
