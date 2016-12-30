using UnityEngine;

public class Shockwave : EffectWave {
  public float Power { get; set; }
  public float Damage { get; set; }

  protected override void OnAwake() {
    base.OnAwake();

    Power = 20;
    Damage = 5;
  }

  protected override void InitEffect(GameObject effectGO) {
    KnockbackEffect knockback = effectGO.GetComponent<KnockbackEffect>();
    knockback.Force = Power * transform.forward;
    knockback.Source = transform;
    knockback.Damage = Damage;
  }
}
