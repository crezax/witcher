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

  public abstract bool IsValidTarget(GameObject target);
  public abstract void OnEffectStart(GameObject target);
  public abstract void OnEffectStay(GameObject target);
  public abstract void OnEffectEnd(GameObject target);

  public float DurationLeft { get; set; }

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

    OnEffectEnd(target);
  }
}
