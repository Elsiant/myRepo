using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Actor
{
    public GameObject   _player;
    public int          _state; //0 idle, 1 run left, 2 run right, 3 rest, 4 die
    public float        _sight;

    public override void Awake()
    {
        base.Awake();
        
        _state = 0;
        Invoke("ChangeState", 1.0f);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (0 == _state)
        {
            Vector2 myPos = transform.position;
            Vector2 playerPos = _player.transform.position;
            if (Vector2.Distance(myPos, playerPos) < _sight &&
                Mathf.Abs(myPos.y - playerPos.y) < 2.0f)
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
                Invoke("ChangeState", Random.Range(4.0f, 6.0f));
            }
        }

        if (20.0f < Vector2.Distance(_startPos, rigid.position)) //시작위치에서 너무 멀어지면
        {
            StopMove();
            if(_startPos.x < rigid.position.x) //오른쪽으로 너무 갔으니 왼쪽으로
            {
                _state = 1;
                spriteRenderer.flipX = false;
            }
            else //너무 왼쪽으로 갔으니 오른쪽으로
            {
                _state = 2;
                spriteRenderer.flipX = true;
            }

            Invoke("ChangeState", 3.0f);
        }

        base.Update();
    }

    public override void Die()
    {
        base.Die();
        _state = 4;
        SoundManager._instance.PlaySound("kill");

        if(0.5f < Random.Range(0, 1.0f)) // 50퍼 확률로 포션 드랍
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
        switch (_state)
        {
            case 0:
                break;
            case 1:
            case 2:
                if(false == base.AddForce(1 == _state))
                {
                    StopMove();
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                    _state = 0;
                    Invoke("ChangeState", 1.0f);
                    break;
                }

                Vector2 myPos = transform.position;
                Vector2 playerPos = _player.transform.position;
                if (Mathf.Abs(myPos.x - playerPos.x) < 1.0f)
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
            case 1:
            case 2:
                _state = Random.Range(0, 3);

                if(1 == _state)
                {
                    spriteRenderer.flipX = false;
                }
                else if(2 == _state)
                {
                    spriteRenderer.flipX = true;
                }

                Invoke("ChangeState", Random.Range(3.0f, 5.0f));
                break;
            case 3:
                _state = 0;
                break;
            case 4:
                break;
        }
    }
}
