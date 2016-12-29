using UnityEngine;
using UnityEngine.UI;

public class NPCInfo : BaseBehaviour {
  [SerializeField]
  private ResourceBarController healthController;
  [SerializeField]
  private Text characterName;
  private Character character;

  public Character Character {
    get {
      return character;
    }
    set {
      character = value;
      healthController.resource = character.Health;
      characterName.text = character.name;
    }
  }

  protected override void OnUpdate() {
    base.OnUpdate();

    if (Character == null) {
      return;
    }

    transform.position = Camera.main.WorldToScreenPoint(
      Character.transform.position + Vector3.up * Character.Height
    );
  }
}
