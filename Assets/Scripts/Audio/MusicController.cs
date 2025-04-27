using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void AddPitch(float add, bool isNeg)
    {
        _audioSource.pitch += add * (isNeg ? -1 : 1);
    }
}
