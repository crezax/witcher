using UnityEngine;

// Base class for effects that can be applied to objects
public abstract class Effect : BaseBehaviour {
  private GameObject target;

  public static GameObject Apply(GameObject effectPrefab, GameObject target) {
    GameObject effectGO = (GameObject)Instantiate(
      effectPrefab,
      target.transform,
      false
    );
    effectGO.GetComponent<Effect>().target = target;
    return effectGO;
  }

  public static Effect[] GetEffectsOnTarget(GameObject target) {
    return target.GetComponentsInChildren<Effect>();
  }

  public static void RemoveAllEffects(GameObject target) {
    foreach (Effect effect in GetEffectsOnTarget(target)) {
      Destroy(effect.gameObject);
    }
  }

  protected abstract bool IsValidTargetImplementation(GameObject target);
  public abstract void OnEffectStart(GameObject target);
  public abstract void OnEffectStay(GameObject target);
  public abstract void OnEffectEnd(GameObject target);

  public bool IsValidTarget(GameObject target) {
    Health targetHealth = target.GetComponent<Health>();
    return (targetHealth == null || targetHealth.CurrentValue > 0) &&
      IsValidTargetImplementation(target);
  }

  public float DurationLeft { get; set; }

  private bool wasEffectApplied;

  protected GameObject Target {
    get {
      return target;
    }
  }

  protected override void OnStart() {
    base.OnStart();

    if (!IsValidTarget(target)) {
      Destroy(gameObject);
      return;
    }

    wasEffectApplied = true;
    OnEffectStart(target);
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    if (DurationLeft < 0) {
      Destroy(gameObject);
      return;
    }

    DurationLeft -= Time.deltaTime;
    OnEffectStay(target);
  }

  protected override void OnWillDestroy() {
    base.OnWillDestroy();

    if (wasEffectApplied) {
      OnEffectEnd(target);
    }
  }
}
