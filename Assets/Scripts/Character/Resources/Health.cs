using UnityEngine;

public class Health : Resource {
  public float DecayTime { get; set; }

  public delegate void DeathDelegate();
  public event DeathDelegate OnDeath;

  private Animator animator;

  protected override void OnAwake() {
    base.OnAwake();

    OnValueChanged += OnHealthChanged;
    animator = GetComponent<Animator>();
  }

  private void OnHealthChanged(float oldValue, float newValue) {
    if (newValue != 0 || oldValue == 0) {
      return;
    }
    OnValueWillChange += PreventHealthChange;
    if (OnDeath != null) {
      OnDeath();
    }
    if (animator != null) {
      animator.SetTrigger(AnimationConstants.DIE);
    }
  }

  private float PreventHealthChange(float oldValue, float newValue) {
    return oldValue;
  }
}
