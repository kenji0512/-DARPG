using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private int _damage = 1;
     // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        if (_rb != null)
        {
            _rb.velocity = transform.up * _speed;
        }

        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CurrentHP enemyHP = collision.GetComponent<CurrentHP>();
            if (enemyHP != null)
            {
                enemyHP.Damage(_damage);
                Destroy(gameObject);//íeÇçÌèú
            }
        }
    }
}
