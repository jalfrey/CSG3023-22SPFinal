/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Jason Alfrey
 * Last Edited: April 28, 2022
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10; //speed of paddle
    public Vector3 position;
    public GameObject paddleGo;
    private Vector3 update = new Vector3(10, 0, 0);

    // Update is called once per frame
    void Update()
    {
        position = paddleGo.transform.position;
        float axis = Input.GetAxis("Horizontal");
        if(Input.GetKey("a"))
        {
            position -= update * -axis * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            position += update * axis * Time.deltaTime;
        }

        paddleGo.transform.position = position;
    }//end Update()
}
