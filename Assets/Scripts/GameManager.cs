using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = new GameManager();
    List<EnemyController> _enemies = new List<EnemyController>();
    public static GameManager Instance => _instance;
    public void Register(EnemyController e)
    {
        _enemies.Add(e);
    }
    public void Remove(EnemyController e)
    {
        _enemies.Remove(e);
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
        // 状態に応じた処理を追加
    }

    public GameState GetGameState()
    {
        return _currentGameState;
    }
}
