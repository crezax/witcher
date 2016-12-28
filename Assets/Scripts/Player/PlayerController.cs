using UnityEngine;

public class PlayerController : BaseBehaviour {
  private const string VERTICAL_AXIS = "Vertical";
  private const string HORIZONTAL_AXIS = "Horizontal";
  private const string RUN_KEY = "Run";

  private MovementController playerMovementController;
  private Speed playerSpeed;

  protected override void OnStart() {
    base.OnStart();

    playerMovementController = Player.Instance.GetComponent<MovementController>();
    playerSpeed = Player.Instance.GetComponent<Speed>();
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    if (Input.GetButtonDown(RUN_KEY)) {
      playerSpeed.IsRunning = true;
    }

    if (Input.GetButtonUp(RUN_KEY)) {
      playerSpeed.IsRunning = false;
    }

    Vector3 dirVector = Input.GetAxis(HORIZONTAL_AXIS) * CameraController.Instance.transform.right / 2 +
      Input.GetAxis(VERTICAL_AXIS) * CameraController.Instance.transform.forward;
    dirVector.y = 0;

    if (dirVector == Vector3.zero) {
      playerMovementController.Stop();
    } else {
      playerMovementController.MoveInDirection(dirVector);
    }
  }
}
