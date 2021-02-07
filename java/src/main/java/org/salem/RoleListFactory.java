package org.salem;

import java.util.Arrays;

/**
 * Represents a role list. It may contain either fixed role slots, or wildcard role slots.
 */
public class RoleListFactory
{
    /** Gets the All Any role list. */
    public static final RoleList AllAny = generateAllAny();

    private static RoleList generateAllAny()
    {
        var slots = new RoleSlot[15];
        Arrays.fill(slots, RoleAlignment.Any);
        return new RoleList(slots);
    }

    // Classic
    // Classic
    /** Gets the Classic role list. */
    public static final RoleList Classic = generateClassic();

    private static RoleList generateClassic()
    {
        var builder = new RoleListBuilder();

        builder.add(Sheriff.class);
        builder.add(Lookout.class);
        builder.add(Investigator.class);
        builder.add(Jailor.class);
        builder.add(Doctor.class);
        builder.add(Escort.class);
        builder.add(Medium.class);
        builder.add(RoleAlignment.TownKilling);
        builder.add(RoleAlignment.TownAny);

        builder.add(Godfather.class);
        builder.add(Mafioso.class);
        builder.add(Framer.class);

        builder.add(SerialKiller.class);
        builder.add(Executioner.class);
        builder.add(Jester.class);

        return builder.toRoleList();
    }
    // End

    // Classic Ranked
    /** Gets the Classic Ranked role list. */
    public static final RoleList ClassicRanked = generateClassicRanked();

    private static RoleList generateClassicRanked()
    {
        var builder = new RoleListBuilder();

        builder.add(Jailor.class);
        builder.add(2, RoleAlignment.TownInvestigative);
        builder.add(RoleAlignment.TownProtective);
        builder.add(RoleAlignment.TownKilling);
        builder.add(RoleAlignment.TownSupport);
        builder.add(3, RoleAlignment.TownAny);

        builder.add(Godfather.class);
        builder.add(Mafioso.class);
        builder.add(2, RoleAlignment.MafiaAny);

        builder.add(Executioner.class);
        builder.add(Witch.class);

        return builder.toRoleList();
    }
    // End

    // Rainbow
    /** Gets the Rainbow role list. */
    public static final RoleList Rainbow = generateRainbow();

    private static RoleList generateRainbow()
    {
        var builder = new RoleListBuilder();

        builder.add(Godfather.class);
        builder.add(Arsonist.class);
        builder.add(Survivor.class);
        builder.add(Jailor.class);
        builder.add(Amnesiac.class);
        builder.add(SerialKiller.class);
        builder.add(Witch.class);

        builder.add(RoleAlignment.Any);

        var slots = builder.roleSlots;
        int offset = slots.size() - 2;
        for (int i = 0; i < 7; i++)
            slots.add(slots.get(offset - i));

        return builder.toRoleList();
    }
    // End

    // Dracula's Palace
    /** Gets the Dracula's Palace role list. */
    public static final RoleList DraculasPalace = generateDraculasPalace();

    private static RoleList generateDraculasPalace()
    {
        var builder = new RoleListBuilder();

        builder.add(Doctor.class);
        builder.add(2, Lookout.class);
        builder.add(Jailor.class);
        builder.add(Vigilante.class);
        builder.add(RoleAlignment.TownProtective);
        builder.add(2, RoleAlignment.TownSupport);
        builder.add(VampireHunter.class);

        builder.add(Jester.class);
        builder.add(Witch.class);

        builder.add(4, Vampire.class);

        return builder.toRoleList();
    }
    // End

    // Town Traitor
    /** Gets the Town Traitor role list. */
    public static final RoleList TownTraitor = generateTownTraitor();

    private static RoleList generateTownTraitor()
    {
        var builder = new RoleListBuilder();

        builder.add(Sheriff.class);
        builder.add(Jailor.class);
        builder.add(Doctor.class);
        builder.add(Lookout.class);
        builder.add(RoleAlignment.TownInvestigative);
        builder.add(RoleAlignment.TownProtective);
        builder.add(RoleAlignment.TownKilling);
        builder.add(RoleAlignment.TownSupport);
        builder.add(3, RoleAlignment.TownAny);

        builder.add(Godfather.class);
        builder.add(Mafioso.class);
        builder.add(RoleAlignment.MafiaAny);

        builder.add(Witch.class);

        return builder.toRoleList();
    }
    // End
    // End

    // Coven
    // Coven Classic
    /** Gets the Coven Classic role list. */
    public static final RoleList CovenClassic = generateCovenClassic();

    private static RoleList generateCovenClassic()
    {
        var builder = new RoleListBuilder();

        builder.add(Sheriff.class);
        builder.add(Lookout.class);
        builder.add(Psychic.class);
        builder.add(Jailor.class);
        builder.add(RoleAlignment.TownProtective);
        builder.add(CovenLeader.class);
        builder.add(PotionMaster.class);
        builder.add(Executioner.class);
        builder.add(RoleAlignment.CovenAny);
        builder.add(Medusa.class);
        builder.add(RoleAlignment.TownAny);
        builder.add(Plaguebearer.class);
        builder.add(RoleAlignment.TownAny);
        builder.add(Pirate.class);
        builder.add(RoleAlignment.TownAny);

        return builder.toRoleList();
    }
    // End

    // Coven Ranked
    /** Gets the Coven Ranked role list. */
    public static final RoleList CovenRanked = generateCovenRanked();

    private static RoleList generateCovenRanked()
    {
        var builder = new RoleListBuilder();

        builder.add(Jailor.class);
        builder.add(2, RoleAlignment.TownInvestigative);
        builder.add(RoleAlignment.TownProtective);
        builder.add(RoleAlignment.TownKilling);
        builder.add(RoleAlignment.TownSupport);
        builder.add(3, RoleAlignment.TownAny);

        builder.add(CovenLeader.class);
        builder.add(Medusa.class);
        builder.add(2, RoleAlignment.CovenAny);

        builder.add(RoleAlignment.NeutralKilling);
        builder.add(RoleAlignment.NeutralEvil);

        return builder.toRoleList();
    }
    // End

    // Mafia Returns
    /** Gets the Mafia Returns role list. */
    public static final RoleList MafiaReturns = generateMafiaReturns();

    private static RoleList generateMafiaReturns()
    {
        var builder = new RoleListBuilder();

        builder.add(Sheriff.class);
        builder.add(Lookout.class);
        builder.add(Psychic.class);
        builder.add(Jailor.class);
        builder.add(RoleAlignment.TownProtective);

        builder.add(Godfather.class);
        builder.add(Ambusher.class);
        builder.add(RoleAlignment.MafiaAny);
        builder.add(Hypnotist.class);

        builder.add(Executioner.class);
        builder.add(Plaguebearer.class);
        builder.add(Pirate.class);

        builder.add(3, RoleAlignment.TownAny);

        return builder.toRoleList();
    }
    // End

    // Coven VIP
    /** Gets the Coven VIP role list. */
    public static final RoleList CovenVIP = generateCovenVIP();

    private static RoleList generateCovenVIP()
    {
        var builder = new RoleListBuilder();

        builder.add(Sheriff.class);
        builder.add(Crusader.class);
        builder.add(Psychic.class);
        builder.add(Vigilante.class);
        builder.add(Trapper.class);
        builder.add(CovenLeader.class);
        builder.add(PotionMaster.class);
        builder.add(GuardianAngel.class);
        builder.add(RoleAlignment.CovenAny);
        builder.add(Medusa.class);
        builder.add(Tracker.class);
        builder.add(RoleAlignment.TownProtective);
        builder.add(RoleAlignment.TownSupport);
        builder.add(Pirate.class);
        builder.add(RoleAlignment.TownProtective);

        return builder.toRoleList();
    }
    // End

    // Coven Lovers
    /** Gets the Coven Lovers role list. */
    public static final RoleList CovenLovers = generateCovenLovers();

    private static RoleList generateCovenLovers()
    {
        var builder = new RoleListBuilder();

        builder.add(Sheriff.class);
        builder.add(Doctor.class);
        builder.add(Psychic.class);
        builder.add(Tracker.class);
        builder.add(2, RoleAlignment.TownProtective);
        builder.add(2, RoleAlignment.TownSupport);

        builder.add(Pirate.class);
        builder.add(Arsonist.class);
        builder.add(Werewolf.class);
        builder.add(SerialKiller.class);
        builder.add(Godfather.class);
        builder.add(Medusa.class);
        builder.add(Plaguebearer.class);

        return builder.toRoleList();
    }
    // End

    // Coven Town Traitor
    /** Gets the Coven Town Traitor role list. */
    public static final RoleList CovenTownTraitor = generateCovenTownTraitor();

    private static RoleList generateCovenTownTraitor()
    {
        var builder = new RoleListBuilder();

        builder.add(Sheriff.class);
        builder.add(Jailor.class);
        builder.add(Crusader.class);
        builder.add(Tracker.class);
        builder.add(RoleAlignment.TownInvestigative);
        builder.add(RoleAlignment.TownProtective);
        builder.add(RoleAlignment.TownKilling);
        builder.add(RoleAlignment.TownSupport);
        builder.add(3, RoleAlignment.TownAny);

        builder.add(CovenLeader.class);
        builder.add(Medusa.class);
        builder.add(2, RoleAlignment.CovenAny);

        return builder.toRoleList();
    }
    // End
    // End
}
