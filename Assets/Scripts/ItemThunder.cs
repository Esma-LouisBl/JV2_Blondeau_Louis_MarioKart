using UnityEngine;

[CreateAssetMenu(fileName = "ItemThunder", menuName = "Scriptable Objects/ItemThunder")]
public class ItemThunder : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.carControler.Thunder();
    }
}
