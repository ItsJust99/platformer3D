using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private KeyCode _fireKey;
    public AudioSource soundPlayer;
    void Update()
    {
        if (Input.GetKey(_fireKey))
        {
            soundPlayer.Play();
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
