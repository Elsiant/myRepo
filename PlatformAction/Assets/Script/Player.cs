using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public float _jumpForce;
    public float _attackSpeed;

    private bool _controllable;
    CapsuleCollider2D capsuleCollider;

    public override void Awake()
    {
        base.Awake();

        _controllable = true;
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        GameManager._instance.SetLifeText(_currentLife);
        GameManager._instance.ScoreUp(0);

        SoundManager._instance.PlayBgm("mission");
    }

    // Update is called once per frame
    public override void Update()
    {
        if(false == _controllable)
        {
            return;
        }

        //Attack
        if (true == _attackReady &&
            true == Input.GetButton("Fire1"))
        {
            //지상
            //if (false == animator.GetBool("IsJumping"))
            {
                SoundManager._instance.PlaySound("hammer");
                animator.SetTrigger("Attack");

                rigid.velocity = new Vector2(0, rigid.velocity.y);

                int score = 0;
                if(false == animator.GetBool("IsWeapon"))
                {
                    score = base.Attack(0.5f, 1.0f, 1.0f, LayerMask.GetMask("Enemy"));
                }
                else
                {
                    score = base.Attack(0.7f, 1.4f, 1.0f, LayerMask.GetMask("Enemy"));
                }

                if(0 != score)
                {
                    GameManager._instance.ScoreUp(score);
                }                

                _attackReady = false;

                Invoke("ResetAttackReady", _attackSpeed);
            }
        }

        //Jump
        if (true == Input.GetButtonDown("Jump") &&
            false == animator.GetBool("IsJumping"))
        {
            rigid.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
            SoundManager._instance.PlaySound("jump");
        }

        //Move
        if (true == Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * _stopSpeed, rigid.velocity.y);
        }

        //왼쪽이면 true 오른쪽이면 false
        if (true == Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
        }

        base.Update();
    }

    public override int TakeDamage(int damage, bool attackerLeft)
    {
        int returnValue = base.TakeDamage(damage, attackerLeft);

        GameManager._instance.SetLifeText(_currentLife);

        return returnValue;
    }

    public override void Die()
    {
        base.Die();
        _controllable = false;
        SoundManager._instance.PlaySound("die");
        GameManager._instance.SetRetryActive(true);
        CancelInvoke("HideAndReset");
    }

    public void WeaponEquip()
    {
        Invoke("ChangeWeapon", 0.4f);

    }

    public int GetCurrentLife()
    {
        return _currentLife;
    }

    public void LifeUp(int life)
    {
        _currentLife += life;
        if(_maxLife < _currentLife)
        {
            _currentLife = _maxLife;
        }

        GameManager._instance.SetLifeText(_currentLife);
    }

    public void ResetPosition()
    {
        transform.position = _startPos;
        rigid.velocity = Vector2.zero;
    }

    public void ResetStage()
    {
        transform.position = _startPos;
        rigid.velocity = Vector2.zero;
        _currentLife = _maxLife;
        GameManager._instance.SetLifeText(_currentLife);
        gameObject.layer = _layerInit;

        GameManager._instance.ResetStage();
        GameManager._instance.SetRetryActive(false);

        animator.SetTrigger("GraveBurst");
        _controllable = true;
    }

    private void ChangeWeapon()
    {
        animator.SetBool("IsWeapon", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if("item" == collision.gameObject.tag)
        {
            Debug.Log("item");
        }
    }

    void FixedUpdate()
    {
        if (false == _controllable)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal") * 2;

        

        rigid.AddForce(new Vector2(h, 0), ForceMode2D.Impulse);

        if (Mathf.Abs(rigid.velocity.x) > _maxSpeed)
        {
            if (rigid.velocity.x < 0)
            {
                rigid.velocity = new Vector2(-_maxSpeed, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(_maxSpeed, rigid.velocity.y);
            }
        }

        //Lending Platform
        if (true == animator.GetBool("IsJumping") &&
            rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 4, LayerMask.GetMask("Platform"));

            if (null != rayHit.collider)
            {
                Debug.Log(rayHit.collider.name);
                if (rayHit.distance - 0.05f <= (capsuleCollider.size.y / 2.0f))
                {
                    animator.SetBool("IsJumping", false);
                }
            }
        }
    }
}
