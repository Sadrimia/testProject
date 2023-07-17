using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource _as;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        _as = GetComponent<AudioSource>();
        _as.mute = PlayerPrefs.GetInt("Music", 0) == 1 ? true : false;
    }

    private void OnEnable()
    {
        CustomToggle.OnValueChanged += MuteMusic;
    }
    private void OnDisable()
    {
        CustomToggle.OnValueChanged -= MuteMusic;
    }

    private void MuteMusic(bool on)
    {
        _as.mute = on;
    }
}
