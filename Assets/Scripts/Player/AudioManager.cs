using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlayCellSound(AudioClip clip) => source.PlayOneShot(clip);

    
}