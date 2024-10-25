using UnityEngine;

public class EnemyController : CurrentHP
{
    [SerializeField] GameObject _player = null;
    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _chaseRange = 5.0f; // 追尾範囲
    private bool _isPlayerInRange = false;

    private Rigidbody2D _rb;
    private CurrentHP _currentHP;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHP = GetComponent<CurrentHP>();
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
        GameManager.Instance.Register(this);
    }

    // Update is called once per frame
    void Update()
    {

        if (_isPlayerInRange && _player != null)
        {
            StartChasingPlayer();
        }
        else
        {
            _rb.velocity = Vector2.zero; // プレイヤーが範囲外の場合は停止
        }
    }
    private void StartChasingPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer <= _chaseRange)
        {
            Vector2 targeting = (_player.transform.position - this.transform.position).normalized;
            _rb.velocity = targeting * _speed; // 速度を設定
        }
        else
        {
            _rb.velocity = Vector2.zero; // 範囲外の場合は停止
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with: Player");
            // プレイヤーにダメージを与える処理
            CurrentHP playerHP = collision.gameObject.GetComponent<CurrentHP>();
            if (playerHP != null)
            {
                playerHP.Damage(1); // 1のダメージ
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isPlayerInRange = true;
            StartChasingPlayer();
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
    private void OmDestroy()
    {
        GameManager.Instance.Remove(this);
    }
}
