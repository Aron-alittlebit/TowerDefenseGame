using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //[SerializeField] private AudioSource soundObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip clip, Transform pos, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, pos.position, volume);
    }
}
