using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace yasumi.Items
{
	public class AttackUPSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Attack UP (Summon)");
			Tooltip.SetDefault("Right Click on summon weapon to add +2 Damage.");
			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}
		public override void SetDefaults() {
			Item.width = 30;
			Item.height = 28;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Purple;
		}
		public override void PostUpdate() {
			Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.7f * Main.essScale);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<AttackUP>()
				.Register();
		}
	}
}