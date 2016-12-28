using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class CameraController : PlayerController {
  private static CameraController instance;

  private const string MOUSE_AXIS_X = "Mouse X";
  private const string MOUSE_AXIS_Y = "Mouse Y";

  private float yawSpeed = 4;
  private float pitchSpeed = 3;

  public static CameraController Instance {
    get {
      return instance;
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    if (instance != null) {
      Destroy(gameObject);
      return;
    }

    instance = this;
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    Vector3 targetRotation = transform.eulerAngles + new Vector3(
      -Input.GetAxis(MOUSE_AXIS_Y) * pitchSpeed,
      Input.GetAxis(MOUSE_AXIS_X) * yawSpeed,
      0
    );

    float xAngleAbsoluteLimit = 30;
    if (targetRotation.x < 360 - xAngleAbsoluteLimit && targetRotation.x > 180) {
      targetRotation.x = 360 - xAngleAbsoluteLimit;
    }

    if (targetRotation.x > xAngleAbsoluteLimit && targetRotation.x <= 180) {
      targetRotation.x = xAngleAbsoluteLimit;
    }

    transform.eulerAngles = targetRotation;
  }

  protected override void OnLateUpdate() {
    base.OnLateUpdate();

    transform.position = Player.Instance.transform.position;
  }
}
