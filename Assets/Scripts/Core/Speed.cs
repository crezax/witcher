using UnityEngine;

public class Speed : BaseBehaviour {
  [SerializeField]
  private float baseSpeed;
  [SerializeField]
  private float bonusSpeed;

  public float BaseSpeed {
    get {
      return baseSpeed;
    }
  }

  public float BonusSpeed { get; set; }

  public float CurrentSpeed {
    get {
      return BonusSpeed + BaseSpeed;
    }
  }
}
