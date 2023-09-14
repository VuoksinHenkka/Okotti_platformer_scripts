using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Mesh_TurnToMoveDirection : MonoBehaviour
{
    public Transform LookAt_Casual;
    public Transform LookAt_Combat;
    private Vector3 PlayerInput_Movement;
    private Vector3 LookAt_MoveToPosition;
    [SerializeField] public List<Transform> Enemies;
    private void LateUpdate()
    {
        if (Manager_Game.Instance.currentGameState != Manager_Game.State.Action) return;



        if (Enemies.Count == 0)
        {
            PlayerInput_Movement = Manager_Game.Instance.ref_MainCamera.transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            PlayerInput_Movement = new Vector3(PlayerInput_Movement.x, 0, PlayerInput_Movement.z);
            LookAt_MoveToPosition = transform.position + (PlayerInput_Movement * 2);
            LookAt_Casual.position = Vector3.Lerp(LookAt_Casual.position, LookAt_MoveToPosition, 3f * Time.deltaTime);




            transform.LookAt(LookAt_Casual);
        }
        else
        {
            UpdateEnemiesList();

            if (Enemies.Count == 0) return;
            transform.LookAt(new Vector3(Enemies[0].position.x, transform.position.y, Enemies[0].position.z));
        }
    }
    private void UpdateEnemiesList()
    {
        List<Transform> enemies_refList = new List<Transform>(Enemies);
        foreach (Transform foundtransform in enemies_refList)
        {
            if (foundtransform.gameObject.activeSelf == false)
            {
                Enemies.Remove(foundtransform);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") Enemies.Add(other.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (Enemies.Contains(other.transform)) Enemies.Remove(other.transform);
        }
    }
}
