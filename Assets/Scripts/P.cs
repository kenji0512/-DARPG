using System.Collections;
using UnityEngine;

public class P : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField, Tooltip("移動スピード")] int moveSpeed = 0;
    [SerializeField] Animator playerAnim = null;
    private Rigidbody2D rb = null;
    [SerializeField] Animator weaponAnim = null;
    public int _currentHelth = 0;
    public int maxHealth = 0;
    [SerializeField] int damage = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentHelth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;

        if (rb.velocity != Vector2.zero)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    playerAnim.SetFloat("X", 1f);
                    playerAnim.SetFloat("Y", 0);

                    weaponAnim.SetFloat("X", 1f);
                    weaponAnim.SetFloat("Y", 0);

                }
                else
                {
                    playerAnim.SetFloat("X", -1f);
                    playerAnim.SetFloat("Y", 0);

                    weaponAnim.SetFloat("X", -1f);
                    weaponAnim.SetFloat("Y", 0);
                }
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", 1);

                weaponAnim.SetFloat("X", 0);
                weaponAnim.SetFloat("Y", 1);
            }
            else
            {
                playerAnim.SetFloat("X", 0);
                playerAnim.SetFloat("Y", -1);

                weaponAnim.SetFloat("X", 0);
                weaponAnim.SetFloat("Y", -1);
            }
        }

        if (Input.GetMouseButton(0))
        {
            weaponAnim.SetTrigger("Attack");
        }

        if (_currentHelth <= 0)
        {
            Destroy(this.gameObject);
        }
        // Debug.Log(rb.velocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _currentHelth -= damage;
        }
    }

}
