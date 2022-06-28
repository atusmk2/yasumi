using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace yasumi.Items
{
    public class energy_cube : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Cube");
			// Tooltip.SetDefault("Used to enhance weapon/armor stats.\nAdd +5 Damage to weapon or +1 Defense to Armor/Accessories.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3,8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults() {
			Item.width = 31;
			Item.height = 30;
            Item.consumable = true;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Purple;
		}
        public override void PostUpdate() {
			Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.7f * Main.essScale);
		}
    }
}