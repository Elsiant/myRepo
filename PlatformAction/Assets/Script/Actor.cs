using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float _speed;
    public float _maxSpeed;
    public float _stopSpeed;
    public int  _maxLife;
    public int  _maxSp;
    public int _damage;
    public int _dropExp;

    protected bool _attackReady;
    protected int _currentLife;
    protected int _currentSp;
    protected int _currentExp;

    protected int _layerInit;

    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected Vector2 _startPos;

    public virtual void Awake()
    {
        rigid           = GetComponent<Rigidbody2D>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
        animator        = GetComponent<Animator>();
        
        _attackReady    = true;
        _currentLife    = _maxLife;
        _currentSp      = _maxSp;

        _layerInit = gameObject.layer;

        _startPos = transform.position;
    }
    
    public virtual void Update()
    {
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

    protected bool CheckFlatform()
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

        Vector2         startPos    = new Vector2(rigid.position.x + front, rigid.position.y);
        RaycastHit2D    rayHit      = Physics2D.Raycast(startPos, Vector2.down, 4, LayerMask.GetMask("Platform"));

        if (null != rayHit.collider)
        {
            return true;
        }

        return false;
    }

    protected RaycastHit2D GetObjectByRay(float distance, float rayLenth = 4)
    {
        float dist = distance;
        if (false == spriteRenderer.flipX)
        {
            dist *= -1;
        }

        Vector2         startPos = new Vector2(rigid.position.x + dist, rigid.position.y);

        return Physics2D.Raycast(startPos, Vector2.down, 4, LayerMask.GetMask("Platform"));
    }

    protected bool AddForce(bool left, bool forceMove = false)
    {
        if(null == GetObjectByRay(1).collider &&
            false == forceMove)
        {
            return false;
        }

        int direction = 1;
        if(true == left)
        {
            direction = -1;
        }

        rigid.AddForce(new Vector2(direction * _speed * Time.deltaTime, 0), ForceMode2D.Impulse);
        
        if (rigid.velocity.x < -_maxSpeed)
        {
            rigid.velocity = new Vector2(-_maxSpeed, rigid.velocity.y);
        }
        else if (_maxSpeed < rigid.velocity.x)
        {
            rigid.velocity = new Vector2(_maxSpeed, rigid.velocity.y);
        }

        return true;
    }

    protected void StopMove()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    protected int Attack(float distance, float boxWidth, float boxHeight, LayerMask layerMask)
    {
        int exp = 0;
        float dist = distance;
        if(false == spriteRenderer.flipX)
        {
            dist *= -1;
        }

        Vector2 position = new Vector2(rigid.position.x + dist, rigid.position.y);
        var targets = Physics2D.OverlapBoxAll(position, new Vector2(boxWidth, boxHeight), 0.0f, layerMask);

        foreach(var target in targets)
        {
            var actor = target.GetComponent<Actor>();
            if(null == actor)
            {
                continue;
            }
            
            exp += target.GetComponent<Actor>().TakeDamage(_damage, rigid.position.x < target.transform.position.x);
        }

        return exp;
    }

    public virtual int TakeDamage(int damage, bool attackerLeft)
    {
        _currentLife -= damage;        

        if(_currentLife <= 0)
        {
            _currentLife = 0;

            Die();
            return _dropExp;
        }
        
        animator.SetTrigger("Hit");
        SoundManager._instance.PlaySound("hit");

        rigid.AddForce(new Vector2((attackerLeft == true) ? 20.0f : -20.0f , 16.0f), ForceMode2D.Impulse);

        StartDamagedState();

        return 0;
    }

    public virtual void GraveBurst()
    {
        this.gameObject.SetActive(true);
        _currentLife = _maxLife;
        _attackReady = true;
        transform.position = _startPos;
        animator.SetTrigger("Hit");
        gameObject.layer = _layerInit;
    }

    protected void StartDamagedState()
    {
        gameObject.layer = _layerInit + 1;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);

        Invoke("EndDamagedSatate", 1.0f);
    }

    protected void EndDamagedSatate()
    {
        gameObject.layer = _layerInit;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    
    protected void ResetAttackReady()
    {
        _attackReady = true;
    }

    public virtual void Die()
    {
        animator.SetTrigger("Die");

        gameObject.layer = _layerInit + 1;

        Invoke("HideAndReset", 4.0f);
    }

    protected void HideAndReset()
    {
        this.gameObject.SetActive(false);
    }

    protected void ShowLifeBar(float second)
    {
        CancelInvoke("HideLifeBar");

        //체력바 보여줌

        Invoke("HideLifeBar", second);
    }

    protected void HideLifeBar()
    {
        //체력바 숨김
    }
}
