using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class patrol : MonoBehaviour {

    [SerializeField]
    List<GameObject> positions;

    [SerializeField]
    int currentTarget = 0;

    [SerializeField]
    bool movingToDest = true;

    [SerializeField]
    bool pausePatrol = false;

    [SerializeField]
    bool chasing = false;

    [SerializeField]
    bool keepLooking = false;

    NavMeshAgent agent;

    Animator anim;

    BoxCollider coll;

    gameManager.status myStatus;

	// Use this for initialization
	void Start () {
        agent = GetComponentInParent<NavMeshAgent>();
        anim = GetComponentInParent<Animator>();
        coll = transform.parent.gameObject.GetComponentInChildren<BoxCollider>();
        myStatus = gameManager.inst().getStatus(transform.parent.gameObject);
        observer.Inst().addListener(observer.events.NPCDeath, onDeath);
	}
	
	// Update is called once per frame
	void Update () {

        if (!myStatus.alive)
            return;

        Debug.DrawLine(gameObject.transform.parent.position + new Vector3(0f, 2.5f, 0f), gameManager.inst().PlayerPosition);

        if (agent.remainingDistance == 0)
        {
            anim.SetInteger("Cond", 0);
        }
        else if (chasing && agent.remainingDistance != 0)
        {
            anim.SetInteger("Cond", 3);
        }
        else
        {
            anim.SetInteger("Cond", 1);
        }

        if (keepLooking && canSeePlayer())
        {
            startChasing();
        }

        if (chasing) {
            if (!canSeePlayer())
            {
                StartCoroutine(searchDelay(5f));
            }
            else
            {
                agent.SetDestination(gameManager.inst().PlayerPosition);
            }
        }

        if (pausePatrol)
            return;

        if (agent.remainingDistance == 0 && movingToDest)
        {
            movingToDest = false;
            currentTarget = (currentTarget + 1) % positions.Count;
            StartCoroutine(moveToNext(2f));
        }
	}

    IEnumerator moveToNext(float waitFor)
    {
        yield return new WaitForSeconds(waitFor);
        agent.SetDestination(positions[currentTarget].transform.position);
        yield return new WaitForSeconds(1f);
        movingToDest = true;
    }

    public void onDeath(List<object> a)
    {
        if (a.IndexOf(transform.parent.gameObject.name) >= 0)
        {
            stopMovement();
            anim.SetInteger("Cond", 8);
            coll.enabled = false;
            myStatus.alive = false;
        }
    }

    void startChasing()
    {
        stopMovement();
        agent.speed = 5f;
        pausePatrol = true;
        chasing = true;
    }

    void stopMovement()
    {
        agent.speed = 1.5f;
        StopAllCoroutines();
        agent.Stop();
        agent.ResetPath();
        keepLooking = false;
        movingToDest = false;
        chasing = false;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && myStatus.alive && canSeePlayer())
        {
            startChasing();
        }
        else if (c.tag == "Player")
        {
            keepLooking = true;
        }
    }

    bool canSeePlayer()
    {
        RaycastHit rh;
        Physics.Linecast(gameObject.transform.parent.position + new Vector3(0f, 2.5f, 0f),gameManager.inst().PlayerPosition, out rh);
        return rh.collider.tag == "PlayerC";
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player" && myStatus.alive)
        {
            keepLooking = false;
            if (chasing)
            {
                chasing = false;
                StartCoroutine(searchDelay(5f));
            }
        }
    }

    IEnumerator searchDelay(float time)
    {
        yield return new WaitForSeconds(time);
        if (!chasing)
        {
            pausePatrol = false;
            stopMovement();
            movingToDest = true;
        }
    }
}
