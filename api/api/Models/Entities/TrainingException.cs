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
    [Table("training_exceptions")]
    public class TrainingException
    {
        [Column("training_id", IsPrimaryKey = true, PrimaryKeyOrder = 0, IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public int TrainingId { get; set; } // integer
        [Column("exception_date", IsPrimaryKey = true, PrimaryKeyOrder = 1)] public DateTime ExceptionDate { get; set; } // date

        #region Associations
        /// <summary>
        /// training_exceptions_training_id_fkey
        /// </summary>
        [Association(CanBeNull = false, ThisKey = nameof(TrainingId), OtherKey = nameof(SportTraining.Id))]
        public SportTraining Trainingexceptionstrainingidfkey { get; set; } = null!;
        #endregion
    }
}
