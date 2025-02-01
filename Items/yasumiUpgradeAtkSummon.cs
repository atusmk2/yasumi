using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using System.Collections.Generic;
using System.IO;

namespace yasumi.Items
{
		public class yasumiUpgradeAttackSummon : GlobalItem {
		public override bool InstancePerEntity => true;
		public int damageplus;
		public static LocalizedText Damagetextsummon { get; private set;}
		public override void SetStaticDefaults() {
			Damagetextsummon = Mod.GetLocalization("Damagetextsummon");
		}
		internal int damUp;
		public bool CheckWeaponSummon(Item Item) {return (Item.stack == 1 && Item.damage > 0 && !Item.consumable && Item.CountsAsClass(DamageClass.Summon)) && !Item.accessory;}
		public bool DamUpgrader() {
			if (Main.mouseItem.type == ModContent.ItemType<AttackUPSummon>()) {
				damUp = 1;
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
			if (CheckWeaponSummon(Item) && DamUpgrader()) {return true;}
			if (CheckWeaponSummon(Item) && damageplus != 0 && Resetter()) {return true;}
			return false;
		}
		public override void RightClick(Item Item, Player player)
		{
			if (CheckWeaponSummon(Item) && DamUpgrader()) {
				damageplus += damUp;
				Item.stack++;
				Main.mouseItem.stack--;
			}
			if (CheckWeaponSummon(Item) && Resetter()) {
				player.QuickSpawnItem(player.GetSource_Misc("drop"), ModContent.ItemType<AttackUPSummon>(), damageplus);
				damageplus -= damageplus;
				Item.stack++;
				Main.mouseItem.stack--;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (CheckWeaponSummon(item) && damageplus > 0) {
				var line = new TooltipLine(Mod, "yasumi", Damagetextsummon.Format(damageplus * 20, damageplus, item.OriginalDamage));
				tooltips.Add(line);
			}
		}		
		public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
		{
			if (damageplus > 0 && item.CountsAsClass(DamageClass.Summon)) { // This actually does nothing on summon weapon, just for showing numbers.
				damage += (0.2f * damageplus);
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
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.LocalPlayer;
			var item = player.HeldItem;
			int bruh = item.GetGlobalItem<yasumiUpgradeAttackSummon>().damageplus;
			if (item.CountsAsClass(DamageClass.Summon) && bruh > 0)
			{
				modifiers.ScalingBonusDamage += bruh * 0.2f ;
			}
		}
	}
}