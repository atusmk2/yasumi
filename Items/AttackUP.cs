using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace yasumi.Items
{
	public class AttackUP : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Attack UP");
			Tooltip.SetDefault("Right Click on weapon to add +5 Damage.");
			// Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3,8));
			// ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}
		public override void SetDefaults() {
			Item.width = 28;
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