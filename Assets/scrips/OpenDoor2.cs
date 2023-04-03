using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor2 : MonoBehaviour, IInteractable
{
    public AudioSource soundPlayer;
    public void Interact()
    {
        Destroy(GameObject.FindWithTag("deurOpenen2"));
        soundPlayer.Play();
    }
}
