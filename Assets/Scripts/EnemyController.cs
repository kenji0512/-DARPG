using UnityEngine;

public class EnemyController : CurrentHP
{
    [SerializeField] GameObject _player = null;
    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _chaseRange = 5.0f; // ÇöÍÍ

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
        StartChasingPlayer();

        //_rb.velocity = Vector2.zero; // vC[ªÍÍOÌêÍâ~
    }
    private void StartChasingPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer <= _chaseRange)
        {
            Vector2 targeting = (_player.transform.position - this.transform.position).normalized;
            _rb.velocity = targeting * _speed; // ¬xðÝè
        }
        else
        {
            _rb.velocity = Vector2.zero; // ÍÍOÌêÍâ~
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with: Player");
            // vC[É_[Wð^¦é
            CurrentHP playerHP = collision.gameObject.GetComponent<CurrentHP>();
            if (playerHP != null)
            {
                playerHP.Damage(1); // 1Ì_[W
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.Remove(this);
    }
    private void OnDrawGizmosSelected()
    {
        // GizmoÌFðwè
        Gizmos.color = Color.red;
        // ÇöÍÍð~Å`æ
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}
