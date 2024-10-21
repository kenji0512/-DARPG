using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CurrentHP : MonoBehaviour
{
    [SerializeField] private int _maxHp = 5; // 最大HP
    [SerializeField] private int _hp; // 現在のHP
    [SerializeField] private Slider _hpSlider; // PlayerのHP表示用スライダー（Enemyには使用しない）

    public int HP => _hp; // HPを外部から参照可能

    public void Start()
    {
        _hp = _maxHp; // HPを最大値に設定
        InitializeHPUI(); // HP UIの初期化
    }

    private void InitializeHPUI()
    {
        // スライダーが設定されている場合、初期化を行う
        if (_hpSlider != null)
        {
            _hpSlider.maxValue = _maxHp;
            _hpSlider.value = _hp;
        }
    }

    public void Damage(int damageAmount = 1)
    {
        Debug.Log($"{gameObject.name}に{damageAmount}のダメージ");
        _hp -= damageAmount; // 減少量を引く

        if (_hp <= 0)
        {
            _hp = 0;
            Die(); // HPが0以下になったら死ぬ
        }

        // DOTweenを使ってスライダーの値をアニメーションで減らす（Playerのみ）
        if (_hpSlider != null)
        {
            _hpSlider.DOValue(_hp, 0.5f); // 0.5秒でHPの値にスムーズに変化
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name}が倒された！");
        Destroy(gameObject); // オブジェクトを削除
    }
}
