using UnityEngine;

// Base class for effects that can be applied to objects
public abstract class Effect : BaseBehaviour {
  private GameObject target;

  public abstract bool IsValidTarget();
  public abstract void OnEffectStart();
  public abstract void OnEffectStay();
  public abstract void OnEffectEnd();

  public float DurationLeft { get; set; }

  protected override void OnAwake() {
    base.OnAwake();

    if (!IsValidTarget()) {
      Destroy(this);
      return;
    }
  }

  protected override void OnStart() {
    base.OnStart();

    OnEffectStart();
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    if (DurationLeft < 0) {
      Destroy(this);
      return;
    }

    DurationLeft -= Time.deltaTime;
    OnEffectStay();
  }

  protected override void OnWillDestroy() {
    base.OnWillDestroy();

    OnEffectEnd();
  }
}
