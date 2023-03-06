using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targert : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
    }
}
