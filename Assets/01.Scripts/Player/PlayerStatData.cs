using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatData", menuName = "Rainy Dancer/Player Stat Data")]
public class PlayerStatData : ScriptableObject
{
    [Header("Health")]
    [SerializeField] private float _maxHealth = 100f;

    [Header("SP")]
    [SerializeField] private float _maxSP = 100f;

    [Header("Combat")]
    [SerializeField] private int _attackPower = 10;

    [Header("Move")]
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _jumpForce = 5f;

    [Header("Slope Slide")]
    [SerializeField] private float _slideSpeed = 15f;
    [SerializeField] private float _slideAcceleration = 5f;

    [Header("Rain")]
    [SerializeField] private float _rainResistance = 0f;

    public float MaxHealth => _maxHealth;
    public int AttackPower => _attackPower;
    public float MoveSpeed => _moveSpeed;
    public float JumpForce => _jumpForce;
    public float SlideSpeed => _slideSpeed;
    public float SlideAcceleration => _slideAcceleration;
    public float RainResistance => _rainResistance;
    public float MaxSP => _maxSP;

}
