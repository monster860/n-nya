using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public CharController holder;
    public float holdXOffset = 0.5f;
    public float holdYOffset = 1.0f;
    Interactible interactible;
    Rigidbody2D rigidbody2d;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        interactible = GetComponent<Interactible>();
        coll = GetComponent<Collider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        if(interactible) {
            interactible.structs.Add(new InteractStruct(this.Interact, "Grab", KeyCode.E));
        }
    }

    public void Pickup(CharController controller) {
        if(controller.heldObject) controller.heldObject.Drop();
        holder = controller;
        if(interactible) interactible.enabled = false;
        coll.enabled = false;
        rigidbody2d.simulated = false;
        controller.heldObject = this;
        gameObject.transform.SetParent(holder.transform, true);
        gameObject.transform.localPosition = new Vector3(holdXOffset * (controller.faceRight ? 1 : -1), holdYOffset, 0);
        gameObject.transform.localRotation = Quaternion.identity;
    }
    public void Drop() {
        if(interactible) interactible.enabled = true;
        coll.enabled = true;
        rigidbody2d.simulated = true;
        holder.heldObject = null;
        gameObject.transform.SetParent(null, true);
        rigidbody2d.velocity = holder.GetComponent<Rigidbody2D>().velocity;
        rigidbody2d.angularVelocity = holder.GetComponent<Rigidbody2D>().angularVelocity;
        holder = null;
    }
    void Interact()
    {
        Debug.Log("INteract");
        if(PlayerController.instance.charController.heldObject == this) {
            Drop();
        } else {
            Pickup(PlayerController.instance.charController);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(holder) {
            gameObject.transform.localPosition = new Vector3(holdXOffset * (holder.faceRight ? 1 : -1), holdYOffset, 0);
            gameObject.transform.localRotation = Quaternion.identity;
        }
    }
}
