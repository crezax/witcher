public class Player : Character, IHasRotationSpeed {
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
