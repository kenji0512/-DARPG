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
                Destroy(gameObject);//’e‚ğíœ
            }
            // “G‚â‘¼‚ÌáŠQ•¨‚É“–‚½‚Á‚½‚ç’e‚ğíœ‚·‚éi•K—v‚È‚ç‚Îj
            if (collision.CompareTag("Wall"))
            {
                Destroy(gameObject); // •Ç‚É“–‚½‚Á‚½ê‡‚à’e‚ğíœ
            }
        }
    }
}
