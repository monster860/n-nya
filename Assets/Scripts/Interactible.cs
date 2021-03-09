using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Interact();

public struct InteractStruct {
    public InteractStruct(Interact interact, string interactText, KeyCode key) {
        this.interact = interact;
        this.interactText = interactText;
        this.key = key;
    }
    public Interact interact;
    public string interactText;
    public KeyCode key;
}

public class Interactible : MonoBehaviour
{
    public List<InteractStruct> structs = new List<InteractStruct>();
    public float range = 1.0f;
    public float playerDistance;
    bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(PlayerController.instance.transform.position, transform.position);
        bool newInRange = playerDistance < range;
        if(newInRange && !inRange) {
            PlayerController.instance.interactibles.Add(this);
        } else if(!newInRange && inRange) {
            PlayerController.instance.interactibles.Remove(this);
        }
        inRange = newInRange;
    }
}
