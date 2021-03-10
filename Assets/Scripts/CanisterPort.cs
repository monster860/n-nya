using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanisterPort : MonoBehaviour
{
    Interactible interactible;
    public Canister canister;
    public float score = 4.0f;
    public string goodCanisterProp = "GoodCanister";
    public string badCanisterProp = "BadCanister";
    public string scoreProp = "CanisterScore";
    // Start is called before the first frame update
    void Start()
    {
        interactible = GetComponent<Interactible>();
        
        if(interactible) {
            interactible.structs.Add(new InteractStruct(this.Interact, "Put", KeyCode.E));
        }
    }
    void Interact()
    {
        if(canister) return;
        Pickupable held = PlayerController.instance.charController.heldObject;
        if(held) {
            held.Drop();
            canister = held.GetComponent<Canister>();
            canister.transform.SetParent(transform);
            canister.transform.rotation = Quaternion.identity;
            canister.transform.localPosition = new Vector3(0, 0.25f, 0);
            canister.GetComponent<Rigidbody2D>().simulated = false;
            canister.GetComponent<Collider2D>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        interactible.enabled = PlayerController.instance.charController.heldObject && !canister;
        if(canister) {
            if(canister.transform.parent != transform) {
                canister = null;
            } else {
                float to_drain = Mathf.Min(canister.fullness, Time.deltaTime * 0.2f);
                canister.fullness -= to_drain;
                score -= to_drain;
            }
        }
        if(goodCanisterProp != "") LevelController.instance.animator.SetInteger(goodCanisterProp, canister ? 1 : 0);
        if(scoreProp != "") LevelController.instance.animator.SetFloat(scoreProp, score);
    }
}
