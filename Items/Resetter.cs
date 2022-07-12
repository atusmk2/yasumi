using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
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
			// Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3,8));
			// ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}
		public override void SetDefaults() {
			Item.width = 30;
			Item.height = 30;
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
