// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;
using System;

#pragma warning disable 1573, 1591
#nullable enable

namespace sposko
{
    [Table("administrators")]
    public class Administrator
    {
        [Column("id", IsPrimaryKey = true)] public Guid Id { get; set; } // uuid

        #region Associations
        /// <summary>
        /// administrators_id_fkey
        /// </summary>
        [Association(CanBeNull = false, ThisKey = nameof(Id), OtherKey = nameof(User.Id))]
        public User Idfkey { get; set; } = null!;
        #endregion
    }
}
