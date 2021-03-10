using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoiceLine : StateMachineBehaviour
{
    public AudioClip clip;
    public AudioSource source;
    float amountPastEnd = 0;
    float gracePeriod = 0.3f; // fix weird speech-skipping bugs
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        amountPastEnd = 0;
        if(!source) {
            source = GameObject.FindObjectOfType<CatboyController>().gameObject.GetComponent<AudioSource>();
        }
        source.clip = clip;
        source.Play();
        animator.SetFloat("VoiceTimeLeft", Mathf.Max(gracePeriod, clip.length));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gracePeriod = Mathf.Max(0, gracePeriod - Time.deltaTime);
        //if(Input.GetKeyDown(KeyCode.F)) source.Stop();
        if((source.clip == clip && source.time > 0) || gracePeriod > 0.001f) {
            animator.SetFloat("VoiceTimeLeft", clip.length - source.time);
        } else {
            animator.SetFloat("VoiceTimeLeft", -(amountPastEnd += Time.deltaTime));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(source.clip == clip) {
            source.Stop();
            animator.SetFloat("VoiceTimeLeft", clip.length); // Ensure future thingies dont insta-exit
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
