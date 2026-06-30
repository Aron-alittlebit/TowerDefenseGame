using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask Ground;
    float groundRadius = 0.4f;

    Vector3 velocity;
    public float Speed;
    public float JumpHeight;
    bool IsGrounded = false;
    float gravity = 2 * -9.18f;
    int jumpCount = 0;
    Animator animator;

    private void Start()
    {
        Speed = GetComponent<CharacterChanging>().CurrentHero.Speed;
    }

    private void OnEnable()
    {
        PlayerEvents.OnHeroChanged += SetSpeed;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHeroChanged -= SetSpeed;
    }

    public void SetAnimator(Animator newAnimator) => animator = newAnimator;

    void Update()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, groundRadius, Ground);

        if(IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal") ;
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * z + transform.right * x;
        move.y = 0;

        characterController.Move(Speed * Time.deltaTime * move);

        animator.SetBool("IsRunning", move != Vector3.zero);

        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            
            velocity.y = Mathf.Sqrt(gravity* -2f * JumpHeight);
        }

        if (!IsGrounded)
            velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    void SetSpeed(HeroData hero)
    {
        Speed = hero.Speed;
    }
}
