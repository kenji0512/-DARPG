using UnityEngine;

public class EnemyController : CurrentHP
{
    [SerializeField] GameObject _player = null;
    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _chaseRange = 5.0f; // �ǔ��͈�

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

        //_rb.velocity = Vector2.zero; // �v���C���[���͈͊O�̏ꍇ�͒�~
    }
    private void StartChasingPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer <= _chaseRange)
        {
            Vector2 targeting = (_player.transform.position - this.transform.position).normalized;
            _rb.velocity = targeting * _speed; // ���x��ݒ�
        }
        else
        {
            _rb.velocity = Vector2.zero; // �͈͊O�̏ꍇ�͒�~
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with: Player");
            // �v���C���[�Ƀ_���[�W��^���鏈��
            CurrentHP playerHP = collision.gameObject.GetComponent<CurrentHP>();
            if (playerHP != null)
            {
                playerHP.Damage(1); // 1�̃_���[�W
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.Remove(this);
    }
    private void OnDrawGizmosSelected()
    {
        // Gizmo�̐F���w��
        Gizmos.color = Color.red;
        // �ǔ��͈͂��~�ŕ`��
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}
