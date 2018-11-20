using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static AudioClip[] sounds = new AudioClip[9]; //Array of sound effects
    private static AudioSource audio; //Audio source

	// Use this for initialization
	void Start ()
    {
        //Movement Sounds
        sounds[0] = Resources.Load<AudioClip>("Button_20_Pack2");
        sounds[1] = Resources.Load<AudioClip>("Button_22_Pack2");
        sounds[2] = Resources.Load<AudioClip>("Button_23_Pack2");
        sounds[3] = Resources.Load<AudioClip>("Button_24_Pack2");
        sounds[4] = Resources.Load<AudioClip>("Button_25_Pack2");
        sounds[5] = Resources.Load<AudioClip>("Button_26_Pack2");
        sounds[6] = Resources.Load<AudioClip>("Button_27_Pack2");
        //Start Sound
        sounds[7] = Resources.Load<AudioClip>("Button_1_Pack2");
        //Color Sound
        sounds[8] = Resources.Load<AudioClip>("Button_17_Pack2");

        audio = GetComponent<AudioSource>(); //Sets the scenes audio source to audio
    }

    public static void MoveSound()
    {
        audio.PlayOneShot(sounds[Random.Range(0, 7)]);
    }

    public static void StartSound()
    {
        audio.PlayOneShot(sounds[7]);
    }

    public static void ColorSound()
    {
        audio.PlayOneShot(sounds[8]);
    }
    
}
