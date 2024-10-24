using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] string _goal = "";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(_goal);
    }
}
