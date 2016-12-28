using UnityEngine;

public class Speed : BaseBehaviour {
  [SerializeField]
  private float walkingSpeed;
  [SerializeField]
  private float runningSpeed;

  protected float WalkingSpeed {
    get {
      return walkingSpeed;
    }
  }

  protected float RunningSpeed {
    get {
      return runningSpeed;
    }
  }

  public bool IsRunning { get; set; }

  public float CurrentSpeed {
    get {
      return IsRunning ? RunningSpeed : WalkingSpeed;
    }
  }
}
