using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour
{
    public PlayerHealth Health { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerGroundChecker GroundChecker { get; private set; }
    public PlayerMove Move { get; private set; }
    public PlayerSlopeSlide SlopeSlide { get; private set; }

    private void Awake()
    {
        Health = GetComponent<PlayerHealth>();
        Input = GetComponent<PlayerInput>();
        GroundChecker = GetComponent<PlayerGroundChecker>();
        Move = GetComponent<PlayerMove>();
        SlopeSlide = GetComponent<PlayerSlopeSlide>();
    }
}
