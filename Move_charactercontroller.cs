using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_charactercontroller : MonoBehaviour
{

    private CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() //charactercontroller, en oo ennen ees käyttänyt, onpa kiva.
    {

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (input != Vector3.zero)
        {
            characterController.Move(input * 2*Time.deltaTime);
        }
    }
}
