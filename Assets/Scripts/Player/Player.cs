using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(Speed))]
public class Player : BaseBehaviour, IHasRotationSpeed {
  private static Player instance;

  public static Player Instance {
    get {
      return instance;
    }
  }

  public float RotationSpeed {
    get {
      return 360;
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
}
