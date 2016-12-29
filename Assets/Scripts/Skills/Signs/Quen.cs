using UnityEngine;

public class Quen : Sign {
  [SerializeField]
  private GameObject shieldPrefab;

  protected override float EnergyCost {
    get {
      return 100;
    }
  }

  protected override void PerformImplementation() {
    Effect.Apply(shieldPrefab, gameObject)
      .GetComponent<ShieldEffect>()
      .DurationLeft = 30;
  }
}
