using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using yasumi.Items;

namespace yasumi.Common
{
	public class yasumiGlobalDrop : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
			if (!NPCID.Sets.CountsAsCritter[npc.type] && !NPCID.Sets.ProjectileNPC[npc.type] && !npc.townNPC && !npc.boss) {
				npcLoot.Add(ItemDropRule.OneFromOptions(200, ModContent.ItemType<AttackUP>(), ModContent.ItemType<DefenseUP>()));
			}
			if (!Main.hardMode && (npc.life > 1000 || npc.boss)) {
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AttackUP>(),3,1,2));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DefenseUP>(),3,1,2));
			}
			if (Main.hardMode && (npc.life > 2000 || npc.boss)) {
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AttackUP>(),3,2,4));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DefenseUP>(),3,2,4));
			}
			if (Main.hardMode && (npc.life > 10000 && npc.boss)) {
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AttackUP>(),2,5,6));
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DefenseUP>(),2,5,6));
			}
		}
	}
}