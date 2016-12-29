using UnityEngine;

// Test inspired by https://youtu.be/iA_Y-R9dbHA?t=1m39s
[RequireComponent(typeof(Energy))]
public class Aard : Sign {
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
}
