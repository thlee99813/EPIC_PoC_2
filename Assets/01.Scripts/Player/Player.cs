using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerGlide))]

public class Player : MonoBehaviour
{
    public PlayerStats Stats { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerGroundChecker GroundChecker { get; private set; }
    public PlayerMove Move { get; private set; }
    public PlayerSlopeSlide SlopeSlide { get; private set; }
    public PlayerGlide Glide { get; private set; }


    private void Awake()
    {
        Stats = GetComponent<PlayerStats>();
        Input = GetComponent<PlayerInput>();
        GroundChecker = GetComponent<PlayerGroundChecker>();
        Move = GetComponent<PlayerMove>();
        SlopeSlide = GetComponent<PlayerSlopeSlide>();
        Glide = GetComponent<PlayerGlide>();

    }
}
