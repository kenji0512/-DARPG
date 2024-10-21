using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CurrentHP : MonoBehaviour
{
    [SerializeField] private int _maxHp = 5; // �ő�HP
    [SerializeField] private int _hp; // ���݂�HP
    [SerializeField] private Slider _hpSlider; // Player��HP�\���p�X���C�_�[�iEnemy�ɂ͎g�p���Ȃ��j

    public int HP => _hp; // HP���O������Q�Ɖ\

    public void Start()
    {
        _hp = _maxHp; // HP���ő�l�ɐݒ�
        InitializeHPUI(); // HP UI�̏�����
    }

    private void InitializeHPUI()
    {
        // �X���C�_�[���ݒ肳��Ă���ꍇ�A���������s��
        if (_hpSlider != null)
        {
            _hpSlider.maxValue = _maxHp;
            _hpSlider.value = _hp;
        }
    }

    public void Damage(int damageAmount = 1)
    {
        Debug.Log($"{gameObject.name}��{damageAmount}�̃_���[�W");
        _hp -= damageAmount; // �����ʂ�����

        if (_hp <= 0)
        {
            _hp = 0;
            Die(); // HP��0�ȉ��ɂȂ����玀��
        }

        // DOTween���g���ăX���C�_�[�̒l���A�j���[�V�����Ō��炷�iPlayer�̂݁j
        if (_hpSlider != null)
        {
            _hpSlider.DOValue(_hp, 0.5f); // 0.5�b��HP�̒l�ɃX���[�Y�ɕω�
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name}���|���ꂽ�I");
        Destroy(gameObject); // �I�u�W�F�N�g���폜
    }
}
