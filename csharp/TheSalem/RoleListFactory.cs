using System;

namespace TheSalem
{
    /// <summary>Provides standard role lists and tools to generate new ones.</summary>
    public static class RoleListFactory
    {
        /// <summary>Gets the All Any role list.</summary>
        public static readonly RoleList AllAny = GenerateAllAny();

        private static RoleList GenerateAllAny()
        {
            var slots = new IRoleSlot[15];
            Array.Fill(slots, RoleAlignment.Any);
            return new(slots);
        }

        #region Classic
        #region Classic
        /// <summary>Gets the Classic role list.</summary>
        public static readonly RoleList Classic = GenerateClassic();

        private static RoleList GenerateClassic()
        {
            var builder = new RoleListBuilder();

            builder.Add<Sheriff>();
            builder.Add<Lookout>();
            builder.Add<Investigator>();
            builder.Add<Jailor>();
            builder.Add<Doctor>();
            builder.Add<Escort>();
            builder.Add<Medium>();
            builder.Add(RoleAlignment.TownKilling);
            builder.Add(RoleAlignment.TownAny);

            builder.Add<Godfather>();
            builder.Add<Mafioso>();
            builder.Add<Framer>();

            builder.Add<SerialKiller>();
            builder.Add<Executioner>();
            builder.Add<Jester>();

            return builder.ToRoleList();
        }
        #endregion

        #region Classic Ranked
        /// <summary>Gets the Classic Ranked role list.</summary>
        public static readonly RoleList ClassicRanked = GenerateClassicRanked();

        private static RoleList GenerateClassicRanked()
        {
            var builder = new RoleListBuilder();

            builder.Add<Jailor>();
            builder.Add(2, RoleAlignment.TownInvestigative);
            builder.Add(RoleAlignment.TownProtective);
            builder.Add(RoleAlignment.TownKilling);
            builder.Add(RoleAlignment.TownSupport);
            builder.Add(3, RoleAlignment.TownAny);

            builder.Add<Godfather>();
            builder.Add<Mafioso>();
            builder.Add(2, RoleAlignment.MafiaAny);

            builder.Add<Executioner>();
            builder.Add<Witch>();

            return builder.ToRoleList();
        }
        #endregion

        #region Rainbow
        /// <summary>Gets the Rainbow role list.</summary>
        public static readonly RoleList Rainbow = GenerateRainbow();

        private static RoleList GenerateRainbow()
        {
            var builder = new RoleListBuilder();

            builder.Add<Godfather>();
            builder.Add<Arsonist>();
            builder.Add<Survivor>();
            builder.Add<Jailor>();
            builder.Add<Amnesiac>();
            builder.Add<SerialKiller>();
            builder.Add<Witch>();

            builder.Add(RoleAlignment.Any);

            for (int i = 0; i < 7; i++)
                builder.RoleSlots.Add(builder.RoleSlots[^(i + 2)]);

            return builder.ToRoleList();
        }
        #endregion

        #region Dracula's Palace
        /// <summary>Gets the Dracula's Palace role list.</summary>
        public static readonly RoleList DraculasPalace = GenerateDraculasPalace();

        private static RoleList GenerateDraculasPalace()
        {
            var builder = new RoleListBuilder();

            builder.Add<Doctor>();
            builder.Add<Lookout>(2);
            builder.Add<Jailor>();
            builder.Add<Vigilante>();
            builder.Add(RoleAlignment.TownProtective);
            builder.Add(2, RoleAlignment.TownSupport);
            builder.Add<VampireHunter>();

            builder.Add<Jester>();
            builder.Add<Witch>();

            builder.Add<Vampire>(4);
            
            return builder.ToRoleList();
        }
        #endregion

        #region Town Traitor
        /// <summary>Gets the Town Traitor role list.</summary>
        public static readonly RoleList TownTraitor = GenerateTownTraitor();

        private static RoleList GenerateTownTraitor()
        {
            var builder = new RoleListBuilder();

            builder.Add<Sheriff>();
            builder.Add<Jailor>();
            builder.Add<Doctor>();
            builder.Add<Lookout>();
            builder.Add(RoleAlignment.TownInvestigative);
            builder.Add(RoleAlignment.TownProtective);
            builder.Add(RoleAlignment.TownKilling);
            builder.Add(RoleAlignment.TownSupport);
            builder.Add(3, RoleAlignment.TownAny);

            builder.Add<Godfather>();
            builder.Add<Mafioso>();
            builder.Add(RoleAlignment.MafiaAny);

            builder.Add<Witch>();

            return builder.ToRoleList();
        }
        #endregion
        #endregion

        #region Coven
        #region Coven Classic
        /// <summary>Gets the Coven Classic role list.</summary>
        public static readonly RoleList CovenClassic = GenerateCovenClassic();

        private static RoleList GenerateCovenClassic()
        {
            var builder = new RoleListBuilder();

            builder.Add<Sheriff>();
            builder.Add<Lookout>();
            builder.Add<Psychic>();
            builder.Add<Jailor>();
            builder.Add(RoleAlignment.TownProtective);
            builder.Add<CovenLeader>();
            builder.Add<PotionMaster>();
            builder.Add<Executioner>();
            builder.Add(RoleAlignment.CovenAny);
            builder.Add<Medusa>();
            builder.Add(RoleAlignment.TownAny);
            builder.Add<Plaguebearer>();
            builder.Add(RoleAlignment.TownAny);
            builder.Add<Pirate>();
            builder.Add(RoleAlignment.TownAny);

            return builder.ToRoleList();
        }
        #endregion

        #region Coven Ranked
        /// <summary>Gets the Coven Ranked role list.</summary>
        public static readonly RoleList CovenRanked = GenerateCovenRanked();

        private static RoleList GenerateCovenRanked()
        {
            var builder = new RoleListBuilder();

            builder.Add<Jailor>();
            builder.Add(2, RoleAlignment.TownInvestigative);
            builder.Add(RoleAlignment.TownProtective);
            builder.Add(RoleAlignment.TownKilling);
            builder.Add(RoleAlignment.TownSupport);
            builder.Add(3, RoleAlignment.TownAny);

            builder.Add<CovenLeader>();
            builder.Add<Medusa>();
            builder.Add(2, RoleAlignment.CovenAny);

            builder.Add(RoleAlignment.NeutralKilling);
            builder.Add(RoleAlignment.NeutralEvil);

            return builder.ToRoleList();
        }
        #endregion

        #region Mafia Returns
        /// <summary>Gets the Mafia Returns role list.</summary>
        public static readonly RoleList MafiaReturns = GenerateMafiaReturns();

        private static RoleList GenerateMafiaReturns()
        {
            var builder = new RoleListBuilder();

            builder.Add<Sheriff>();
            builder.Add<Lookout>();
            builder.Add<Psychic>();
            builder.Add<Jailor>();
            builder.Add(RoleAlignment.TownProtective);

            builder.Add<Godfather>();
            builder.Add<Ambusher>();
            builder.Add(RoleAlignment.MafiaAny);
            builder.Add<Hypnotist>();

            builder.Add<Executioner>();
            builder.Add<Plaguebearer>();
            builder.Add<Pirate>();

            builder.Add(3, RoleAlignment.TownAny);

            return builder.ToRoleList();
        }
        #endregion

        #region Coven VIP
        /// <summary>Gets the Coven VIP role list.</summary>
        public static readonly RoleList CovenVIP = GenerateCovenVIP();

        private static RoleList GenerateCovenVIP()
        {
            var builder = new RoleListBuilder();

            builder.Add<Sheriff>();
            builder.Add<Crusader>();
            builder.Add<Psychic>();
            builder.Add<Vigilante>();
            builder.Add<Trapper>();
            builder.Add<CovenLeader>();
            builder.Add<PotionMaster>();
            builder.Add<GuardianAngel>();
            builder.Add(RoleAlignment.CovenAny);
            builder.Add<Medusa>();
            builder.Add<Tracker>();
            builder.Add(RoleAlignment.TownProtective);
            builder.Add(RoleAlignment.TownSupport);
            builder.Add<Pirate>();
            builder.Add(RoleAlignment.TownProtective);

            return builder.ToRoleList();
        }
        #endregion

        #region Coven Lovers
        /// <summary>Gets the Coven Lovers role list.</summary>
        public static readonly RoleList CovenLovers = GenerateCovenLovers();

        private static RoleList GenerateCovenLovers()
        {
            var builder = new RoleListBuilder();

            builder.Add<Sheriff>();
            builder.Add<Doctor>();
            builder.Add<Psychic>();
            builder.Add<Tracker>();
            builder.Add(2, RoleAlignment.TownProtective);
            builder.Add(2, RoleAlignment.TownSupport);

            builder.Add<Pirate>();
            builder.Add<Arsonist>();
            builder.Add<Werewolf>();
            builder.Add<SerialKiller>();
            builder.Add<Godfather>();
            builder.Add<Medusa>();
            builder.Add<Plaguebearer>();

            return builder.ToRoleList();
        }
        #endregion

        #region Coven Town Traitor
        /// <summary>Gets the Coven Town Traitor role list.</summary>
        public static readonly RoleList CovenTownTraitor = GenerateCovenTownTraitor();

        private static RoleList GenerateCovenTownTraitor()
        {
            var builder = new RoleListBuilder();

            builder.Add<Sheriff>();
            builder.Add<Jailor>();
            builder.Add<Crusader>();
            builder.Add<Tracker>();
            builder.Add(RoleAlignment.TownInvestigative);
            builder.Add(RoleAlignment.TownProtective);
            builder.Add(RoleAlignment.TownKilling);
            builder.Add(RoleAlignment.TownSupport);
            builder.Add(3, RoleAlignment.TownAny);

            builder.Add<CovenLeader>();
            builder.Add<Medusa>();
            builder.Add(2, RoleAlignment.CovenAny);

            return builder.ToRoleList();
        }
        #endregion
        #endregion
    }
}
