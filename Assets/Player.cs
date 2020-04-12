using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speeds")]
    public float WalkSpeed = 3;

    private MoveState _moveState = MoveState.Idle;
    private DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private Animator _animatorController;
    private BoxCollider2D _boxCollider2D;
    private float _walkTime = 0, _walkCooldown = 0.2f;
    private float _attackTime = 0, _attackCooldown = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animatorController = GetComponent<Animator>();
        _directionState = transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveState == MoveState.Attack)
        {
            _attackTime -= Time.deltaTime;

            if (_attackTime <= 0)
            {
                Idle();
            }
        }
        else if (_moveState == MoveState.Walk)
        {
            Vector3 direction = ((_directionState == DirectionState.Right ? Vector2.right : -Vector2.right)
                                    * WalkSpeed * Time.deltaTime);

            _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + direction, WalkSpeed * Time.deltaTime);

            _walkTime -= Time.deltaTime;

            if (_walkTime <= 0)
            {
                Idle();
            }
        }
    }

    public void MoveLeft()
    {
        if (_moveState != MoveState.Attack)
        {
            _moveState = MoveState.Walk;
            if (_directionState == DirectionState.Right)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                _directionState = DirectionState.Left;
            }
            _walkTime = _walkCooldown;
            _animatorController.Play("walk");
        }
    }

    public void MoveRight()
    {
        if (_moveState != MoveState.Attack)
        {
            _moveState = MoveState.Walk;
            if (_directionState == DirectionState.Left)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                _directionState = DirectionState.Right;
            }
            _walkTime = _walkCooldown;
            _animatorController.Play("walk");
        }
    }

    public void Attack()
    {
        if (_moveState != MoveState.Attack)
        {
            _moveState = MoveState.Attack;
            _animatorController.Play("attack");
            _attackTime = _attackCooldown;
        }
    }

    private void Idle()
    {
        _moveState = MoveState.Idle;
        _animatorController.Play("idle");
    }

    private void OnTriggerStay2D(Collider2D collider2d)
    {
        Enemy enemy = collider2d.GetComponent<Enemy>();
        if (enemy)
        {
            if (_moveState == MoveState.Attack)
            {
                enemy.Damage(Random.Range(7, 22));
            }
        }
    }

    enum DirectionState
    {
        Right,
        Left
    }

    enum MoveState
    {
        Idle,
        Walk,
        Attack
    }
}
