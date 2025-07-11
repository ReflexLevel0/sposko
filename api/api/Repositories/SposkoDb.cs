// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 1573, 1591
#nullable enable

namespace sposko
{
    public partial class SposkoDb : DataConnection, ISposkoDb
    {
        public SposkoDb()
        {
            InitDataContext();
        }

        public SposkoDb(string configuration)
            : base(configuration)
        {
            InitDataContext();
        }

        public SposkoDb(DataOptions<SposkoDb> options)
            : base(options.Options)
        {
            InitDataContext();
        }

        partial void InitDataContext();

        public ITable<Administrator> Administrators => this.GetTable<Administrator>();
        public ITable<ChildGroup> ChildGroups => this.GetTable<ChildGroup>();
        public ITable<Child> Children => this.GetTable<Child>();
        public ITable<Note> Notes => this.GetTable<Note>();
        public ITable<Parent> Parents => this.GetTable<Parent>();
        public ITable<SportGroup> SportGroups => this.GetTable<SportGroup>();
        public ITable<SportTraining> SportTrainings => this.GetTable<SportTraining>();
        public ITable<Sport> Sports => this.GetTable<Sport>();
        public ITable<Trainer> Trainers => this.GetTable<Trainer>();
        public ITable<TrainingException> TrainingExceptions => this.GetTable<TrainingException>();
        public ITable<User> Users => this.GetTable<User>();
    }

    public static partial class ExtensionMethods
    {
        #region Table Extensions
        public static Administrator? Find(this ITable<Administrator> table, Guid id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<Administrator?> FindAsync(this ITable<Administrator> table, Guid id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public static ChildGroup? Find(this ITable<ChildGroup> table, Guid childId, int groupId)
        {
            return table.FirstOrDefault(e => e.ChildId == childId && e.GroupId == groupId);
        }

        public static Task<ChildGroup?> FindAsync(this ITable<ChildGroup> table, Guid childId, int groupId, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.ChildId == childId && e.GroupId == groupId, cancellationToken);
        }

        public static Child? Find(this ITable<Child> table, Guid id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<Child?> FindAsync(this ITable<Child> table, Guid id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public static Note? Find(this ITable<Note> table, int id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<Note?> FindAsync(this ITable<Note> table, int id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public static Parent? Find(this ITable<Parent> table, Guid id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<Parent?> FindAsync(this ITable<Parent> table, Guid id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public static SportGroup? Find(this ITable<SportGroup> table, int id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<SportGroup?> FindAsync(this ITable<SportGroup> table, int id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public static SportTraining? Find(this ITable<SportTraining> table, int id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<SportTraining?> FindAsync(this ITable<SportTraining> table, int id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public static Sport? Find(this ITable<Sport> table, int id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<Sport?> FindAsync(this ITable<Sport> table, int id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public static Trainer? Find(this ITable<Trainer> table, Guid id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<Trainer?> FindAsync(this ITable<Trainer> table, Guid id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public static TrainingException? Find(this ITable<TrainingException> table, int trainingId, DateTime exceptionDate)
        {
            return table.FirstOrDefault(e => e.TrainingId == trainingId && e.ExceptionDate == exceptionDate);
        }

        public static Task<TrainingException?> FindAsync(this ITable<TrainingException> table, int trainingId, DateTime exceptionDate, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.TrainingId == trainingId && e.ExceptionDate == exceptionDate, cancellationToken);
        }

        public static User? Find(this ITable<User> table, Guid id)
        {
            return table.FirstOrDefault(e => e.Id == id);
        }

        public static Task<User?> FindAsync(this ITable<User> table, Guid id, CancellationToken cancellationToken = default)
        {
            return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
        #endregion
    }
}
