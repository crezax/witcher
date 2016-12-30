using UnityEngine;

public class Axii : Sign {
  [SerializeField]
  private GameObject incapacitateEffectPrefab;

  protected override float EnergyCost {
    get {
      return 100;
    }
  }

  public override bool CanPerform(GameObject target) {
    return target != null && base.CanPerform(target);
  }

  protected override void PerformImplementation(GameObject target) {
    Effect.Apply(incapacitateEffectPrefab, target)
      .GetComponent<IncapacitateEffect>()
      .DurationLeft = 7;
  }
}
