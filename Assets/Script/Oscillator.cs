using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingposition;
    [SerializeField] Vector3 movementVector;
    float movementFector;
    [SerializeField] float period = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        startingposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFector = (rawSinWave + 1.0f) / 2.0f;

        Vector3 offset = movementVector * movementFector;
        transform.position = startingposition + offset;
    }
}
