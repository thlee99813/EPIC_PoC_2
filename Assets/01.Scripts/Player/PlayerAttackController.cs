using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerUmbrella))]
[RequireComponent(typeof(PlayerUmbrellaShooter))]
[RequireComponent(typeof(PlayerMeleeAttack))]
public class PlayerAttackController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerUmbrella _playerUmbrella;
    private PlayerUmbrellaShooter _umbrellaShooter;
    private PlayerMeleeAttack _meleeAttack;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerUmbrella = GetComponent<PlayerUmbrella>();
        _umbrellaShooter = GetComponent<PlayerUmbrellaShooter>();
        _meleeAttack = GetComponent<PlayerMeleeAttack>();
    }

    private void Update()
    {
        if (!_playerInput.ConsumeAttackPressed())
        {
            return;
        }

        if (_playerUmbrella.IsBlockingProjectile)
        {
            _umbrellaShooter.Shoot();
            return;
        }

        _meleeAttack.TryAttack();
    }
}
