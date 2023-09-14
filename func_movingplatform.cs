using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class func_movingplatform : MonoBehaviour
{

    public enum Action {Move, Wait };
    public Action currentAction = Action.Wait;

    private bool MoveBackwards = false;

    public List<Transform> targetPositions;
    public List<float> waitAtPosition;
    public Transform currentTargetPosition;
    public float speed = 0.1f;

    public trigger_addforcetoplayer ourExternalForce;

    private int currentIndex = 0;


    private float waitBeforeMove = 0;

    private void Start()
    {
        currentTargetPosition = targetPositions[currentIndex];
    }
    private void Update()
    {
        if (currentAction == Action.Wait)
        {
            if (waitBeforeMove > 0)
            {
                waitBeforeMove = Mathf.Clamp(waitBeforeMove -= 1 * Time.deltaTime, 0, 100);
            }
            else
            {
                if (currentIndex == targetPositions.Count - 1) MoveBackwards = true;
                else if (currentIndex == 0) MoveBackwards = false;

                if (MoveBackwards) currentIndex--;
                else currentIndex++;

                currentAction = Action.Move;
            }
        }
        else if (currentAction == Action.Move)
        {
            if (Vector3.Distance(transform.position, targetPositions[currentIndex].position) > 0.1f)
            {
                Vector3 moveDirection = (targetPositions[currentIndex].position - transform.position).normalized;

                ourExternalForce.MoveForce = (moveDirection * (speed * Time.deltaTime));

                transform.Translate(moveDirection* (speed*Time.deltaTime), Space.World);
            }
            else 
            {
                ourExternalForce.MoveForce = Vector3.zero;
                waitBeforeMove = waitAtPosition[currentIndex];
                currentAction = Action.Wait; 
            }
        }
    }
}
