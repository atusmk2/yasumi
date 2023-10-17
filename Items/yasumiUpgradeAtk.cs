using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;
using yasumi.Common;

namespace yasumi.Items
{
		public class yasumiUpgradeAttack : GlobalItem {
		public override bool InstancePerEntity => true;
		public int damageplus;
		internal int damUp;
		public bool CheckWeapon(Item Item) {return (Item.stack == 1 && Item.damage > 0 && !Item.consumable && !Item.CountsAsClass(DamageClass.Summon)) && !Item.accessory;}
		public bool DamUpgrader() {
			if (Main.mouseItem.type == ModContent.ItemType<AttackUP>()) {
				damUp = 5;
				return true;
			}
			return false;
		}
		public bool Resetter() {
			if (Main.mouseItem.type == ModContent.ItemType<Resetter>()) {
				return true;
			}
			return false;
		}
		
		public override bool CanRightClick(Item Item)
		{
			if (CheckWeapon(Item) && DamUpgrader()) {return true;}
			if (CheckWeapon(Item) && damageplus != 0 && Resetter()) {return true;}
			return false;
		}
		public override void RightClick(Item Item, Player player)
		{
			if (CheckWeapon(Item) && DamUpgrader()) {
				damageplus += damUp;
				Item.stack++;
				Main.mouseItem.stack--;
			}
			if (CheckWeapon(Item) && Resetter()) {
				player.QuickSpawnItem(player.GetSource_Misc("drop"), ModContent.ItemType<AttackUP>(), damageplus / 5);
				damageplus -= damageplus;
				Item.stack++;
				Main.mouseItem.stack--;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (CheckWeapon(item) && damageplus > 0) {
				var line = new TooltipLine(Mod, "yasumi", $"[i:{ModContent.ItemType<AttackUP>()}] [c/92f892:Damage +{damageplus}]");
				tooltips.Add(line);
			}
		}		
		public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
		{
			if (damageplus > 0 && !item.CountsAsClass(DamageClass.Summon)) {
				damage.Flat += damageplus;
			}
		}
		public override void SaveData(Item item, TagCompound tag)
		{
			if (damageplus > 0) {
				tag["damageplus"] = damageplus;
			}
		}
		public override void LoadData(Item item, TagCompound tag)
		{
			if (tag["damageplus"] != null) {
				damageplus = (int) tag["damageplus"];
			}
		}
		public override void NetSend(Item item, BinaryWriter writer)
		{
			writer.Write7BitEncodedInt(damageplus);
		}
		public override void NetReceive(Item item, BinaryReader reader)
		{
			damageplus = reader.Read7BitEncodedInt();
		}
	}
}