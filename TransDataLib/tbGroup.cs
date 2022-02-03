using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;


namespace TransDataLib
{
    /// <summary>
    /// Structure that contains all data from the last query separated by categories;
    /// </summary>
    public class DataBase
    {
        public List<tbGroup> Groups;

        public DataBase()
        {
            Groups = new List<tbGroup>();
        }

        /// <summary>
        /// Вывод в консоль всей информации о данном экземпляре
        /// </summary>
        public void PrintConsole()
        {
            foreach (var group in Groups)
            {
                Console.WriteLine($"===== VM: {group.pgH3} ==== GROUP {group.refNo} ==== LOT : {group.pgH2} ======== ");
                //Console.WriteLine($"Group packs : {group.Packages.Count}");
                foreach (var pack in group.Packages)
                {
                    //Console.WriteLine($"--------- PACKAGE {pack.PackNo} total results: {pack.Values.Count} ---------------");
                    foreach (var value in pack.Values)
                    {
                      // Console.WriteLine($"        refNo: {value.refNo} packNo: {value.PackNo} Key: {value._Key} X: {value.X}");
                    }
                    //Console.WriteLine("----------------------------------------------------");
                }
               // Console.WriteLine("====================================================");
            }
        }
    }

    // --- ROWS DEFENITION

    /// <summary>
    /// Simple row of tbGroup table
    /// </summary>
    public class tbGroup
    {
        public virtual int grpIndex { get; set; }
        public virtual int refNo { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string pgH1 { get; set; }
        public virtual int pgH2 { get; set; }
        public virtual int pgH3 { get; set; }
        public virtual string pgH4 { get; set; }
        public virtual string pgH5 { get; set; }
        public virtual string pgH6 { get; set; }
        public virtual string pgH7 { get; set; }
        public virtual string pgH8 { get; set; }
        public virtual string pgH9 { get; set; }
        public virtual string MethodGroup { get; set; }
        public virtual string MethodName { get; set; }
        public virtual int PacksAmount { get; set; }
        public virtual int IterationsScheduled { get; set; }

        public virtual IList<tbPackages> Packages { get; set; }

        public virtual int LowerThenCount(int lower)
        {
            int count = 0;
            foreach (var pack in Packages)
            {
                foreach (var value in pack.Values)
                {
                    if (value._Key == "TEN_M3" && value.X < lower)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public virtual bool CompareWith(tbGroup Group)
        {
            bool isEqual = true;
            if (this.refNo != Group.refNo) isEqual = false;
            if (this.Packages.Count != Group.Packages.Count) isEqual = false;
            else
            {
                for (int i = 0; i < this.Packages.Count; i++)
                {
                    if (this.Packages[i].Values.Count != Group.Packages[i].Values.Count) isEqual = false;
                }
            }

            return isEqual;
        }
    }

    /// <summary>
    /// Simple row of tbPackages table
    /// </summary>
    public class tbPackages
    {
        public virtual int ID { get; set; }
        public virtual tbGroup Owner { get; set; }
        /// <summary>
        /// Reference kay to Group
        /// </summary>
        public virtual int refNo { get; set; }
        /// <summary>
        /// Reference key to Package info
        /// </summary>
        public virtual int PackNo { get; set; }
        /// <summary>
        /// Linear Dencity of the cord on this pack
        /// </summary>
        public virtual decimal PackLD { get; set; }
        /// <summary>
        /// Custom lable for pack
        /// </summary>
        public virtual string Label { get; set; }
        public virtual string Status { get; set; }
        /// <summary>
        /// Reference key to main ID key
        /// </summary>
        public virtual int grpIndex { get; set; }
        /// <summary>
        /// Measurement results of this pack
        /// </summary>
        public virtual IList<tbPackageValues> Values { get; set; }

        public override string ToString()
        {
            return $"RefNo:{refNo} PackNo:{PackNo} PackLD:{PackLD} Label:{Label} ID:{grpIndex} Status:{Status}";
        }

    }
   
    /// <summary>
    /// Simple row of tbPackageValues table
    /// </summary>
    public class tbPackageValues
    {
        public virtual int ID { get; set; }
        public virtual tbPackages Owner { get; set; }
        public virtual int refNo { get; set; }
        public virtual int PackNo { get; set; }
        /// <summary>
        /// Meas type key
        /// </summary>
        public virtual string _Key { get; set; }
        public virtual int MethodNumber { get; set; }
        public virtual string Method { get; set; }
        public virtual int grpIndex { get; set; }
        public virtual decimal X { get; set; }
        public virtual decimal Min { get; set; }
        public virtual decimal Max { get; set; }
        public virtual decimal S { get; set; }
        public virtual decimal V { get; set; }
        public virtual decimal Q { get; set; }
        public virtual decimal R { get; set; }

        public override string ToString()
        {
            return $"refNo:{refNo} ID:{grpIndex} PackNo:{PackNo} Key:{_Key} MethodNumber{MethodNumber} Method:{Method} X:{X} Min:{Min} Max:{Max} S:{S} V:{V} Q:{Q} R:{R}";
        }

    }

    public class LowFiltered
    {
        public virtual DateTime Date { get; set; }
        public virtual int packNo { get; set; }
        public virtual int pgH2 { get; set; }
        public virtual int pgH3 { get; set; }
        public virtual string Label { get; set; }
        public virtual decimal X { get; set; }
    }

    #region Mappings

    public class tbGroupMap : ClassMap<tbGroup>
    {
        public tbGroupMap()
        {
            Map(x => x.grpIndex);
            Id(x => x.refNo)
                .GeneratedBy
                .Assigned();
            Map(x => x.Date);
            Map(x => x.pgH1);
            Map(x => x.pgH2);
            Map(x => x.pgH3);
            Map(x => x.pgH4);
            Map(x => x.pgH5);
            Map(x => x.pgH6);
            Map(x => x.pgH7);
            Map(x => x.pgH8);
            Map(x => x.pgH9);
            Map(x => x.MethodGroup);
            Map(x => x.MethodName);
            Map(x => x.PacksAmount);
            Map(x => x.IterationsScheduled);
            HasMany(x => x.Packages)
                .Cascade
                .All();
            Join("tbPackages", m =>
            {
                m.Fetch.Join();
                m.KeyColumn("refNo");
            });
        }
    }
    
    public class tbPackagesMap : ClassMap<tbPackages>
    {
        public tbPackagesMap()
        {
            Map(x => x.refNo);
            Map(x => x.PackNo);
            Map(x => x.PackLD);
            Map(x => x.Label);
            Map(x => x.Status);
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.grpIndex);
            References(x => x.Owner)
                .Cascade.All();
            HasMany(x => x.Values)
                .Cascade.All();
            Join("tbPackageValues", m =>
            {
                m.Fetch.Join();
                m.KeyColumn("refNo");
            });
        }
    }

    public class tbPackageValuesMap : ClassMap<tbPackageValues>
    {
        public tbPackageValuesMap()
        {
            References(x => x.Owner).Cascade.All();
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.grpIndex);
            Map(x => x.refNo);
            Map(x => x.PackNo);
            Map(x => x._Key);
            Map(x => x.MethodNumber);
            Map(x => x.Method);
            Map(x => x.X);
            Map(x => x.Min);
            Map(x => x.Max);
            Map(x => x.S);
            Map(x => x.V);
            Map(x => x.Q);
            Map(x => x.R);
        }
    }

    #endregion



}
