using LinqToDB;
using LinqToDB.Data;

namespace sposko;

public interface ISposkoDb
{
    ITable<Administrator> Administrators { get; }
    ITable<ChildGroup> ChildGroups { get; }
    ITable<Child> Children { get; }
    ITable<Note> Notes { get; }
    ITable<Parent> Parents { get; }
    ITable<SportGroup> SportGroups { get; }
    ITable<SportTraining> SportTrainings { get; }
    ITable<Sport> Sports { get; }
    ITable<Trainer> Trainers { get; }
    ITable<TrainingException> TrainingExceptions { get; }
    ITable<User> Users { get; }
}
