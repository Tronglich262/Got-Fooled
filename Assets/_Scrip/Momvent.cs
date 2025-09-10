using System.Collections;
using UnityEngine;
public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 20f;

    [Header("Jump Tuning")]
    public float fallMultiplier = 2.5f;   // rơi nhanh hơn
    public float lowJumpMultiplier = 2f;  // nhảy thấp khi thả sớm

    [Header("UI & Life")]
    public GameObject[] Mau;
    public GameObject gameovertext;

    [Header("State")]
    public Vector3 startPosition;
    public int countdie;
    public static Movement instance;

    public delegate void Box1();
    public event Box1 box11;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // giữ player khi đổi scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // lưu vị trí spawn ban đầu
    }

    void Update()
    {
        Move();
        Jump();
        BetterJump();
        CheckGameOver();
    }

    void Move()
    {
        float inputx = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(inputx * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void BetterJump()
    {
        if (rb.linearVelocity.y < 0) // đang rơi xuống
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space)) // nhả sớm khi đang nhảy lên
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("die"))
        {
            LoseLife();
        }
        if (collision.gameObject.CompareTag("box3"))
        {
            LoseLife();
        }
        if (collision.gameObject.CompareTag("box1"))
        {
           StartCoroutine(DeleyEvent());
        }

    }
    IEnumerator DeleyEvent()
    {
        yield return new WaitForSeconds(2f);
        box11?.Invoke(); 
    }

    void LoseLife()
    {
        countdie++;

        for (int i = Mau.Length - 1; i >= 0; i--)
        {
            if (Mau[i].activeSelf)
            {
                Mau[i].SetActive(false);
                break;
            }
        }

        // Reset vị trí Player về chỗ ban đầu
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;

        // Reset tất cả Column
        Column[] allColumns = FindObjectsOfType<Column>();
        foreach (var col in allColumns)
        {
            col.ResetColumn();
        }
    }


    void CheckGameOver()
    {
        if (countdie >= Mau.Length)
        {
            Time.timeScale = 0f;
            gameovertext.SetActive(true);
        }
    }

    // Hàm reset toàn bộ (dùng khi bấm Again)
    public void ResetGame()
    {
        countdie = 0;

        foreach (GameObject m in Mau)
        {
            m.SetActive(true);
        }

        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        gameovertext.SetActive(false);
        Time.timeScale = 2f;

        // Reset tất cả Column
        Column[] allColumns = FindObjectsOfType<Column>();
        foreach (var col in allColumns)
        {
            col.ResetColumn();
        }
    }


}
