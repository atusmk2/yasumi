using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using yasumi.Items;

namespace yasumi
{
    public class yasumiGlobalDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
			if (!NPCID.Sets.CountsAsCritter[npc.type] && !NPCID.Sets.ProjectileNPC[npc.type] && !npc.townNPC) {
				npcLoot.Add(ItemDropRule.OneFromOptions(1000, ModContent.ItemType<AttackUP>(),	ModContent.ItemType<DefenseUP>()));
            }
        }
    }
}