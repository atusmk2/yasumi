using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria.DataStructures;

namespace yasumi.Items
{
		public class yasumiUpgradeAttack : GlobalItem {
		public override bool InstancePerEntity => true;
		
		public int damageplus;
		public int damUp;
		public bool CheckWpn(Item Item) {return (Item.stack == 1 && Item.damage > 0 && !Item.consumable && !Item.CountsAsClass(DamageClass.Summon));}
		public bool DamUpgrader() {
			if (Main.mouseItem.type == ModContent.ItemType<AttackUP>()) {
				damUp = 5;
				return true;
			}
			return false;
		}
		public int LvLimit() {
			if (!Main.hardMode) {return 5;}
			if (Main.hardMode) {return 10;}
			else return 0;
		}
		public bool WeaponLimit() {
			if (!Main.hardMode && ((DamUpgrader() && damageplus < (LvLimit() * 8)))) {return true;}
			if (Main.hardMode && ((DamUpgrader() && damageplus < (LvLimit() * 8)))) {return true;}
			if (NPC.downedMoonlord && DamUpgrader()) {return true;}
			return false;
		}
		public override bool CanRightClick(Item Item)
		{
			if (CheckWpn(Item) && WeaponLimit()) {return true;}
			return false;
		}
		public override void RightClick(Item Item, Player player)
		{
			if (CheckWpn(Item) && DamUpgrader()) {
				damageplus += damUp;
				Item.stack++;
				Main.mouseItem.stack--;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (CheckWpn(item) && damageplus > 0) {
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
			tag["damageplus"] = damageplus;
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