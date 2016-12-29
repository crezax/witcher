using UnityEngine;

public class Shockwave : EffectWave {
  public float Power { get; set; }

  protected override void OnAwake() {
    base.OnAwake();

    Power = 20;
  }

  protected override void InitEffect(GameObject effectGO) {
    KnockbackEffect knockback = effectGO.GetComponent<KnockbackEffect>();
    knockback.Force = Power * transform.forward;
    knockback.Source = transform;
  }
}
