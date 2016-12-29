using UnityEngine;

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

  protected override void OnAttackerRegistered(GameObject attacker) {
    base.OnAttackerRegistered(attacker);

    Character character = attacker.GetComponent<Character>();
    if (character == null) {
      return;
    }
    UIController.Instance.ShowNpcResourceBars(character);
  }

  protected override void OnAttackerUnregistered(GameObject attacker) {
    base.OnAttackerUnregistered(attacker);

    Character character = attacker.GetComponent<Character>();
    if (character == null) {
      return;
    }
    UIController.Instance.HideNpcResourceBard(character);
  }
}
