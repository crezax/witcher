using UnityEngine;

public class PlayerController : BaseBehaviour {
  private const string VERTICAL_AXIS = "Vertical";
  private const string HORIZONTAL_AXIS = "Horizontal";
  private const string MOUSE_WHEEL_AXIS = "Mouse ScrollWheel";
  private const string RUN_BUTTON = "Run";
  private const string SIGN_BUTTON = "Sign";
  private const string SWORD_BUTTON = "Sword";

  private const float RUN_ENERGY_COST = 5;

  private static PlayerController instance;
  public static PlayerController Instance {
    get {
      return instance;
    }
  }

  private MovementController playerMovementController;
  private Speed playerSpeed;
  private Energy playerEnergy;
  private Sign[] signs;
  private int selectedSignId;
  private MeleeAttack meleeAttack;
  private float bonusRunSpeed = 3;
  private bool isRunning;

  public Sign SelectedSign {
    get {
      return signs[selectedSignId];
    }
  }

  protected override void OnAwake() {
    base.OnAwake();

    if (instance != null) {
      Destroy(gameObject);
      return;
    }

    instance = this;
  }

  protected override void OnStart() {
    base.OnStart();

    // We cache all these scripts in "OnStart" instead of "OnAwake" to be sure
    // player is already spawned. Should player not be spawned immediately, we
    // could just use a coroutine that would wait for player spawn here, or
    // delegate on player that would be invoked on spawn
    playerMovementController = Player.Instance.GetComponent<MovementController>();
    playerSpeed = Player.Instance.GetComponent<Speed>();
    playerEnergy = Player.Instance.GetComponent<Energy>();
    signs = Player.Instance.GetComponents<Sign>();
    meleeAttack = Player.Instance.GetComponent<MeleeAttack>();
    selectedSignId = 0;
  }

  protected override void OnUpdate() {
    base.OnUpdate();
    HandleMovement();
    HandleSigns();
    HandleSwordCombat();
  }

  private bool IsRunning {
    get {
      return isRunning;
    }
    set {
      if (isRunning == value) {
        return;
      }
      isRunning = value;
      if (isRunning) {
        playerSpeed.BonusSpeed += bonusRunSpeed;
      } else {
        playerSpeed.BonusSpeed -= bonusRunSpeed;
      }
    }
  }

  private void HandleMovement() {
    if (Input.GetButtonDown(RUN_BUTTON) && playerEnergy.CurrentValue > RUN_ENERGY_COST) {
      IsRunning = true;
    }

    if (Input.GetButtonUp(RUN_BUTTON)) {
      IsRunning = false;
    }

    if (IsRunning && playerMovementController.IsMoving) {
      playerEnergy.CurrentValue -= RUN_ENERGY_COST * Time.deltaTime;
      if (playerEnergy.CurrentValue == 0) {
        IsRunning = false;
      }
    }

    Vector3 dirVector = Input.GetAxis(HORIZONTAL_AXIS) * CameraController.Instance.transform.right / 2 +
      Input.GetAxis(VERTICAL_AXIS) * CameraController.Instance.transform.forward;
    dirVector.y = 0;

    if (dirVector == Vector3.zero) {
      playerMovementController.Stop();
    } else {
      playerMovementController.MoveInDirection(dirVector);
    }
  }

  private void HandleSigns() {
    selectedSignId = (selectedSignId + signs.Length + (int)Input.GetAxis(MOUSE_WHEEL_AXIS)) % signs.Length;

    if (Input.GetButtonDown(SIGN_BUTTON)) {
      SelectedSign.Perform(CameraController.Instance.TargetGO);
    }
  }

  private void HandleSwordCombat() {
    if (Input.GetButtonDown(SWORD_BUTTON)) {
      GameObject targetGO = CameraController.Instance.TargetGO;
      if (targetGO != null && !meleeAttack.CanPerform(targetGO) && !Player.Instance.IsUsingSkill) {
        // Perform jump animation towards target, we don't have that sadly
      } else {
        meleeAttack.Perform(targetGO);
      }
    }
  }
}
