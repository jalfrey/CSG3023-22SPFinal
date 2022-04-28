/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Jason Alfrey
 * Last Edited: April 28, 2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private bool isInPlay = false;
    private Rigidbody rb;
    public Transform paddle;
    public int score;
    [Header("General Settings")]
    public Text ballTxt;
    public Text scoreTxt;

    [Header("Ball Settings")]
    public int speed = 10;
    public AudioSource audioSource;
    private Vector3 initialForce = new Vector3(0, 10, 0);
    public static int numBalls;
    
    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }//end Awake()


    // Start is called before the first frame update
    void Start()
    {
        SetStartingPos(); //set the starting position
    }//end Start()


    // Update is called once per frame
    void Update()
    {
        ballTxt.text = "Balls: " + numBalls;
        scoreTxt.text = "Score " + score;
        Debug.Log(isInPlay);

        if (!isInPlay)
        {
            Vector3 ballPos = paddle.localPosition;
            rb.transform.localPosition = ballPos;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Move();
                isInPlay = true;
            }
        }

    }//end Update()

    private void LateUpdate()
    {
        if(isInPlay) //ball is in play
        {
            rb.velocity = speed * rb.velocity.normalized;
        }

    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddle
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    void Move()
    {
        rb.AddForce(initialForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            score += 100;
            Destroy(collision.gameObject);
            audioSource.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OutBounds"))
        {
            numBalls--;
            if (numBalls > 0)
            {
                Invoke("WaitForSeconds", 2f);
                SetStartingPos();
            }
        }

    }

}
