using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracker : MonoBehaviour
{
    public AudioSource[] soundFX;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            soundFX[0].Play();
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            soundFX[1].Play();
        }
    }
}
