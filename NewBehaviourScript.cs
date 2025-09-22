using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private Vector3 initialPosition; 
    private Vector3 endPosition; 
    private float displacement;
    public float deceleration = 0.75f;
    private float initialSpeed;
    private float time;
    public Slider sliderForce;
    private bool movement;

    void Start()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        if(movement)
        {
            move();
        }
        else{
            stop();
        }
    }

    public void move()
    {
        movement = true;
        initialSpeed = sliderForce.value;
        displacement = initialSpeed*initialSpeed/(2*deceleration);
        endPosition = initialPosition + new Vector3(displacement, 0f, 0f);
        time +=Time.deltaTime;
        float speed = initialSpeed - (deceleration * time);
        if (speed > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        }
    }
    public void stop()
    {
        transform.position = initialPosition;
        movement = false;
        time = 0;
    }

}
