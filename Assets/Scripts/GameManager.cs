using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    List<EnemyController> _enemies = new List<EnemyController>();

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                _instance = go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public void Register(EnemyController e)
    {
        if (!_enemies.Contains(e))
        {
            _enemies.Add(e);
        }
    }

    public void Remove(EnemyController e)
    {
        if (_enemies.Contains(e))
        {
            _enemies.Remove(e);
        }
    }

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    private GameState _currentGameState;

    public void SetGameState(GameState state)
    {
        _currentGameState = state;
        // ��Ԃɉ�����������ǉ�
    }

    public GameState GetGameState()
    {
        return _currentGameState;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ŕێ�����
        }
        else
        {
            Destroy(gameObject); // �����̃C���X�^���X������ꍇ�͐V�����̂�j��
        }
    }
}
