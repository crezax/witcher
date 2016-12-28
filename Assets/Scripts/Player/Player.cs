using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(Speed))]
[RequireComponent(typeof(Energy))]
public class Player : BaseBehaviour, IHasRotationSpeed {
  private static Player instance;

  public static Player Instance {
    get {
      return instance;
    }
  }

  private Energy energy;

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
    energy = GetComponent<Energy>();
  }

  protected override void OnStart() {
    base.OnStart();

    energy.MaxEnergy = 100;
    energy.RegenerationDelay = 1;
    energy.RegenerationRate = 50;
  }
}
