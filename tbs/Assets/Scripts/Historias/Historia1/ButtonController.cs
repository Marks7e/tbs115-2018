using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip bgMusic;
    private string musicName = "Sounds/ClickInWood";
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>(musicName);
        btn.onClick.AddListener(Sound);
    }

    void Sound()
    {
        audioSource.PlayOneShot(bgMusic);
    }
}
