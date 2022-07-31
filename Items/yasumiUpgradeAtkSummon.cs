using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;
using yasumi.Common;

namespace yasumi.Items
{
		public class yasumiUpgradeAttackSummon : GlobalItem {
		public override bool InstancePerEntity => true;
		public int damageplus;
		internal int maxDamage = ModContent.GetInstance<yasumiConfig>().maxDamage;
		internal int damUp;
		public bool CheckWpn(Item Item) {return (Item.stack == 1 && Item.damage > 0 && !Item.consumable && Item.CountsAsClass(DamageClass.Summon)) && !Item.accessory;}
		public bool DamUpgrader() {
			if (Main.mouseItem.type == ModContent.ItemType<AttackUPSummon>()) {
				damUp = 2;
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
		public bool WeaponLimit() {
			if (!Main.hardMode && ((DamUpgrader() && damageplus < 40))) {return true;}
			if (Main.hardMode && ((DamUpgrader() && damageplus < 80))) {return true;}
			if (NPC.downedMoonlord && damageplus < maxDamage && DamUpgrader()) {return true;}
			return false;
		}
		public override bool CanRightClick(Item Item)
		{
			if (CheckWpn(Item) && WeaponLimit()) {return true;}
			if (CheckWpn(Item) && damageplus != 0 && Resetter()) {return true;}
			return false;
		}
		public override void RightClick(Item Item, Player player)
		{
			if (CheckWpn(Item) && DamUpgrader()) {
				damageplus += damUp;
				Item.stack++;
				Main.mouseItem.stack--;
			}
			if (CheckWpn(Item) && Resetter()) {
				player.QuickSpawnItem(player.GetSource_Misc("drop"), ModContent.ItemType<AttackUPSummon>(), damageplus / 2);
				damageplus -= damageplus;
				Item.stack++;
				Main.mouseItem.stack--;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (CheckWpn(item) && damageplus > 0) {
				var line = new TooltipLine(Mod, "yasumi", $"[i:{ModContent.ItemType<AttackUPSummon>()}] [c/92f892:Damage +{damageplus}]");
				var line2 = new TooltipLine(Mod, "yasumi", "[c/ffeb3b:*ALL Minion/Sentry damage will be updated when holding this item.]");
				tooltips.Add(line);
				tooltips.Add(line2);
			}
		}		
		public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
		{
			if (damageplus > 0 && item.CountsAsClass(DamageClass.Summon)) {
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
	public class yasumiGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return entity.CountsAsClass(DamageClass.Summon);
		}
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.LocalPlayer;
			var item = player.HeldItem;
			int bruh = item.GetGlobalItem<yasumiUpgradeAttackSummon>().damageplus;
			if (item.CountsAsClass(DamageClass.Summon) && bruh > 0)
			{
				damage += bruh;
			}
		}
	}
}