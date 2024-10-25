using System.Collections;
using UnityEngine;

public class PlayerController : CurrentHP
{
    [SerializeField] public float _moveSpeed = 2f; // �ړ����x
    [SerializeField] public float _attackCooldown = 0.5f; // �U���̃N�[���_�E������
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
        // �U�����͈ړ��𖳌���
        if (!_isAttacking && GameManager.Instance.GetGameState() == GameManager.GameState.Playing)
        {
            _rb.MovePosition(_rb.position + _moveInput * _moveSpeed * Time.fixedDeltaTime);
        }
    }
    private void HandleMovement()//�ړ�����
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
    private void HandleAttack()//�U����������
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
            // �e�𔭎˂���ۂɃv���C���[�������Ă���������擾
            Vector2 shootDirection = new Vector2(_moveInput.x, _moveInput.y);
            if (shootDirection != Vector2.zero) // �������[���łȂ��ꍇ�̂�
            {
                GameObject projectile = Instantiate(_bulletprefab, _firePoint.position, Quaternion.identity);
                // �����Ɋ�Â��Ēe�̉�]��ݒ�
                projectile.transform.up = shootDirection;
            }
        }// _firePoint.rotation���g���Č����Ă�������ɒe�𔭎�
    }

    // �A�j���[�V�����C�x���g�ŌĂ΂��֐�
    public void OnAttackAnimationEnd()
    {
        Debug.Log("Attack ended");
        _isAttacking = false; // �U����Ԃ�����
    }
}
