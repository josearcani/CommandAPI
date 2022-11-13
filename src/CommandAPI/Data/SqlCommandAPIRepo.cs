using CommandAPI.Models;

namespace CommandAPI.Data;

public class SqlCommandAPIRepo : ICommandAPIRepo
{
    private readonly CommandContext _context;
    public SqlCommandAPIRepo(CommandContext context)
    {
        _context = context;
    }

    public void CreateCommand(Command cmd)
    {
        if (cmd is null)
        {
            throw new ArgumentException(nameof(cmd));
        }
        _context.CommandItems.Add(cmd);
        this.SaveChanges();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands()
    {
        return _context.CommandItems.ToList();
    }

    public Command? GetCommandById(int id)
    {
        return _context.CommandItems.FirstOrDefault(c => c.Id == id);
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public void UpdateCommand(Command cmd)
    {
        // not necessary to implement, the mapper takes care of it
    }
}