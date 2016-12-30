using UnityEngine;

public class Igni : Sign {
  [SerializeField]
  private GameObject flameWavePrefab;

  public float BurnDamage { get; set; }
  public float BurnDuration { get; set; }

  protected override float EnergyCost {
    get {
      return 100;
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    // defaults
    BurnDamage = 10;
    BurnDuration = 5;
  }

  protected override void PerformImplementation(GameObject target) {
    // Maybe rotate towards target 1st?
    GameObject flameWaveGO = (GameObject)Instantiate(
      flameWavePrefab,
      transform.position,
      transform.rotation
    );
    FlameWave flameWave = flameWaveGO.GetComponent<FlameWave>();
    flameWave.Caster = gameObject;
    flameWave.BurnDamage = BurnDamage;
    flameWave.BurnDuration = BurnDuration;
  }
}
