using UnityEngine;

public class YrdenTrap : BaseBehaviour {
  [SerializeField]
  private GameObject snareEffectPrefab;

  public float Duration { get; set; }

  protected override void OnStart() {
    base.OnStart();

    Destroy(gameObject, Duration);
  }

  protected override void OnTriggerDidEnter(Collider collider) {
    base.OnTriggerDidEnter(collider);

    Effect.Apply(snareEffectPrefab, collider.gameObject);
  }
}
