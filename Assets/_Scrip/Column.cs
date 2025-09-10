using Unity.Jobs;
using UnityEngine;
public enum Box
{
    box1,
    box2,
    box3,
}
public class Column : MonoBehaviour
{
    public float moveDistance = 5f;   // khoảng cách né sang bên
    public float moveSpeed = 10f;      // tốc độ né
    public float detectRange = 3f;    // khoảng cách phát hiện Player
    private bool hasMoved = false;    // chỉ né 1 lần
    public Box boxtype;
    private Transform player;
    public BoxCollider2D boxCollider;
    private Vector3 startPosition;
    private Rigidbody2D rb;
    void Start()
    {
        startPosition = transform.position;  // lưu vị trí ban đầu
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy object có Tag = Player trong scene!");
        }
        Time.timeScale = 2f;
        if(Movement.instance != null)
        {
            Movement.instance.box11 += DropBox3;
        }
    }

        void Update()
    {
       checkboxx1();
        checkbox2();
    }
    public void ResetColumn()
    {
        transform.position = startPosition;
        hasMoved = false;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic; // trở lại trạng thái cột đứng yên
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
        }
    }
    private System.Collections.IEnumerator MoveAway()
    {
        Vector3 target = transform.position + new Vector3(moveDistance, 0, 0);

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
    public void checkboxx1()
    {
        if (boxtype != Box.box1) return;
        if (!hasMoved && player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= detectRange)
            {
                hasMoved = true;
                StartCoroutine(MoveAway());
            }
        }
        if (Movement.instance.countdie == 1)
        {
            this.boxCollider.isTrigger = false;
        }
    }
    public void checkbox2()
    {
        if (boxtype != Box.box2) return;
        if (!hasMoved && player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= detectRange)
            {
                hasMoved = true;
                StartCoroutine(MoveAway());
            }
        }
        if (Movement.instance.countdie == 2)
        {
            this.boxCollider.isTrigger = false;
        }
    }
   


    void OnDisable()
    {
        if (Movement.instance != null)
        {
            Movement.instance.box11 -= DropBox3;
        }
    }

    void DropBox3()
    {
        if (boxtype != Box.box3) return;
        Debug.Log($"DropBox3 called for {gameObject.name}");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3f;
        }
        else
        {
            Debug.LogError($"No Rigidbody2D found on {gameObject.name}");
        }
    }


}
