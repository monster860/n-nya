using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set;}
    public CharController charController;
    public HashSet<Interactible> interactibles = new HashSet<Interactible>();
    public GameObject interactMarker;
    void Awake()
    {
        instance = this;
        charController = GetComponent<CharController>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        charController.walkDir = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) {
            charController.Jump();
        }
        Interactible closest = null;
        foreach(Interactible interactible in interactibles) {
            if(!interactible.enabled) continue;
            if(!closest || interactible.playerDistance < closest.playerDistance) {
                closest = interactible;
            }
        }
        if(closest) {
            interactMarker.SetActive(true);
            interactMarker.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y, interactMarker.transform.position.z);
            foreach(InteractStruct str in closest.structs) {
                if(Input.GetKeyDown(str.key)) {
                    str.interact();
                }
            }
        } else {
            interactMarker.SetActive(false);
        }
        if((Input.GetKeyDown(KeyCode.Q) || (!closest && Input.GetKeyDown(KeyCode.E))) && charController.heldObject) charController.heldObject.Drop();
    }
}
