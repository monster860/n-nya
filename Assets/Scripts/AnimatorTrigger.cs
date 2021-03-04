using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTrigger : MonoBehaviour
{
    public Animator animator = null;
    public string parameterName = "";
    public bool useEnterValue = true;
    public int enterValue = 1;
    public bool useExitValue = true;
    public int exitValue = 0;
    void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!animator) {
            animator = LevelController.instance.gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Entered");
        if(collider.gameObject.GetComponent<PlayerController>() && useEnterValue)
        {
            animator.SetInteger(parameterName, enterValue);
        }
    }
    void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.GetComponent<PlayerController>() && useExitValue)
        {
            animator.SetInteger(parameterName, exitValue);
        }
    }
}
