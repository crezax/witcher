using UnityEngine;

public class BurnEffect : Effect {

  public float BurnDamage { get; set; }
  private Health targetHealth;

  protected override bool IsValidTargetImplementation(GameObject target) {
    return target.GetComponent<Health>() != null;
  }

  public override void OnEffectStart(GameObject target) {
    targetHealth = target.GetComponent<Health>();
  }

  public override void OnEffectStay(GameObject target) {
    targetHealth.CurrentValue -= BurnDamage * Time.deltaTime;
  }

  public override void OnEffectEnd(GameObject target) { }
}
