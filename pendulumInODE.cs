using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pendulumInODE : MonoBehaviour
{

    Vector2 f(float t, Vector2 x) {
        Vector2 vec = new Vector2(0f, 0f);
        float x1 = x[0];
        float x2 = x[1];
        vec[0] = x2;
        vec[1] = -9.81f / l * Mathf.Sin(x1);
        return vec;
    }

    Vector2 rk4(Vector2 x, float t, float h)
    {
        Vector2 k1 = f(t, x);
        Vector2 xk2 = new Vector2(x[0] + 0.5f * h * k1[0],
                      x[1] + 0.5f * h * k1[1]);
        Vector2 k2 = f(t + 0.5f * h, xk2);

        Vector2 xk3 = new Vector2(x[0] + 0.5f * h * k2[0],
                      x[1] + 0.5f * h * k2[1]);
        Vector2 k3 = f(t + 0.5f * h, xk3);

        Vector2 xk4 = new Vector2(x[0] + h * k3[0],
                      x[1] + h * k3[1]);
        Vector2 k4 = f(t + h, xk4);

        Vector2 xn = new Vector2(
            x[0] + (h / 6.0f) * (k1[0] + 2.0f * k2[0] + 2.0f * k3[0] + k4[0]),
            x[1] + (h / 6.0f) * (k1[1] + 2.0f * k2[1] + 2.0f * k3[1] + k4[1])
        );
        return xn;
    }
    private float h = 1e-3f;
    private float t = 0f;
    private float accumulator = 0f;
    Vector2 theta = new Vector2(0f, 5f);
    public Slider spring_Slider;
    public GameObject spring;
    public GameObject sphere;
    private float springScale;
    private float radius;
    private float l;

    void Start()
    {
        l = 1f;
    }

    void Update()
    {
        springScale = spring_Slider.value;
        l = springScale / 4.0f;

        if (l < 0.1f) l = 0.1f;

        accumulator += Time.deltaTime;
        while (accumulator >= h)
        {
            theta = rk4(theta, t, h);
            t += h;
            accumulator -= h;
        }
        print(Mathf.Sqrt(9.81f/l));
        
        spring.transform.localScale = new Vector3(0.1f, springScale, 0.1f);
        spring.transform.localPosition = new Vector3(springScale, 0, 0);
        sphere.transform.localPosition = new Vector3(springScale * 2.0f, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, theta[0] * 180 / Mathf.PI - 90f);
    }
}