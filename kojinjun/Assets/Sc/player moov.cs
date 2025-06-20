using System.Collections;
using UnityEngine;

// Rigidbody2Dコンポーネントが必須であることを示す
[RequireComponent(typeof(Rigidbody2D))]
// Collider2Dコンポーネントが必須であることを示す
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    //Headerは見出し
    [Header("移動設定")]
    [SerializeField]
    private float moveSpeed = 5f; // 移動速度

    [Header("ジャンプ設定")]
    [SerializeField]
    private float jumpForce = 10f; // ジャンプ力
    [SerializeField]
    private Transform groundCheck; // 接地判定の位置
    [SerializeField]
    private float groundCheckRadius = 0.2f; // 接地判定の円の半径
    [SerializeField]
    private LayerMask groundLayer; // 「地面」とみなすレイヤー

    // ダッシュ関連のパラメータ
    [Header("ダッシュ設定")]
    [SerializeField] private float dashSpeed = 20f;         // ダッシュの速度
    [SerializeField] private float dashCooldown = 2f;       // ダッシュのクールダウンタイム
    [SerializeField] private float dashcol = 1f; //ダッシュコルーチン
    
    // プライベート変数
    private Rigidbody2D rb;
    private float horizontalInput;
    private float _time = 2f;             //ダッシュのタイマー
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool isDashing ;     //ダッシュ中
    //private bool canDash = true;        // ダッシュ可能かどうかのフラグ
   




    private void Awake()
    {
        // 必要なコンポーネントを取得して変数に格納
        rb = GetComponent<Rigidbody2D>();
    }

    // フレームごとに呼ばれる
    private void Update()
    {
        // 左右のキー入力を取得 (-1:左, 0:入力なし, 1:右)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // ジャンプキーが押された瞬間、かつ地面にいる場合

            if (Input.GetButtonDown("Jump")&&isGrounded)
            {

                // Y方向の速度をリセットしてから力を加えることで、安定したジャンプになる
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                
            }

        // ダッシュの入力受付
        if (Input.GetKeyDown(KeyCode.LeftShift) && _time > dashCooldown)
        {
            StartCoroutine(Dash());
            _time = 0;

        }


    }


    private void FixedUpdate()
    {
        // --- 接地判定 ---
        // groundCheckの位置に、指定した半径の円を作り、その円がgroundLayerに触れているか判定
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- 左右移動 ---
        // X方向の速度を更新（Y方向の速度はそのまま維持する）
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }

        _time += Time.deltaTime;




        // --- キャラクターの向きを反転 ---
        // 入力方向と現在の向きが違う場合にFlip()を呼び出す
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }



    }

    private IEnumerator Dash()
    {
        isDashing = true;

        if (isFacingRight)
        {
            rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(dashcol);
        isDashing = false;
    }
    // キャラクターの向きを反転させる
    private void Flip()
    {
       
        isFacingRight = !isFacingRight;

        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // UnityエディタのSceneビューに、デバッグ用の円を描画する
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}