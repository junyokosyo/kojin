using UnityEditor.Tilemaps;
using UnityEngine;

// Rigidbody2D�R���|�[�l���g���K�{�ł��邱�Ƃ�����
[RequireComponent(typeof(Rigidbody2D))]
// Collider2D�R���|�[�l���g���K�{�ł��邱�Ƃ�����
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    //Header�͌��o��
    [Header("�ړ��ݒ�")]
    [SerializeField]
    private float moveSpeed = 5f; // �ړ����x

    [Header("�W�����v�ݒ�")]
    [SerializeField]
    private float jumpForce = 10f; // �W�����v��
    [SerializeField]
    private float _dashForce = 10f;//�_�b�V���̗�
    [SerializeField]
    private Transform groundCheck; // �ڒn����̈ʒu
    [SerializeField]
    private float groundCheckRadius = 0.2f; // �ڒn����̉~�̔��a
    [SerializeField]
    private LayerMask groundLayer; // �u�n�ʁv�Ƃ݂Ȃ����C���[

    // �v���C�x�[�g�ϐ�
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true;

    // �Q�[���J�n���Ɉ�x�����Ă΂��
    private void Awake()
    {
        // �K�v�ȃR���|�[�l���g���擾���ĕϐ��Ɋi�[
        rb = GetComponent<Rigidbody2D>();
    }

    // �t���[�����ƂɌĂ΂��
    private void Update()
    {
        // ���E�̃L�[���͂��擾 (-1:��, 0:���͂Ȃ�, 1:�E)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // �W�����v�L�[�������ꂽ�u�ԁA���n�ʂɂ���ꍇ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Y�����̑��x�����Z�b�g���Ă���͂������邱�ƂŁA���肵���W�����v�ɂȂ�
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //�_�b�V������
        if (Input.GetKeyDown(KeyCode.LeftShift))

        {
            rb.velocity = new Vector2(rb.velocity.x,1);
            rb.AddForce(Vector2.right* _dashForce, ForceMode2D.Impulse);
        }

    }

    // �Œ�t���[�����[�g�ŌĂ΂��i�������Z�p�j
    private void FixedUpdate()
    {
        // --- �ڒn���� ---
        // groundCheck�̈ʒu�ɁA�w�肵�����a�̉~�����A���̉~��groundLayer�ɐG��Ă��邩����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- ���E�ړ� ---
        // X�����̑��x���X�V�iY�����̑��x�͂��̂܂܈ێ�����j
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // --- �L�����N�^�[�̌����𔽓] ---
        // ���͕����ƌ��݂̌������Ⴄ�ꍇ��Flip()���Ăяo��
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    // �L�����N�^�[�̌����𔽓]������
    private void Flip()
    {
        // ���݂̌����𔽓]
        isFacingRight = !isFacingRight;

        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Unity�G�f�B�^��Scene�r���[�ɁA�f�o�b�O�p�̉~��`�悷��
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}