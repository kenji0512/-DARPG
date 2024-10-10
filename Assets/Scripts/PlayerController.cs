using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移動速度の設定
    [SerializeField] public float _moveSpeed = 2f;
    [SerializeField] public float _attackcooldown = 0.5f;
    //Playerの状態管理
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _animator; // Animatorコンポーネント
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
            //水平と垂直方向の入力を取得
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            //入力をベクトルにまとめる
            _moveInput = new Vector2(moveX, moveY).normalized;

            // アニメーションパラメータを設定
            _animator.SetFloat("Horizontal", _moveInput.x);
            _animator.SetFloat("Vertical", _moveInput.y);
        }


    }
    void FixedUpdate()
    {
        if (_isAttacking)
        {
            // Rigidbody2Dを使ってプレイヤーを移動させる
            _rb.MovePosition(_rb.position + _moveInput * _moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void Attack()
    {
        // 攻撃モーションを開始
        _animator.SetTrigger("Attack");
        _isAttacking = true;
        _lastAttacTime = Time.time;

        // 攻撃アニメーションの終了後に攻撃状態を解除する
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        // 攻撃アニメーションの終了まで待機（アニメーションの長さに応じて調整）
        yield return new WaitForSeconds(0.5f); // 0.5秒はアニメーションの再生時間に合わせて調整

        _isAttacking = false; // 攻撃状態を解除
    }
}
