using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private playerMovment pm;

    [Header("Sliding")]
    public float maxSliderTime;
    public float slideForce;
    private float sliderTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private bool slidingS;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<playerMovment>();

        startYScale = playerObj.localScale.y;
    }

    private void startSlide()
    {

    }
    
    private void
}
