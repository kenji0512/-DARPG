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
    [SerializeField] public GameObject _bulletprefab;
    [SerializeField] public Transform _firePoint;

    private void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.Playing)
        {
            HandleMovement();

            HandleAttack();
        }
     }

    void FixedUpdate()
    {
        // 攻撃中は移動を無効化
        if (!_isAttacking && GameManager.Instance.GetGameState() == GameManager.GameState.Playing)
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
            ShootBullet();
            _lastAttackTime = Time.time;
        }
    }
    private void ShootBullet()
    {
        if(_bulletprefab != null && _firePoint != null)
        {
            // 弾を発射する際にプレイヤーが向いている方向を取得
            Vector2 shootDirection = new Vector2(_moveInput.x, _moveInput.y);
            if (shootDirection != Vector2.zero) // 向きがゼロでない場合のみ
            {
                GameObject projectile = Instantiate(_bulletprefab, _firePoint.position, Quaternion.identity);
                // 向きに基づいて弾の回転を設定
                projectile.transform.up = shootDirection;
            }
        }// _firePoint.rotationを使って向いている方向に弾を発射
    }

    // アニメーションイベントで呼ばれる関数
    public void OnAttackAnimationEnd()
    {
        Debug.Log("Attack ended");
        _isAttacking = false; // 攻撃状態を解除
    }
}
