using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Debug.Log(startPosition);
    }

    // Update is called once per frame
    void Update()
    {

        if(period == 0f)
        {
            return;
        }

        float cycles = Time.time / period; //As time goes on, the number of cycles increases
        
        const float tau = Mathf.PI * 2; //Constant value of 6.283
        float rawSinWave = Mathf.Sin(tau * cycles); //Going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; //Recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
