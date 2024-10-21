using System.Collections;
using UnityEngine;

public class PlayerController : CurrentHP
{
    [SerializeField] public float _moveSpeed = 2f; // 移動速度
    [SerializeField] public float _attackCooldown = 0.5f; // 攻撃のクールダウン時間
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    [SerializeField] public Animator _weaponAnim;
    [SerializeField] public Animator _playerAnim;
    private bool _isAttacking = false;
    [SerializeField] public float _lastAttackTime = 0f;

    private void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
        _weaponAnim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (_playerAnim == null)
        {
            _playerAnim = GetComponent<Animator>();
        }

        if (_weaponAnim == null)
        {
            _weaponAnim = GetComponent<Animator>(); // 必要であれば初期化
        }
    }

    void Update()
    {
        HandleMovement();

        //HandleAttack();
    }

    void FixedUpdate()
    {
        // 攻撃中は移動を無効化
        if (!_isAttacking)
        {
            _rb.MovePosition(_rb.position + _moveInput * _moveSpeed * Time.fixedDeltaTime);
        }
    }
    private void HandleMovement()//移動処理
    {
        if (!_isAttacking)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            _moveInput = new Vector2(moveX, moveY).normalized;
            _playerAnim.SetFloat("Horizontal", _moveInput.x);
            _playerAnim.SetFloat("Vertical", _moveInput.y);
        }
    }
    private void HandleAttack()//攻撃発動処理
    {
        if (Input.GetButtonDown("Fire1") && !_isAttacking && Time.time >= _lastAttackTime + _attackCooldown)
        {
            Attack();
        }
    }
    private void Attack()//攻撃処理
    {
        // 攻撃Blend Treeを有効化する
        _weaponAnim.SetFloat("Horizontal", _moveInput.x);
        _weaponAnim.SetFloat("Vertical", _moveInput.y);
        Debug.Log($"Attack Direction: Horizontal={_moveInput.x}, Vertical={_moveInput.y}");
        _isAttacking = true;
        _lastAttackTime = Time.time;

        Damage();
    }

    // アニメーションイベントで呼ばれる関数
    public void OnAttackAnimationEnd()
    {
        Debug.Log("Attack ended");
        _isAttacking = false; // 攻撃状態を解除
    }
}
