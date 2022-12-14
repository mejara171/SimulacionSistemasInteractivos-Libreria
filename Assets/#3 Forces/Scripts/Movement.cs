using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public MyVector Velocity => velocity;

    [SerializeField] private MyVector force;
    [SerializeField] private MyVector accelaration;
    [SerializeField] private Transform target;
    [SerializeField] private MyVector velocity;
    [SerializeField] private bool CheckBounds = true;

    private MyVector position;
    private MyVector displacement;


    private int NowAcceleration = 0;
    private MyVector[] accelerations = new MyVector[4] { new MyVector(0f, -9.8f), new MyVector(9.8f, 0f), new MyVector(0f, 9.8f), new MyVector(-9.8f, 0f) };

    private void Start()
    {
        position = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }


    // Update is called once per frame
    void Update()
    {
        position.Draw(Color.green);
        displacement.Draw(position, Color.yellow);
        accelaration.Draw(position, Color.red);
        velocity.Draw(position, Color.blue);
        Debug.Log(Time.deltaTime);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity *= 0;
            accelaration = accelerations[(++NowAcceleration) % accelerations.Length];
        }

        accelaration = target.position - transform.position;
    }

    public void Move()
    {
        velocity = velocity + accelaration * (Time.fixedDeltaTime);
        //calculate displacement and new position
        position = position + velocity * (Time.fixedDeltaTime);


        //check bounds

        if (CheckBounds)
        {
            if (position.x < -5 || position.x > 5)
            {
                position.x = Mathf.Sign(position.x) * 5;
                velocity.x = -velocity.x;
            }

            if (position.y < -5 || position.y > 5)
            {
                position.y = Mathf.Sign(position.y) * 5;
                velocity.y = -velocity.y;
            }
        }




        //update unity object
        transform.position = position;
    }


}
