using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public float _speed;
    public float _maxSpeed;
    public GameObject _player;
    public int _state; //0 idle, 1 run left, 2 run right, 3 rest

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        rigid           = GetComponent<Rigidbody2D>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
        animator        = GetComponent<Animator>();
        _state = 0;
    }

    private void Update()
    {
        if (0 == _state)
        {
            Vector2 myPos       = transform.position;
            Vector2 playerPos   = _player.transform.position;
            if (Vector2.Distance(myPos, playerPos) < 1)
            {
                if (myPos.x < playerPos.x)
                {
                    _state = 1;
                    spriteRenderer.flipX = false;
                    rigid.velocity = new Vector2(-7.0f, rigid.velocity.y);
                }
                else
                {
                    _state = 2;
                    spriteRenderer.flipX = true;
                    rigid.velocity = new Vector2(7.0f, rigid.velocity.y);
                }

                Invoke("ChangeState", Random.Range(2.0f, 4.0f));
            }
        }
    }

    void FixedUpdate()
    {
        //움직이는중
        if (1 == _state ||
            2 == _state)
        {
            //앞에 플랫폼이 없다
            if(false == CheckFlatform())
            {
                _state = 0;
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                Debug.Log("멈추고 대기");
            }
        }

        if(1 == _state)
        {
            rigid.AddForce(_speed * Vector2.left * Time.deltaTime, ForceMode2D.Impulse);
            if (rigid.velocity.x < -_maxSpeed)
            {
                rigid.velocity = new Vector2(-_maxSpeed, rigid.velocity.y);
            }
        }
        else if(2 == _state)
        {
            rigid.AddForce(_speed * Vector2.right * Time.deltaTime, ForceMode2D.Impulse);
            if (_maxSpeed < rigid.velocity.x)
            {
                rigid.velocity = new Vector2(_maxSpeed, rigid.velocity.y);
            }
        }
    }

    private void ChangeState()
    {
        CancelInvoke("ChangeState");
        switch(_state)
        {
            case 0:
                break;
            case 1:
            case 2:
                _state = 3;
                Invoke("ChangeState", Random.Range(1.0f, 3.0f));
                break;
            case 3:
                _state = 0;
                break;
        }
    }

    private bool CheckFlatform()
    {
        float front;
        if(rigid.velocity.x < 0)
        {
            front = -3.0f;
        }
        else
        {
            front = 3.0f;
        }

        Vector2 startPos = new Vector2(rigid.position.x + front, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(startPos, Vector2.down, 4, LayerMask.GetMask("Platform"));

        if (null != rayHit.collider)
        {
            return true;
        }

        return false;
    }
}
