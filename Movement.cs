using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager'ý ekleyin

public enum speeds { Slow = 0, Normal = 1, Faster = 3, Fastest = 4 }
public class Movement : MonoBehaviour
{
    public speeds CurrentSpeed;

    //                        0     1       2       3      4
    float[] SpeedValues = { 8f, 10.4f, 12.96f, 15.6f, 19.27f };
    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    Rigidbody2D rb;
    public Transform Sprite;
    public GameObject player; // Yok edilecek nesne

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpýþma algýlama
        if (collision.gameObject.CompareTag("destroyer"))
        {
            // Oyunu yeniden baþlatma
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Update()
    {
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
        if (Onground())
        {
            //jump
            if (Input.GetMouseButton(0))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 26.6581f, ForceMode2D.Impulse);
            }
            //print("player Jump");
        }
        else
        {
            Sprite.transform.Rotate(Vector3.back * 60);
        }

    }

    bool Onground()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }

}
