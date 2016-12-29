using System.Linq;
using UnityEngine;

public class CameraController : BaseBehaviour {
  private static CameraController instance;

  private const string MOUSE_AXIS_X = "Mouse X";
  private const string MOUSE_AXIS_Y = "Mouse Y";

  private float yawSpeed = 10;
  private float pitchSpeed = 3;
  [SerializeField]
  private CharacterDetector targetingDetector;
  private Character target;

  public Character Target {
    get {
      return target;
    }
    private set {
      if (Target == value) {
        return;
      }
      if (Target != null) {
        UIController.Instance.HideNpcResourceBard(Target);
      }
      target = value;
      if (Target != null) {
        UIController.Instance.ShowNpcResourceBars(Target);
      }
    }
  }

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

    if (targetingDetector.PotentialTargets.Count > 0) {
      Target = targetingDetector
        .PotentialTargets
        .Where(t => t.GetComponent<Character>() != null && t != gameObject)
        .Select(t => t.GetComponent<Character>())
        .OrderBy(
        t => Vector3.Distance(
          Player.Instance.transform.position,
          t.transform.position
        )
      ).First();
    } else {
      Target = null;
    }
  }

  protected override void OnLateUpdate() {
    base.OnLateUpdate();

    transform.position = Player.Instance.transform.position;
  }
}
