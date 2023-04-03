using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractable
{
    public AudioSource soundPlayer;
    public void Interact()
    {
        Destroy(GameObject.FindWithTag("deurOpenen"));
        soundPlayer.Play();

    }
}

