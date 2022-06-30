using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using yasumi.Items;

namespace yasumi
{
	public class yasumiUpgrade : GlobalItem {
		public override bool InstancePerEntity => true;
		public int damageplus;
		public int defenseplus;
		public int damUp;
		public int defUp;
		public bool CheckAcc(Item Item) {return (Item.accessory == true);}
		public bool CheckWpn(Item Item) {return (Item.stack == 1 && Item.damage > 0 && !Item.consumable);}
		public bool CheckArm(Item Item) {return (Item.headSlot != -1 || Item.bodySlot != -1 || Item.legSlot != -1);}
		public bool DamUpgrader() {
			if (Main.mouseItem.type == ModContent.ItemType<AttackUP>()) {
				damUp = 5;
				return true;
			}
			// if (Main.mouseItem.type == ModContent.ItemType<energy_cube>()) {
			// 	damUp = 20;
			// 	return true;
			// }
			return false;
		}
		public bool DefUpgrader() {
			if (Main.mouseItem.type == ModContent.ItemType<DefenseUP>()) {
				defUp = 1;
				return true;
			}
			// if (Main.mouseItem.type == ModContent.ItemType<energy_cube2>()) {
			// 	defUp = 5;
			// 	return true;
			// }
			return false;
		}
		public int LvLimit() {
			if (!Main.hardMode) {return 5;}
			if (Main.hardMode) {return 10;}
			else return 0;
		}
		public bool WeaponLimit() {
			if (!Main.hardMode && ((DamUpgrader() && damageplus < (LvLimit() * 4)))) {return true;}
			if (Main.hardMode && ((DamUpgrader() && damageplus < (LvLimit() * 4)))) {return true;}
			if (NPC.downedMoonlord && DamUpgrader()) {return true;}
			return false;
		}
		public bool ArmorLimit() {
			if (!Main.hardMode && ((DefUpgrader() && defenseplus < LvLimit()))) {return true;}
			if (Main.hardMode && ((DefUpgrader() && defenseplus < LvLimit()))) {return true;}
			if (NPC.downedMoonlord && DefUpgrader()) {return true;}
			return false;
		}
		public override bool CanRightClick(Item Item)
		{
			if (CheckWpn(Item) && WeaponLimit()) {return true;}
			if ((CheckAcc(Item) || CheckArm(Item)) && ArmorLimit()) {return true;}
			return false;
		}
		public override void RightClick(Item Item, Player player)
		{
			if (CheckWpn(Item) && DamUpgrader()) {
				damageplus += damUp;
				Item.stack++;
				Main.mouseItem.stack--;
			}
			if ((CheckArm(Item) || CheckAcc(Item)) && DefUpgrader()) {
				defenseplus += defUp;
				Item.stack++;
				Main.mouseItem.stack--;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (CheckWpn(item) && damageplus > 0) {
				var line = new TooltipLine(Mod, "yasumi", $"[i:{ModContent.ItemType<AttackUP>()}] [c/92f892:Damage +{damageplus}]") {
					// OverrideColor = new Color(146, 248, 146)
				};
				tooltips.Add(line);
			}
			if ((CheckArm(item) || CheckAcc(item)) && defenseplus > 0) {
				var line = new TooltipLine(Mod, "yasumi", $"[i:{ModContent.ItemType<DefenseUP>()}] [c/92f892:Defense +{defenseplus}]\n[c/ffeb3b:*Stats will be updated upon being worn.]") {
					// OverrideColor = new Color(134, 134, 239)
				};
				tooltips.Add(line);
			}
		}		
		public override void UpdateEquip(Item Item, Player player)
		{
			if (defenseplus > 0) {
				Item.defense = Item.OriginalDefense + defenseplus;
			}
		}
		public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
		{
			if (damageplus > 0) {
				damage.Flat += damageplus;
			}
		}
		public override void SaveData(Item item, TagCompound tag)
		{
			tag["damageplus"] = damageplus;
			tag["defenseplus"] = defenseplus;
		}
		public override void LoadData(Item item, TagCompound tag)
		{
			if (tag["damageplus"] != null) {
				damageplus = (int) tag["damageplus"];
				defenseplus = (int) tag["defenseplus"];
			}
		}
	}
}