using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float _jumpForce;
    public float _maxSpeed;
    public float _stopSpeed;
    public float _attackSpeed;

    Rigidbody2D         rigid;
    SpriteRenderer      spriteRenderer;
    Animator            animator;
    CapsuleCollider2D   capsuleCollider;

    private bool _attackReady;
    private int _attackLevel;
    private int _attackMax;

    void Awake()
    {
        rigid           = GetComponent<Rigidbody2D>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
        animator        = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        _attackReady = true;
        _attackLevel = 0;
        _attackMax = 3;
    }

    void Update()
    {
        //Attack
        if (true == _attackReady &&
            true == Input.GetButton("Fire1"))
        {
            //지상
            if (false == animator.GetBool("IsJumping"))
            {
                animator.SetTrigger("Attack");

                rigid.velocity = new Vector2(0, rigid.velocity.y);

                _attackReady = false;

                Invoke("ResetAttackReady", _attackSpeed);

                _attackLevel++;
                if (_attackLevel == _attackMax)
                {
                    _attackLevel = 0;
                }
            }
        }

        //Jump
        if (true == Input.GetButtonDown("Jump") &&
            false == animator.GetBool("IsJumping"))
        {
            rigid.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
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

        //이동중이면 상태 변환
        if (Mathf.Abs(rigid.velocity.x) > _stopSpeed)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Floor" == other.tag &&
            rigid.velocity.y < 0)
        {

            animator.SetBool("IsJumping", false);
        }
    }

    private void ResetAttackReady()
    {
        _attackReady = true;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal") * 2;

        //if(false == animator.GetBool("IsJumping"))
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
                if (rayHit.distance + 0.3f <= (capsuleCollider.size.y / 2.0f) )
                {
                    animator.SetBool("IsJumping", false);
                }
            }
        }
    }
}
