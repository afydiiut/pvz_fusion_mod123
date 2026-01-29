using Il2Cpp;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(Better_Pot_Fusion.Core), "PvZ Fusion All-In-One", "1.0.0", "Dyna", null)]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace Better_Pot_Fusion
{
    public class Core : MelonMod
    {
        public override void OnUpdate()
        {
            // دمج كل النباتات تلقائياً عند النقر
            if (Board.Instance != null && Mouse.Instance != null && Mouse.Instance.theItemOnMouse != null && Input.GetMouseButtonDown(0))
            {
                int col = Mouse.Instance.theMouseColumn;
                int row = Mouse.Instance.theMouseRow;
                int handId = Mouse.Instance.thePlantTypeOnMouse;

                for (int i = 0; i < Board.Instance.plantArray.Count; i++)
                {
                    var plant = Board.Instance.plantArray[i];
                    if (plant != null && plant.thePlantColumn == col && plant.thePlantRow == row)
                    {
                        // جلب نتيجة الدمج لكل النباتات المتاحة في نسخة 3.2.1
                        int result = Board.Instance.GetMixPlant(plant.thePlantType, handId);
                        if (result != 0)
                        {
                            CreatePlant.Instance.SetPlant(col, row, result, null, Vector2.zero, true, 0f);
                            if (Mouse.Instance.theCardOnMouse != null) Mouse.Instance.theCardOnMouse.CD = 0f;
                            plant.Die(0);
                            Mouse.Instance.ClearItemOnMouse(false);
                            break;
                        }
                    }
                }
            }
        }
    }
}
