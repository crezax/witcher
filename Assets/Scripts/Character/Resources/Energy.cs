using UnityEngine;

public class Energy : Resource {
  private float lastTimeSpent;

  public float RegenerationDelay { get; set; }

  protected override bool ShouldRegen {
    get {
      return Time.time - lastTimeSpent > RegenerationDelay;
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    lastTimeSpent = Time.time - RegenerationDelay;
    OnValueChanged += OnEnergyChanged;
  }

  private void OnEnergyChanged(float oldValue, float newValue) {
    if (oldValue > newValue) {
      lastTimeSpent = Time.time;
    }
  }
}
