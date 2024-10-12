using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] float _speed = 1.0f;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 targeting = (_player.transform.position - this.transform.position).normalized;
            _rb.velocity = new Vector2((targeting.x * _speed), (targeting.y * _speed));
        }
    }
}
