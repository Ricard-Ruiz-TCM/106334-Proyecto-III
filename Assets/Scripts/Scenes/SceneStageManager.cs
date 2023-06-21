using FMODUnity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneStageManager : MonoBehaviour {

    [SerializeField, Header("Music:")]
    private EventReference backgroundMusic;

    [SerializeField, Header("Dialog:")]
    private GameObject _dialogUI;
    [SerializeField]
    private GameObject _tutorialUI;
    [SerializeField]
    private GameObject _upgradeUI;
    [SerializeField]
    private GameObject _perksUI;    
    [SerializeField]
    private PerkSelectorUI _leftPerkPanel, _rightPerkPanel;
    [SerializeField]
    private GameObject _weaponsUI;
    [SerializeField]
    private WeaponSelectorUI _leftWeaponPanel, _midWeaponPanel, _rightWeaponPanel;


    [SerializeField, Header("Combat:")]
    private GameObject _turnUI;
    [SerializeField]
    private GameObject _playerUI;

    [SerializeField, Header("Stage:")]
    private StageObjetiveUI _objetiveUI;
    [SerializeField]
    private StageResolutionUI _stageResolutionUI;

    [SerializeField, Header("Estado del stage:")]
    private stageState _state = stageState.innDialog;

    [SerializeField, Header("Información del Nivel:")]
    private Stage _stage;
    [SerializeField]
    private StageData _data;

    /** Escena actual */
    [SerializeField, Header("Escena:")]
    private gameScenes _thisScene;
    [SerializeField]
    private gameScenes _nextScene;

    [SerializeField, Header("Entidades con turno en la escena:")]
    private GameObject _player;
    [SerializeField]
    private List<GameObject> _actors;

    public static event Action onPlayerActive;

    // Unity OnEnable
    void OnEnable() {
        // DialogTriggers
        OnEndDialog.onTrigger += nextStageState;
        OnOpenPerkPanel.onTrigger += openPerkPanel;
        OnOpenWeaponPanel.onTrigger += openWeaponPanel;
        OnOpenUpgradePanel.onTrigger += openUpgradePanel;
        OnNextStageDialog.onTrigger += stageSuccess;

        Stage.onCompleteStage += completeStage;
    }

    // Unity OnDisable
    void OnDisable() {
        // DialogTriggers
        OnEndDialog.onTrigger -= nextStageState;
        OnOpenPerkPanel.onTrigger -= openPerkPanel;
        OnOpenWeaponPanel.onTrigger -= openWeaponPanel;
        OnOpenUpgradePanel.onTrigger -= openUpgradePanel;
        OnNextStageDialog.onTrigger -= stageSuccess;

        Stage.onCompleteStage -= completeStage;
    }

    // Unity Start
    void Start() {

        FMODManager.instance.ChangeBackgroundMusic(backgroundMusic);

        // Check si tenemos que ir al combat directo
        if (_data.innitialDialog == null) {
            _state = stageState.shiftManaged;
        }

        // Set data to Stage
        _stage.SetData(_data);

        // Set del player al gamemanager
        uCore.GameManager.setPlayer(_player.GetComponent<Actor>());

        // Set del estado del estado, a funciones y cositas
        buildStage();

        FadeFX.instance.FadeOut();
    }

    /** Método para actualizar el stage */
    private void buildStage() {

        // "Clear" del estado del stage
        _dialogUI.SetActive(false);
        _upgradeUI.SetActive(false);
        _perksUI.SetActive(false);
        if (_weaponsUI != null)
            _weaponsUI.SetActive(false);
        _turnUI.SetActive(false);
        _playerUI.SetActive(false);
        _objetiveUI.gameObject.SetActive(false);
        _stageResolutionUI.gameObject.SetActive(false);

        // "Build" para los elementos
        switch (_state) {
            case stageState.innDialog:
                startDialog(_data.innitialDialog);
                break;
            case stageState.shiftManaged:
                startCombat();
                break;
            case stageState.endDialog:
                startDialog(_data.lastDialog);
                break;
            case stageState.completed:
                endStage();
                break;
            default:
                break;
        }
    }

    /** Método apra inicializar dialogo */
    private void startDialog(DialogNode node) {
        _dialogUI.SetActive(true);
        // Innit del dialogManager
        DialogManager.instance.startDialog(node);
    }

    private void startTutorial(DialogNode node) {
        _tutorialUI.SetActive(true);
        // Innit del dialogManager
        DialogManager.instance.startDialog(node);
    }

    /** Método para inicializar combate */
    private void startCombat() {
        _objetiveUI.gameObject.SetActive(true);
        if (_data.tutorialDialog != null)
            startTutorial(_data.tutorialDialog);
        _objetiveUI.SetObjetive(_data);
        _playerUI.SetActive(true);
        _turnUI.SetActive(true);

        // Restore Player
        foreach (perkID id in uCore.GameManager.restorePerks()) {
            _player.GetComponent<Actor>().perks.addPerk(id);
        }
        foreach (skillID id in uCore.GameManager.restoreSkills()) {
            _player.GetComponent<Actor>().skills.addSkill(id);
        }

        // Activate the actors
        _player.SetActive(true);
        onPlayerActive?.Invoke();

        int upgrade = 0;
        WeaponInventoryItem wp = new WeaponInventoryItem() { weapon = _player.GetComponent<EquipmentManager>().weapon, upgrade = upgrade };
        // RestoreWeapon
        if (uCore.GameManager.haveWeaponSaved()) {
            WeaponInventoryItem wip = uCore.GameManager.getWeaponInv();
            wp.weapon = wip.weapon;
        }
        if (_thisScene.Equals(gameScenes.Stage7) || _thisScene.Equals(gameScenes.Stage14)) {
            upgrade = 1;
        }
        if (_thisScene.Equals(gameScenes.Stage18)) {
            upgrade = 2;
        }
        upgrade = Mathf.Clamp(upgrade, 0, 3);

        // saver :3 
        if (wp.weapon == null)
            wp.weapon = (WeaponItem)uCore.GameManager.GetItem(itemID.Gladius);

        uCore.GameManager.getPlayer().equip.SetWeapon(wp.weapon, upgrade);
        
        uCore.GameManager.getPlayer().build();

        foreach (GameObject actors in _actors) {
            actors.SetActive(true);
            if (actors.GetComponent<Actor>() != null)
                actors.GetComponent<Actor>().build();
        }

        // Innit del turnManager
        TurnManager.instance.completeRound();
    }

    /** Método para mostrar el final del stage */
    public void endStage() {
        _stageResolutionUI.gameObject.SetActive(true);
    }

    /** Método para abrir el panel de upgrades del blacsmith */
    public void openUpgradePanel() {
        _dialogUI.SetActive(false);
        _upgradeUI.SetActive(true);
    }

    /** Método para abrir el panel de perks */
    public void openPerkPanel() {
        _dialogUI.SetActive(false);
        _perksUI.SetActive(true);

        _leftPerkPanel.setPerk(_data.perks[0]);
        _rightPerkPanel.setPerk(_data.perks[1]);
    }

    public void openWeaponPanel()
    {
        _dialogUI.SetActive(false);
        if (_weaponsUI != null)
            _weaponsUI.SetActive(true);

        _leftWeaponPanel.SetWeapon(_data.weapons[0]);
        _midWeaponPanel.SetWeapon(_data.weapons[1]);
        _rightWeaponPanel.SetWeapon(_data.weapons[2]);
    }

    /** Método para indicar en que estado del stage estamos y pasar al siguiente con el EndNode */
    public void nextStageState() {
        _state = (stageState)((int)_state + 1);

        // Check si es final o terminamos??
        if (_state.Equals(stageState.endDialog)) {
            if (_data.lastDialog == null) {
                _state = stageState.completed;
            }
        }

        buildStage();
    }

    /** Método para determinar qeu se ha ganado el Stage */
    public void stageSuccess() {
        uCore.GameManager.SaveGameData();
        uCore.GameManager.savePerks(_player.GetComponent<Actor>());
        uCore.GameManager.saveSkills(_player.GetComponent<Actor>());
        uCore.GameManager.saveWeapon(_player.GetComponent<Actor>().equip.getWeaponInvItem());
        uCore.Director.LoadSceneAsync(_nextScene, false);
        FadeFX.instance.FadeIn(() => { uCore.Director.AllowScene(); });
    }

    /** Método para determinar que se ha perdido el Stage */
    public void stageFailed() {
        uCore.GameManager.LoadGameData();
        uCore.Director.LoadSceneAsync(_thisScene, false);
        FadeFX.instance.FadeIn(() => { uCore.Director.AllowScene(); });
    }

    /** Botones */
    public void BTN_StageSuccessUIClose() {
        FadeFX.instance.FadeIn(() => { stageSuccess(); });
    }
    /** ------- */

    /** Método para la resolución del stage */
    public void completeStage(stageResolution res) {
        _stageResolutionUI.SetResolution(res, stageSuccess, stageFailed);
        nextStageState();
    }

}
