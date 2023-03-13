using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ability/ShootAbility")]

public class ShootAbility : Ability
{
    [SerializeField] private GameObject _bullet;

    public override void Use(PlayerMovment player)
    {
        Instantiate(_bullet, player.transform.position, player.transform.rotation);
    }
}
