using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�ړ����x�̐ݒ�
    [SerializeField] public float _moveSpeed = 2f;
    [SerializeField] public float _attackcooldown = 0.5f;
    //Player�̏�ԊǗ�
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _animator; // Animator�R���|�[�l���g
    private bool _isAttacking = false;
    private float _lastAttacTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Fire1") && !_isAttacking && Time.time >= _lastAttacTime + _attackcooldown)
        {
            Attack();
        }
        if (!_isAttacking)
        {
            //�����Ɛ��������̓��͂��擾
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            //���͂��x�N�g���ɂ܂Ƃ߂�
            _moveInput = new Vector2(moveX, moveY).normalized;

            // �A�j���[�V�����p�����[�^��ݒ�
            _animator.SetFloat("Horizontal", _moveInput.x);
            _animator.SetFloat("Vertical", _moveInput.y);
        }


    }
    void FixedUpdate()
    {
        if (_isAttacking)
        {
            // Rigidbody2D���g���ăv���C���[���ړ�������
            _rb.MovePosition(_rb.position + _moveInput * _moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void Attack()
    {
        // �U�����[�V�������J�n
        _animator.SetTrigger("Attack");
        _isAttacking = true;
        _lastAttacTime = Time.time;

        // �U���A�j���[�V�����̏I����ɍU����Ԃ���������
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        // �U���A�j���[�V�����̏I���܂őҋ@�i�A�j���[�V�����̒����ɉ����Ē����j
        yield return new WaitForSeconds(0.5f); // 0.5�b�̓A�j���[�V�����̍Đ����Ԃɍ��킹�Ē���

        _isAttacking = false; // �U����Ԃ�����
    }
}
