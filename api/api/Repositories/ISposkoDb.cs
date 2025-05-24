using LinqToDB;
using LinqToDB.Data;

namespace sposko;

public interface ISposkoDb
{
    ITable<Administrator> Administrators { get; set; }
    ITable<ChildGroup> ChildGroups { get; set; }
    ITable<Child> Children { get; set; }
    ITable<Note> Notes { get; set; }
    ITable<Parent> Parents { get; set; }
    ITable<SportGroup> SportGroups { get; set; }
    ITable<SportTraining> SportTrainings { get; set; }
    ITable<Sport> Sports { get; set; }
    ITable<Trainer> Trainers { get; set; }
    ITable<TrainingException> TrainingExceptions { get; set; }
    ITable<User> Users { get; set; }
}
