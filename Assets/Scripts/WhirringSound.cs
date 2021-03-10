using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WhirringSound : MonoBehaviour
{
    AudioSource audioSource;
    public float acceleration = 0.2f;
    public bool onTarget;
    public float state = 0;
    public float oscPeriod = 3.0f;
    public float oscState = 0.0f;
    public float oscAmplitude = 0.05f;
    public GameObject spaceThingy;
    public Vector3 spaceThingyInitialPos;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(spaceThingy) {
            spaceThingyInitialPos = spaceThingy.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        oscState += Time.deltaTime * 2 * Mathf.PI / oscPeriod;
        if(oscState > Mathf.PI*2) oscState -= Mathf.PI*2;
        float oscMult = Mathf.Sin(oscState) * oscAmplitude + 1;
        if(onTarget && state < 1) {
            state = Mathf.Min(state + Time.deltaTime * acceleration, 1);
        } else if(!onTarget && state > 0) {
            state = Mathf.Max(state - Time.deltaTime * acceleration, 0);
        }
        if(state != audioSource.volume) {
            if(state == 0) audioSource.Stop();
            else if(audioSource.volume == 0 && state > 0) audioSource.Play();
            audioSource.volume = state;
        }
        audioSource.pitch = state * oscMult;
        if(spaceThingy) { // shitcode because uh yeah
            spaceThingy.transform.position += Vector3.right * state * Time.deltaTime * 32;
            if(spaceThingy.transform.position.x > spaceThingyInitialPos.x + 32.0f) spaceThingy.transform.position += Vector3.left * 32.0f;
        }
    }
}
