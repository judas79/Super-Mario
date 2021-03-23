using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // T15 singleton instance of our soundmanager
    // if another soundmanager is created it will be deleted
    // Holds the single instance of the SoundManager that 
    // you can access from any script
    public static SoundManager Instance = null;


    // T15 define All sound effects in the game
    // T16 added MannyDies sound clip
    // All are public so you can set them in the Inspector
    public AudioClip Jump;
    public AudioClip SixMillionJump;
    public AudioClip GetCoin;
    public AudioClip RockSmash;
    public AudioClip MannyDies;
    public AudioClip SnailDies;

    // T15 Refers to the audio source added to the SoundManager
    // to play sound effects
    private AudioSource soundEffectAudio;

    // T14 Start is called before the first frame update
    // This is a singleton that makes sure you only
    // ever have one Sound Manager, Instance
    // If there is any other Sound Manager created destroy it
    void Start()
    {
        // if instance doesn't exist then create it
        if (Instance == null)
        {
            Instance = this;
        }
        // if instance is not this Instance, then destroy it
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        // get the component, that is attached to this AudioManager
        AudioSource theSource = GetComponent<AudioSource>();
        soundEffectAudio = theSource;
    }
    // T15 Other GameObjects can call this to play sounds
    // from any other class, passing in the audioclip
    // Other GameObjects can call this to play sounds
    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }
}
