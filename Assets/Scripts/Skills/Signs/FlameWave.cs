using UnityEngine;

public class FlameWave : EffectWave {
  public float BurnDamage { get; set; }
  public float BurnDuration { get; set; }

  protected override void InitEffect(GameObject effectGO) {
    BurnEffect burn = effectGO.GetComponent<BurnEffect>();
    burn.BurnDamage = BurnDamage;
    burn.DurationLeft = BurnDuration;
  }
}
