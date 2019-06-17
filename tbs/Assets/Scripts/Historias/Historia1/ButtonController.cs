using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Global;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgMusic;
    private string _musicName = "Sounds/ClickInWood";
    public Button btn;
    public DependencyInjector _di;


    // Start is called before the first frame update
    void Start()
    {
        _di= new DependencyInjector();
        audioSource = GetComponent<AudioSource>();
        bgMusic = Resources.Load<AudioClip>(_musicName);
        btn.onClick.AddListener(Sound);

        GetGeneralVolume();
    }

    void Sound()
    {
        audioSource.PlayOneShot(bgMusic);
    }
    
    private void GetGeneralVolume()
    {
        float generalVolume = 0.0f;
        generalVolume = GlobalVariables.GeneralVolume;
        audioSource.volume = generalVolume;
    }
}
