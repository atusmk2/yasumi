using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace yasumi.Items
{
	public class Resetter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Resetter");
			Tooltip.SetDefault("Right Click on upgraded item to reset to its default stats.");
			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}
		public override void SetDefaults() {
			Item.width = 34;
			Item.height = 34;
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
				.AddIngredient(ItemID.ManaCrystal, 2)
				.AddTile(TileID.WorkBenches)
				.Register();
			}
		}
	}
