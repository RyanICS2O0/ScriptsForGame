using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; } // ensures it will be stored in the memory as the only SoundManager instance 

    private AudioSource source; //Access the audio component to play the sound


    private void Awake()
    {
        source = GetComponent<AudioSource>(); //To assign the variable 

        // Keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        // Destroy duplicate gameobject
        else if (instance != null && instance != this)

            Destroy(gameObject);
    }

    public void PlaySound (AudioClip _sound) //takes in audio clip
    {
        source.PlayOneShot (_sound);
    }
}
