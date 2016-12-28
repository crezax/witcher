using UnityEngine;

public class MovementController : BaseBehaviour {

  private Vector3 target;
  private float distance;
  private GameObject followed;
  private Vector3 directionVector;
  private MovementTypeEnum movementType;
  private bool isMoving;
  private Rigidbody rbody;
  private IHasRotationSpeed rotationSpeed;
  private Speed speed;
  private bool canMove;
  private Animator animator;

  public Vector3 Target {
    get {
      return target;
    }
  }
  public GameObject Followed {
    get {
      return followed;
    }
  }
  public bool IsMoving {
    get {
      return isMoving;
    }
    private set {
      isMoving = value;
      if (animator == null) {
        return;
      }
      if (speed.IsRunning) {
        animator.SetBool("IsRunning", isMoving);
        animator.SetBool("IsWalking", false);
      } else {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsWalking", isMoving);
      }
    }
  }

  private MovementTypeEnum MovementType {
    get {
      return movementType;
    }
    set {
      movementType = value;
      if (movementType != MovementTypeEnum.DIRECTION) {
        directionVector = Vector3.zero;
      }
      if (movementType != MovementTypeEnum.FOLLOW) {
        followed = null;
      }
      if (movementType != MovementTypeEnum.TARGET) {
        target = Vector3.zero;
      }
    }
  }

  // To prevent character from turning around in place we use movementAccuracy
  public const float movementAccuracy = .1f;

  public void MoveTo(
    Vector3 position,
    float distance = movementAccuracy
  ) {
    MovementType = MovementTypeEnum.TARGET;
    target = position;
    IsMoving = CanMove;
    this.distance = distance;
  }

  public void Follow(
    GameObject go,
    float distance = movementAccuracy
  ) {
    MovementType = MovementTypeEnum.FOLLOW;
    followed = go;
    IsMoving = CanMove;
    this.distance = distance;
  }

  public bool CanMove {
    get {
      return canMove;
    }
    set {
      canMove = value;
    }
  }

  public void MoveInDirection(Vector3 dir) {
    MovementType = MovementTypeEnum.DIRECTION;
    directionVector = dir;
    distance = 0;
    IsMoving = CanMove;
  }

  public void MoveInstantly(Vector3 position, bool forwardOverNetwork = true) {
    rbody.transform.position = position;
  }

  public void RotateInstantly(Quaternion rotation, bool forwardOverNetwork = true) {
    transform.rotation = rotation;
  }

  public void Stop(bool forwardOverNetwork = true) {
    MovementType = MovementTypeEnum.STOP;
    IsMoving = false;
  }

  protected override void OnAwake() {
    base.OnAwake();

    rbody = GetComponent<Rigidbody>();
    rotationSpeed = GetComponent<IHasRotationSpeed>();
    speed = GetComponent<Speed>();
    canMove = true;
    animator = GetComponent<Animator>();
    MovementType = MovementTypeEnum.STOP;
  }

  protected override void OnPhysicsUpdate() {
    // In case the object we follow is destroyed
    if (MovementType == MovementTypeEnum.FOLLOW && followed == null) {
      Stop();
    }

    Vector3 moveVector = Vector3.zero;
    switch (MovementType) {
      case MovementTypeEnum.TARGET:
        moveVector = target - transform.position;
        break;
      case MovementTypeEnum.FOLLOW:
        moveVector = followed.transform.position - transform.position;
        break;
      case MovementTypeEnum.DIRECTION:
        moveVector = directionVector.normalized;
        break;
      case MovementTypeEnum.STOP:
        IsMoving = false;
        return;
    }

    //Rotate in direction you should move
    if (canMove) {
      Quaternion targetRotation = Quaternion.LookRotation(moveVector);

      if (rotationSpeed != null) {
        transform.rotation = Quaternion.RotateTowards(
          transform.rotation,
          targetRotation,
          Time.deltaTime * rotationSpeed.RotationSpeed
        );
      } else {
        transform.rotation = targetRotation;
      }
    }

    // Move towards target, but keep your distance
    if (moveVector.magnitude > distance && canMove) {
      Vector3 targetPosition = transform.position +
        transform.forward *
        speed.CurrentSpeed *
        Time.deltaTime;

      if (rbody != null) {
        rbody.MovePosition(targetPosition);
      } else {
        transform.position = targetPosition;
      }

      IsMoving = true;
    }

    if (!canMove) {
      IsMoving = false;
    }

    if (moveVector.magnitude < distance) {
      IsMoving = false;

      // We reached the target spot, invoke stop, so the character doesn't run 
      // back to the stop after getting moved by external forced
      if (MovementType == MovementTypeEnum.TARGET) {
        Stop();
      }
    }
  }

  private enum MovementTypeEnum {
    TARGET, DIRECTION, FOLLOW, STOP
  }
}