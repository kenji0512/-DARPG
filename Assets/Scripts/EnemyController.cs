using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _chaseRange = 5.0f; // 追尾範囲
    private bool _isPlayerInRange = false;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer <= _chaseRange)
            {
                Vector2 targeting = (_player.transform.position - this.transform.position).normalized;
                _rb.velocity = new Vector2(targeting.x * _speed, targeting.y * _speed);
            }
            else
            {
                _rb.velocity = Vector2.zero; // プレイヤーが範囲外の場合は停止
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerInRange = false;
            _rb.velocity = Vector2.zero; // プレイヤーが範囲外に出たら敵の移動を停止
        }
    }
}
