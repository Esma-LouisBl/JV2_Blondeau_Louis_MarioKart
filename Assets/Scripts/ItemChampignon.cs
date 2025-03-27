using UnityEngine;

[CreateAssetMenu(fileName = "ItemChampignon", menuName = "Scriptable Objects/ItemChampignon")]
public class ItemChampignon : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.carControler.Turbo();
    }
}
