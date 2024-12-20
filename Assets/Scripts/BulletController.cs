using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 1;
     // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        if (_rb != null)
        {
            _rb.velocity = transform.up * _speed;
        }

        Destroy(gameObject, _lifeTime);
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
                Destroy(gameObject);//弾を削除
            }
            // 敵や他の障害物に当たったら弾を削除する（必要ならば）
            if (collision.CompareTag("Wall"))
            {
                Destroy(gameObject); // 壁に当たった場合も弾を削除
            }
        }
    }
}
