using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D player;
    [SerializeField]
    Transform cameraPos;
    [SerializeField]
    float playerSpeed = 1f;
    [SerializeField]
    float jumpHeight = 1f;
    [SerializeField]
    float dashSpeed = 1f;
    [SerializeField]
    float dashRecharge = 1f;
    float timer = 0f;
    bool touchingGround;
    Vector3 respawnPos;
    AudioSource jumpsound;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Rigidbody2D>();

        respawnPos = player.position;

        ResetPlayer();
        jumpsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Reset player - William strandberg
        if (Input.GetKeyDown(KeyCode.R) || transform.position.y < -11)
        {
            ResetPlayer();
        }

        cameraPos.position = new Vector3(player.position.x, 0, -10);
    }

    void FixedUpdate()
    {
        //Movement using addforce.impulse - William Strandberg
        if (Input.GetKey(KeyCode.D) && touchingGround == true) //Move right
        {
            player.AddForce(new Vector2(playerSpeed * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.A) && touchingGround == true) //Move left
        {
            player.AddForce(new Vector2(playerSpeed * -1 * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
        }
        
        if (Input.GetKey(KeyCode.W) && touchingGround == true) //Jump
        {
            player.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            jumpsound.Play();
        }
        
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.D) && timer > dashRecharge) //Dash Right
        {
            player.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
            timer = 0f;
        }
        
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.A) && timer > dashRecharge) //Dash left
        {
            player.AddForce(new Vector2(dashSpeed * -1, 0), ForceMode2D.Impulse);
            timer = 0f;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Enemy")) //Ground collision check - William strandberg
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }
    }

     void OnCollisionExit2D(Collision2D collision)
    {
        touchingGround = false;
    }

    public void ResetPlayer()
    {
        transform.position = respawnPos;
    }
}
