using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai_enemy : MonoBehaviour
{
    public enum Behaviour { patrol, attack}
    public Behaviour ourBehaviour = Behaviour.patrol;
    public List<Transform> PatrolPath;
    private int currentPatrolPathIndex = 0;
    public NavMeshAgent ourAgent;
    public float distanceToTargetPath = 100;
    public float distanceToPlayer = 100;
    private float attackDistance = 3f;
    private float Health = 2f;
    public Animator ourAnimator;
    public bool ourUmbrellaIsOpen = false;
    public ai_umbrella ourUmbrellaAI;
    private float hurtbuffer = 0;
    private float attackbuffer = 0;
    public float umbrellaupdate = 0;

    private MaterialPropertyBlock materialBlock;
    public SkinnedMeshRenderer ourSkinnedMesh;
    public float inkyness = 0;


    private void Awake()
    {
        float isUmbrellaOpen_Randomizer = Random.value;
        if (isUmbrellaOpen_Randomizer < 0.5) ourUmbrellaIsOpen = true;
    }

    public void TryAttack()
    {
        if (Vector3.Distance(transform.position, Manager_Game.Instance.ref_playermovement.transform.position) < attackDistance)
        {
            Manager_Game.Instance.HurtPlayer();
        }
    }


    void Update()
    {

        if (Manager_Game.Instance.currentGameState != Manager_Game.State.Action) { return; }
        distanceToPlayer = Vector3.Distance(transform.position, Manager_Game.Instance.ref_playermovement.transform.position);


        if (ourBehaviour== Behaviour.patrol)
        {
            if (umbrellaupdate != 10)
            {
                umbrellaupdate = Mathf.Clamp(umbrellaupdate += 1 * Time.deltaTime, 0, 10);
            }
            else
            {
                umbrellaupdate = 0;
                ourUmbrellaIsOpen = !ourUmbrellaIsOpen;
            }
            if (distanceToPlayer < 4 && attackbuffer == 0)
            {
                ourAgent.SetDestination(Manager_Game.Instance.ref_playermovement.transform.position);
                ourUmbrellaIsOpen = false;
                ourBehaviour = Behaviour.attack;
                attackbuffer = 3f;
                ourAnimator.SetTrigger("Attack");
                return;
            }
            distanceToTargetPath = Vector3.Distance(transform.position, PatrolPath[currentPatrolPathIndex].transform.position);
            if (distanceToTargetPath < 1f) currentPatrolPathIndex = UpdatePatrolPath();
            else ourAgent.SetDestination(PatrolPath[currentPatrolPathIndex].transform.position);
        }
        else if (ourBehaviour == Behaviour.attack)
        {
            ourAgent.SetDestination(Manager_Game.Instance.ref_playermovement.transform.position);
            if (attackbuffer != 0) attackbuffer = Mathf.Clamp(attackbuffer -= 1 * Time.deltaTime, 0, 3);
            else ourBehaviour = Behaviour.patrol;
        }
        ourAnimator.SetFloat("velocity", ourAgent.velocity.magnitude);
        if (ourUmbrellaAI) ourUmbrellaAI.isOpen = ourUmbrellaIsOpen;


        if (inkyness > 0) inkyness = Mathf.Clamp(inkyness -= 0.1f * Time.deltaTime, 0, 1);
        UpdateMaterialBlock();
        if (hurtbuffer > 0) hurtbuffer = Mathf.Clamp(hurtbuffer -= 1 * Time.deltaTime, 0, 1);
    }

    private int UpdatePatrolPath()
    {
        int newIndex = currentPatrolPathIndex += 1;
        if (newIndex >= PatrolPath.Count) newIndex = 0;
        return newIndex;
    }


    public void Receive(Receivables.receivetype whatReceive)
    {
        if (ourBehaviour == Behaviour.attack) return;
        switch(whatReceive)
        {
            case Receivables.receivetype.attack:
                ourAnimator.SetTrigger("Hurt");
                break;
            case Receivables.receivetype.fire:
                ourAnimator.SetTrigger("Hurt");
                Health--;
                break;
            case Receivables.receivetype.liquid:
                if (ourUmbrellaIsOpen == false)
                {
                    if (hurtbuffer == 0)
                    {
                        ourAnimator.SetTrigger("Hurt");
                        Health -= 1f;
                        inkyness = 1;
                        if (Health < 0) Die();
                        hurtbuffer = 1;
                    }

                }
                else
                {
                    //ourAnimator.SetTrigger("Defend");
                    ourUmbrellaAI.inkyness += 0.25f;
                }
                break;
            case Receivables.receivetype.water:
                break;
            default: break;
        }
    }

    public void Die()
    {
        Manager_Game.Instance.ref_SpawnablesLibrary.Spawn_FX_OnEnemyDeath(transform.position);
        gameObject.SetActive(false);   
    }

    private void UpdateMaterialBlock()
    {
        if (ourSkinnedMesh == null) ourSkinnedMesh = GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;

        if (materialBlock == null) materialBlock = new MaterialPropertyBlock();

        //set the color property
        materialBlock.SetFloat("_Inkyness", inkyness);
        //reassign the material to the renderer
        ourSkinnedMesh.SetPropertyBlock(materialBlock);
    }

}
