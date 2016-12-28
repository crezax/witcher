using UnityEngine;

public class PlayerController : BaseBehaviour {
  private const string VERTICAL_AXIS = "Vertical";
  private const string HORIZONTAL_AXIS = "Horizontal";
  private const string RUN_KEY = "Run";

  private const float RUN_ENERGY_COST = 5;

  private MovementController playerMovementController;
  private Speed playerSpeed;
  private Energy playerEnergy;

  protected override void OnStart() {
    base.OnStart();

    playerMovementController = Player.Instance.GetComponent<MovementController>();
    playerSpeed = Player.Instance.GetComponent<Speed>();
    playerEnergy = Player.Instance.GetComponent<Energy>();
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    if (Input.GetButtonDown(RUN_KEY) && playerEnergy.CurrentEnergy > RUN_ENERGY_COST) {
      playerSpeed.IsRunning = true;
    }

    if (Input.GetButtonUp(RUN_KEY)) {
      playerSpeed.IsRunning = false;
    }

    if (playerSpeed.IsRunning && playerMovementController.IsMoving) {
      playerEnergy.CurrentEnergy -= RUN_ENERGY_COST * Time.deltaTime;
      if (playerEnergy.CurrentEnergy == 0) {
        playerSpeed.IsRunning = false;
      }
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
