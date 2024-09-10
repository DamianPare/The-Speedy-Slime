using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip splashClip;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            audioSource.PlayOneShot(splashClip);
            Debug.Log("Splash");
        }
    }

}
