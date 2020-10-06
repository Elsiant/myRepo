using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Actor
{
    public GameObject   _player;
    public int          _state; //0 idle, 1 run left, 2 run right, 3 rest, 4 die
    public float        _sight;

    public override void Awake()
    {
        base.Awake();
        _state = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (0 == _state)
        {
            Vector2 myPos       = transform.position;
            Vector2 playerPos   = _player.transform.position;
            if (Vector2.Distance(myPos, playerPos) < _sight &&
                Mathf.Abs(myPos.y - playerPos.y) < 2.0f )
            {
                if (myPos.x < playerPos.x)
                {
                    _state = 2;
                    spriteRenderer.flipX = true;
                }
                else
                {
                    _state = 1;
                    spriteRenderer.flipX = false;
                }

                Invoke("ChangeState", Random.Range(1.0f, 3.0f));
            }
        }

        base.Update();
    }

    public override void Die()
    {
        base.Die();
        _state = 4;
        SoundManager._instance.PlaySound("kill");

        if (0.5f < Random.Range(0, 1.0f)) // 50퍼 확률로 포션 드랍
        {
            GameManager._instance.CreatePotion(rigid.position);
        }
    }

    public override void GraveBurst()
    {
        base.GraveBurst();
        _state = 0;
    }

    private void FixedUpdate()
    {
        switch(_state)
        {
            case 0:
                break;
            case 1:
            case 2:
                base.AddForce(1 == _state);

                Vector2 myPos       = transform.position;
                Vector2 playerPos   = _player.transform.position;
                if(Mathf.Abs(myPos.x - playerPos.x) < 1.0f)
                {
                    base.StopMove();
                    base.Attack(0.4f, 0.8f, 1.0f, LayerMask.GetMask("Player"));
                    animator.SetTrigger("Attack");

                    _state = 3;

                    Invoke("ResetAttackReady", 1.0f);
                }
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    private void ChangeState()
    {
        CancelInvoke("ChangeState");
        switch (_state)
        {
            case 0:
                break;
            case 1:
            case 2:
                _state = 3;
                Invoke("ChangeState", Random.Range(2.0f, 4.0f));
                break;
            case 3:
                _state = 0;
                break;
            case 4:
                break;
        }
    }
}
