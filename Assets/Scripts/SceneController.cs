using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Collider ChangeScene;
    public Button StartScene;
    public string SceneA;
    public Button GameScene;
    public string SceneB;

    [SerializeField] private GameObject _loadingUI;
    [SerializeField] private Slider _slider;

    public void MoveA()
    {
        SceneManager.LoadScene(SceneA);
    }

    public void MoveLoadNextSceneB()
    {
        _loadingUI.SetActive(true);
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneA);
        AsyncOperation async1 = SceneManager.LoadSceneAsync(SceneB);
        while (!async.isDone)
        {
            _slider.value = async.progress;
            yield return null;
        }
        while (!async1.isDone)
        {
            _slider.value = async.progress;
            yield return null;
        }
    }

    public void MoveB()
    {
        SceneManager.LoadScene(SceneB);
    }

    ////コライダ―に入ったらscene切替
    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneA);
    }
    public void MoveLoadNextSceneC()
    {
        _loadingUI.SetActive(true);
        StartCoroutine(LoadNextScene());
    }
}
