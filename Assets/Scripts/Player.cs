using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Fighter
{
    private Vector3 moveDelta;

    private MoveState _moveState = MoveState.Idle;
    private DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private BoxCollider2D _boxCollider;
    public Animator _animator;
    private float _walkTime = 0, _walkCD = 0.1f;
    public bool isDead = false;
    public int evadeChance;
    public int damageBoost;


    private RaycastHit2D hit;
    public void MoveRight()
    {
        _moveState = MoveState.Walk;

        if (_directionState != DirectionState.Right)
            _directionState = DirectionState.Right;

        _walkTime = _walkCD;
        _animator.Play("WalkRight");
    }
    public void MoveLeft()
    {
        _moveState = MoveState.Walk;

        if (_directionState != DirectionState.Left)
            _directionState = DirectionState.Left;

        _walkTime = _walkCD;
        _animator.Play("WalkLeft");
    }

    public void MoveUp()
    {
        _moveState = MoveState.Walk;

        if (_directionState != DirectionState.Up)
            _directionState = DirectionState.Up;

        _walkTime = _walkCD;
        _animator.Play("WalkTop");
    }

    public void MoveDown()
    {
        _moveState = MoveState.Walk;

        if (_directionState == DirectionState.Up)
            _directionState = DirectionState.Right;

        _walkTime = _walkCD;

        if (_directionState == DirectionState.Left)
            _animator.Play("WalkLeft");
        else if (_directionState == DirectionState.Right)
            _animator.Play("WalkRight");


    }

    public void Idle()
    {
        _moveState = MoveState.Idle;

        if (_directionState == DirectionState.Left)
            _animator.Play("IdleLeft");
        if (_directionState == DirectionState.Right)
            _animator.Play("IdleRight");
        if (_directionState == DirectionState.Up)
            _animator.Play("IdleTop");
    }

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _boxCollider.name = "Player";
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isMoving == true)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            moveDelta = new Vector3(x, y, 0);


            hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(0, moveDelta.y),
                Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
            if (hit.collider == null)
            {
                _transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
            }

            hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(moveDelta.x, 0),
                Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
            if (hit.collider == null)
            {
                _transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
            }


            if (_moveState == MoveState.Walk)
            {
                _walkTime -= Time.deltaTime;
                if (_walkTime <= 0)
                {
                    Idle();
                }
            }
        }
        maxHitpoint = 100 + (5 * GameManager.instance.GetCurrentLevel());
        GameManager.instance.agility = GameManager.instance.GetCurrentLevel();
        GameManager.instance.strength = GameManager.instance.GetCurrentLevel();
        evadeChance = GameManager.instance.agility + 20;
        damageBoost = GameManager.instance.strength + 5;        

        GameManager.instance.WeaponDamage = GameManager.instance.weaponDmgArr[GameManager.instance.weaponNum];

        if (GameManager.instance.weaponNum == 2 || GameManager.instance.weaponNum == 6 || GameManager.instance.weaponNum == 10 || GameManager.instance.weaponNum == 14 || GameManager.instance.weaponNum == 18)
            GameManager.instance.isBow = true;
        else
            GameManager.instance.isBow = false;

    }

    enum DirectionState
    {
        Left,
        Right,
        Up
    }

    enum MoveState
    {
        Idle,
        Walk
    }


    public override void RecieveDamage(Damage dmg)
    {
        base.RecieveDamage(dmg);
    }

    public override void Death()
    {
        isDead = true;
    }

    public void Heal(int healAmount)
    {        
        hitpoint += healAmount;
        GameManager.instance.ShowText("+" + healAmount.ToString() + " здр", 25, Color.green, transform.position, Vector3.up * 50, 2f);
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;        
    }
}