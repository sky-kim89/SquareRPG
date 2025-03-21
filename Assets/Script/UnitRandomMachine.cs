namespace MyProjeckt
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    public class UnitRandomMachine
    {

        static public UnitData GetUnitData(string name)
        {
            UnitData data = new UnitData();
            data.Name = name;

            int seed = NameToSeed(data.Name);

            Random.InitState(seed);
            data.Weapon = (eWeaponType)Random.Range((int)eWeaponType.Sword, (int)eWeaponType.Last);
            int index = Random.Range(1, (int)eGradeType.Last);
            data.Grade = (eGradeType)index;
            switch(data.Weapon)
            {
                case eWeaponType.Bow:
                    data.AttackRange = Random.Range(10f, 12f); ;
                    break;
                case eWeaponType.Wand:
                    data.AttackRange = Random.Range(6f, 8f); ;
                    break;
                default:
                    data.AttackRange = 2f;
                    break;
            }
            SettingStats(data);

            index = Random.Range(0, Table.BobyColors.Length);
            data.UnitColors[0] = Table.BobyColors[index];

            index = Random.Range(0, Table.HeadColors.Length);
            data.UnitColors[1] = Table.HeadColors[index];

            index = Random.Range(0, Table.HairColors.Length);
            data.UnitColors[2] = Table.HairColors[index];

            index = Random.Range(0, Table.EyeRColors.Length);
            data.UnitColors[3] = Table.EyeRColors[index];
            data.UnitColors[4] = Table.EyeLColors[index];

            data.Skills = new Skill[2];
            data.Skills[0] = SkillManager.Instance.GetRandomPassiveSkill();
            data.Skills[1] = SkillManager.Instance.GetRandomActiveSkill();
            return data;
        }

        static public UnitData GetUnitData(HeroSaveData hero)
        {
            UnitData data = GetUnitData(hero.Name);
            data.AddUnitCount = hero.AddUnitCount;
            data.AddLevel = hero.AddLevel;
            data.UpGrade = hero.UpGrade;
            return data;
        }


        static public UnitData NewUnitData()
        {
            Random.InitState(Random.Range(int.MinValue, int.MaxValue));

            int index = Random.Range(0, Table.NameTables.Length);
            index = index - index % 3;
            string name = Table.NameTables[index];

            return GetUnitData(name);
        }

        static private void SettingStats(UnitData data)
        {
            float total = 20;
            int max = 10 + (int)data.Grade * 2 + 1;
            int min = (int)data.Grade;

            data.AP = Random.Range(min, max);
            total = total - data.AP + min;

            if (total < max)
                max = (int)total;

            data.HP = Random.Range(min, max);
            total = total - data.HP + min;

            if (total < max)
                max = (int)total;

            data.SP = Random.Range(min, max);
            total = total - data.SP + min;

            data.LP = total;
        }

        static private int NameToSeed(string name)
        {
            int seed = 0;
            int prev = 0;

            foreach (int i in name)
            {
                if (prev == 0)
                    seed += i;
                else
                    seed += prev * i + prev + i;

                prev = i;
            }
            return seed;
        }
    }
}
