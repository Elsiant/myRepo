using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Actor
{
    public GameObject _player;

    public override int TakeDamage(int damage, bool attackerLeft)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            _currentLife = 0;

            Die();
            return _dropExp;
        }

        animator.SetTrigger("Hit");
        SoundManager._instance.PlaySound("hit");
        
        return 0;
    }

    public override void Die()
    {
        base.Die();

        _player.GetComponent<Player>().WeaponEquip();
        SoundManager._instance.PlaySound("bonusLife");
    }
}
