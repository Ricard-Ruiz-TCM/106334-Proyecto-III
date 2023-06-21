using UnityEngine;

public class WeaponSelectorManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneStageManager.onPlayerActive += setEquipmentToPlayer;
    }

    private void OnDisable()
    {
        SceneStageManager.onPlayerActive -= setEquipmentToPlayer;
    }

    private void setEquipmentToPlayer()
    {
        var equip = gameObject.GetComponent<EquipmentManager>();
        //uCore.GameManager.getPlayer().equip.SetEquipment(equip.getArmorInvItem(), equip.getWeaponInvItem(), equip.getShieldInvItem());
    }
}
