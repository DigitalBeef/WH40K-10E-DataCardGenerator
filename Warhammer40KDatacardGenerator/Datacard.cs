using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warhammer40KDatacardGenerator
{ 
        struct ModelStats
        {
            public string 
                movement,
                toughness, 
                armor, 
                wounds, 
                leadership, 
                objectiveControl,
                invuln;
        }
        struct Weapon 
        {
            public bool isMelee;

            public string 
                name = "",
                range = "", 
                attacks = "", 
                skill = "", 
                strength = "", 
                ap = "", 
                damage = "";

            public List<string> skills;

            public Weapon()
            {
                skills = new List<string>();
            }
        }

     class Datacard
    {
        public List<Weapon> RangedWeapons { get; private set; }
        public List<Weapon> MeleeWeapons { get; private set; }
        public ModelStats Stats { get; private set; }
        public List<String> Abilities { get; private set; }

        public Datacard() 
        {
            RangedWeapons = new List<Weapon>();
            MeleeWeapons = new List<Weapon>();
            Abilities = new List<string>();
        } 

        public void SetModelStats(ModelStats _stats)
        {
            Stats = _stats;
        }

        public void AddWeapon(Weapon _wep)
        {
            if (_wep.isMelee)
                MeleeWeapons.Add(_wep);
            else
            RangedWeapons.Add(_wep);
        }

        public void AddAbility(string _abl)
        {
            Abilities.Add(_abl);
        }

        public string GetStringForm()
        {
            string retval = ModelStatsAsString(Stats) + "\n";
            retval += GetWeaponsString() + "\n";
            if(Abilities.Count > 0)
                retval += GetAbilitiesString();
            return retval;
        }

        public string GetWeaponsString()
        {
            string retval = "";
            if(RangedWeapons.Count > 0)
            {
                retval += "[e85545]Ranged weapons[-]\n";
                foreach (var item in RangedWeapons)
                    retval += WeaponStatsAsString(item);
                retval += "\n";
            }
            if(MeleeWeapons.Count >0)
            {
                retval += "[e85545]Melee weapons[-]\n";
                foreach (var item in MeleeWeapons)
                    retval += WeaponStatsAsString(item);
            }
            return retval;
        }

        public string GetAbilitiesString()
        {
            string retval = "[dc61ed]Abilities[-]\n";
            for (int i = 0; i < Abilities.Count; i++)
            {
                if (i > 0)
                    retval += "\n";
                retval += " " + Abilities[i];
            }

            return retval;
        }

        public static string ModelStatsAsString(ModelStats _stats)
        {
            string retval = "";
            retval += "[56f442] M   T   Sv    W    Ld   OC  Inv [-]\n";

            if (_stats.movement.Length == 2)
                _stats.movement = " " + _stats.movement;
            string padding = "";
            for (int i = _stats.movement.Length; i <= 5; i++)
                padding += " ";

            retval +=
                _stats.movement + padding +
                _stats.toughness + "   " +
                _stats.armor + "    " +
                _stats.wounds + "      " +
                _stats.leadership + "   " +
                _stats.objectiveControl + "     " +
                _stats.invuln;

            return retval;
        }

        public static string WeaponStatsAsString(Weapon _stats)
        {
            string retval = "";
            string skillLabel;

            if (_stats.isMelee)
                skillLabel = " WS:";
            else
                skillLabel = " BS:";

            retval += " [c6c930]" + _stats.name + "[-]\n";
            retval += "  ";
            if (_stats.isMelee == false)
                retval += _stats.range + " ";

            retval +=
           "A:" + _stats.attacks +
           skillLabel + _stats.skill +
           " S:" + _stats.strength +
           " AP:" + _stats.ap +
           " D" + _stats.damage + "\n";

            if (_stats.skills.Count > 0)
            {
                retval += "  [7bc596][";
                for (int i = 0; i < _stats.skills.Count; i++)
                {
                    if (i > 0)
                        retval += ", ";

                    retval += _stats.skills[i]; 
                }
                retval += "][-]";
            }
            
            return retval;
        }
    }
}