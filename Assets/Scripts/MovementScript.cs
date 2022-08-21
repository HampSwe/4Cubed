using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    //reference variables
    public Rigidbody rb;
    public Transform tf;
    public Vector3 startPos;
    public Camera cam;
    Vector3 tmpPos;

    public float offSet;
    public float blinkDuration;
    public float blinkRepeat;

    public bool rotationSystem;
    public float rotation;
    public float rotationSpeed;


    // Use this for initialization
    void Start()
    {

        tf.position = startPos;

    }



    // Update is called once per frame
    void Update()
    {



        //tf.position = Input.mousePosition;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        Physics.Raycast(ray, out hit);



        tmpPos.Set(hit.point.x, hit.point.y, 0);

        tf.position = tmpPos;


        if (rotationSystem)
        {
            if (Input.GetMouseButton(0))
            {
                rotation += rotationSpeed * Time.deltaTime;
            }
            else if (Input.GetMouseButton(1))
            {
                rotation -= rotationSpeed * Time.deltaTime;
            }
        }




    }
}
