using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerController : MonoBehaviour {

    /* bools
     * walking
     * attacking
     * dead
     */

    Animator anim;
    CharacterController controller;
    SphereCollider attackCollider;

    gameManager.status myStatus;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float runSpeed;

    [SerializeField]
    float strafeSpeed;

    [SerializeField]
    float runAnimSpeed;

    [SerializeField]
    float animSpeedScale;

    [SerializeField]
    float cameraSpeed;

    [SerializeField]
    float parallelLerpSpeed;

    [SerializeField]
    float parallelRayLength;

    GameObject cam;

    float boundRotateX = 0f;

    [SerializeField]
    float boundRotateXMax;
    [SerializeField]
    float boundRotateXMin;

    [SerializeField]
    float actionRange;

    bool attacking = false;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        cam = GameObject.Find("Camera");
        controller = GetComponent<CharacterController>();
        attackCollider = GameObject.Find("RatAttackCollider").GetComponent<SphereCollider>();
        attackCollider.enabled = false;

        myStatus = gameManager.inst().PlayerStatus;
        observer.Inst().addListener(observer.events.playerDeath, onDeath);
        observer.Inst().addListener(observer.events.sensitivityChange, onSensitivityChange);
	}
	
    void onSensitivityChange(List<object> o)
    {
        cameraSpeed = (float)o[0];
    }

	// Update is called once per frame
	void Update () {
        if (!myStatus.alive)
            return;

        if (Input.GetKeyDown(KeyCode.BackQuote))
            observer.Inst().invokeAction(observer.events.objectiveComplete, null);

        if (Input.GetKeyDown(KeyCode.Escape))
            gameManager.inst().pauseMenu();

        //Eating
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit rh;
            if (Physics.Raycast(transform.position, -transform.forward, out rh, actionRange) && rh.collider.tag.Contains("food"))
            {
                List<object> o = new List<object>();
                o.Add(rh.transform.gameObject);
                o.Add(rh.collider.tag.Split(':')[1]);
                observer.Inst().invokeAction(observer.events.eat, o);
            }
        }

        // attacking
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            attacking = true;
            StartCoroutine(attack());
        }

        // animation
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            anim.SetBool("walking", true);
        } else {
            anim.SetBool("walking", false);
        }

        // MOVEMENT
        Vector3 movement = Vector3.zero;
        movement += (transform.forward * Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.A))
            movement -= transform.right * strafeSpeed;

        if (Input.GetKey(KeyCode.D))
            movement += transform.right * strafeSpeed;

        // sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= -runSpeed;
        }
        else
        {
            movement *= -movementSpeed;
        }

        // need the if for idle animation
        if (movement.magnitude != 0f)
        {
            anim.speed = movement.magnitude / movementSpeed * animSpeedScale;
        }
        else
        {
            anim.speed = 1f;
        }

        controller.SimpleMove(movement);

        // ROTATION

        // rat
        transform.Rotate(0f, cameraSpeed * Input.GetAxis("Mouse X") * Time.deltaTime, 0f, Space.Self);
        // camera rotation
        float rotationAmount = Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSpeed;
        if (boundRotateX + rotationAmount < boundRotateXMax && boundRotateX + rotationAmount > boundRotateXMin)
        {
            cam.transform.Rotate(rotationAmount, 0f, 0f, Space.Self);
            boundRotateX += rotationAmount;
        }


        // set gameobject parallel to ground
        RaycastHit rch;
        if(Physics.Raycast(transform.position, -1 * transform.up, out rch, parallelRayLength))
            transform.forward = Vector3.Lerp(transform.forward, Vector3.Cross(Vector3.Cross(rch.normal, transform.forward), rch.normal), parallelLerpSpeed);
	}

    public void onDeath(List<object> l)
    {
        myStatus.alive = false;
        anim.SetBool("dead", true);
    }

    IEnumerator attack()
    {
        anim.SetBool("attacking", true);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(.45f);
        anim.SetBool("attacking", false);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(.45f);
        attacking = false;
    }
}
