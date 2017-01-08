using UnityEngine;
using System.Collections;

public abstract class PlayerCharacter : MonoBehaviour {
    protected string characterName;
    protected float horAxis,verAxis;
    protected bool isMoving;
    public float curMaxSpeed;
    private Rigidbody rigidBody;
    protected Animator anim;
    //private Transform _playerModel;

    //private Vector3 _lastMovingDirection;
    // Use this for initialization
    virtual protected void Start () {
        rigidBody = GetComponent<Rigidbody>();
        //_playerModel = GameObject.FindGameObjectWithTag("PlayerModel").transform;
        //_lastMovingDirection = _playerModel.transform.forward;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	virtual protected void Update () {
        horAxis = Input.GetAxis(characterName+"Horizontal");
        verAxis = Input.GetAxis(characterName+"Vertical");
        ManagePlayerModelRotation();
        if (Mathf.Abs(horAxis) > 0 || Mathf.Abs(verAxis) > 0)
        {
            isMoving = true;
            anim.SetBool("Walking", true);
            // Global input. w key produces Vector3(0, 0, 1), a key Vector3(-1, 0, 0) etc.
            // Does not take into account in what direction the camera is facing for what
            // direction to move into
            Vector3 input = new Vector3(horAxis, 0, verAxis);

            // This happens f.x. when w and a are pressed (Vector(-1, 0, 1).magnitude = sqrt(2) > 1
            // We always want to cap the player to curMaxSpeed so we normalize input to achieve that goal.
            // -> input.magnitude < 1 f.x. if we use a controller and move slightly in z and -x direction.
            // In that case, we don't want to normalize because then the character's speed would always
            // lock to curMaxSpeed, even with small input from controllers.

            input = input.normalized;

            input *= curMaxSpeed;


            // We now convert that global direction of movement into local.
            // Global Z direction is now local z direction.
            // -> Desired and current speeds.
            Vector3 desired = transform.InverseTransformDirection(Vector3.forward);
            Vector3 current = transform.InverseTransformDirection(rigidBody.velocity);

            // Takes care of two things:
            // 1. desired will never become greater than curMaxSpeed (in the case current = 0)
            // 2. In the case current = curMaxSpeed, no further force is added.
            Vector3 clampedXZVelocity = Vector3.ClampMagnitude(desired - current, curMaxSpeed);

            // We aren't concerned about applying y force. We are just handling x and z input
            clampedXZVelocity.y = 0;

            // We finally apply the input. *64 because for some reason we
            // never reach curMaxSpeed completely. If curMaxSpeed = 2, the
            // max velocity we reach is actually about 1.9.

            rigidBody.MovePosition(rigidBody.position + input * Time.deltaTime);
            //_lastMovingDirection = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
        }
        else
        {
            anim.SetBool("Walking", false);
            isMoving = false;
        }

    }
    virtual protected void FixedUpdate()
    {
         
    }
    private void ManagePlayerModelRotation()
    {
        transform.LookAt(new Vector3(horAxis, 0, verAxis)+transform.position);
    }
    public virtual void ExecuteAction() {
    }
}
