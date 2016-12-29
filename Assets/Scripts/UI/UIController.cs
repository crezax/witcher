using System.Collections.Generic;
using UnityEngine;

public class UIController : BaseBehaviour {
  private static UIController instance;

  public static UIController Instance {
    get {
      return instance;
    }
  }

  [SerializeField]
  private GameObject npcInfoPrefab;
  private Dictionary<GameObject, GameObject> npcToResourceBars;

  protected override void OnAwake() {
    base.OnAwake();

    if (instance != null) {
      Destroy(instance.gameObject);
      return;
    }

    instance = this;
    npcToResourceBars = new Dictionary<GameObject, GameObject>();
  }

  public void ShowNpcResourceBars(Character npc) {
    if (npcToResourceBars.ContainsKey(npc.gameObject)) {
      return;
    }
    GameObject npcInfoGO = (GameObject)Instantiate(
      npcInfoPrefab,
      transform,
      false
    );
    npcInfoGO.GetComponent<NPCInfo>().Character = npc;
    npcToResourceBars[npc.gameObject] = npcInfoGO;
  }

  public void HideNpcResourceBard(Character npc) {
    if (!npcToResourceBars.ContainsKey(npc.gameObject)) {
      return;
    }

    Destroy(npcToResourceBars[npc.gameObject]);
    npcToResourceBars.Remove(npc.gameObject);
  }
}
