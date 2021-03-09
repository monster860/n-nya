using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModifyDoor : StateMachineBehaviour
{
    public string doorName;
    Door door;
    public bool openDoor = false;
    public bool closeDoor = false;
    public bool lockDoor = false;
    public bool unlockDoor = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        door = GameObject.Find(doorName).GetComponent<Door>();
        if(openDoor) door.Open();
        if(closeDoor) door.Close();
        if(lockDoor) door.locked = true;
        if(unlockDoor) door.locked = false;
    }
}
