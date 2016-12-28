using UnityEngine;

public class Energy : BaseBehaviour {
  private float currentEnergy;
  private float lastTimeSpent;

  public float MaxEnergy { get; set; }
  public float RegenerationDelay { get; set; }
  public float RegenerationRate { get; set; }

  public float CurrentEnergy {
    get {
      return currentEnergy;
    }
    set {
      if (value < currentEnergy) {
        lastTimeSpent = Time.time;
      }
      currentEnergy = Mathf.Max(0, Mathf.Min(MaxEnergy, value));
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    lastTimeSpent = Time.time - RegenerationDelay;
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    if (Time.time - lastTimeSpent > RegenerationDelay) {
      currentEnergy = Mathf.Min(
        MaxEnergy,
        currentEnergy + RegenerationRate * Time.deltaTime
      );
    }
  }
}
