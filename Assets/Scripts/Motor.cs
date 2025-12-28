using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Motor : MonoBehaviour
{
    //INPUTS
    [Header("Inputs (Config)")]
    [SerializeField] private float speed = 0.5f;

    [Header("Inputs (Dependencies)")]
    [Tooltip("Ordered by priority: element 0 has highest priority.")]
    [SerializeField] private MonoBehaviour[] providerBehaviours;

    private IMoveProvider[] providers;
    private Rigidbody2D rb;

    public MoveCommand CurrentCommand { get; private set; } = MoveCommand.Idle; //read-only
    public Vector2 DesiredMove => CurrentCommand.Move; //normalized movement direction used by the motor (magnitude <= 1)

    public bool IsSketching => CurrentCommand.IsSketching; //True when sketch mode is active
    public bool IsWalking => !IsSketching && DesiredMove != Vector2.zero; //True when moving and not sketching
    public bool IsIdle => !IsSketching && DesiredMove == Vector2.zero; //True when not moving and not sketching

    //Last non-zero movement direction, updated when movement stops (not when sketch toggles)
    public Vector2 LastMoveDirection { get; private set; } = Vector2.down;

    public Vector2 Velocity => rb != null ? rb.velocity : Vector2.zero;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //Convert serialized MonoBehaviours to IMoveProvider array once
        providers = new IMoveProvider[providerBehaviours.Length];
        for (int i = 0; i < providerBehaviours.Length; i++)
        {
            providers[i] = providerBehaviours[i] as IMoveProvider;
            if (providerBehaviours[i] != null && providers[i] == null)
            {
                Debug.LogError($"{name}: Provider at index {i} does not implement IMoveProvider.");
            }
        }
    }

    private void Update()
    {
        MoveCommand prev = CurrentCommand;
        CurrentCommand = PullCommandFromProviders();

        //Normalize movement (prevents diagonal speed boost)
        Vector2 move = CurrentCommand.Move;
        if (move.sqrMagnitude > 1f) move.Normalize();
        CurrentCommand = new MoveCommand(move, CurrentCommand.IsSketching);

        //Update last direction when movement stops (walking -> idle), not when entering sketch.
        bool wasMoving = !prev.IsSketching && prev.Move != Vector2.zero;
        bool nowWalking = !CurrentCommand.IsSketching && CurrentCommand.Move != Vector2.zero;

        if (wasMoving && !nowWalking)
            LastMoveDirection = prev.Move;
    }

    private void FixedUpdate()
    {
        //Sketching means no movement
        Vector2 vel = CurrentCommand.IsSketching ? Vector2.zero : CurrentCommand.Move * speed;
        rb.velocity = vel;
    }

    private MoveCommand PullCommandFromProviders()
    {
        for (int i = 0; i < providers.Length; i++) {
            IMoveProvider p = providers[i];

            if (p == null) continue;
            if (p.TryGetMoveCommand(out MoveCommand cmd))
                return cmd;
        }

        return MoveCommand.Idle;
    }
}
