using UnityEngine;

[RequireComponent(typeof(Energy))]
public class Aard : Sign {
  private Energy energy;
  [SerializeField]
  private GameObject shockwavePrefab;

  protected override float EnergyCost {
    get {
      return 100;
    }
  }

  protected override void PerformImplementation() {
    GameObject shockWaveGO = (GameObject)Instantiate(
      shockwavePrefab,
      transform.position,
      transform.rotation
    );
    shockWaveGO.GetComponent<Shockwave>().Caster = gameObject;
  }

  protected override void OnAwake() {
    base.OnAwake();

    energy = GetComponent<Energy>();
  }
}
