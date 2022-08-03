using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace yasumi.Items
{
	public class DefenseUP : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Defense UP");
			Tooltip.SetDefault("Right Click on armor/accessories to add +1 Defense.");
			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}
		public override void SetDefaults() {
			Item.width = 26;
			Item.height = 28;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Purple;
		}
		public override void PostUpdate() {
			Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.7f * Main.essScale);
		}
	}
}