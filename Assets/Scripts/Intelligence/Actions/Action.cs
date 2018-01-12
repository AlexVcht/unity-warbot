
public interface ActionGame
{
    void execute();
}

public interface ActionGenetique
{
    void mutate();
}

public abstract class Action : ActionGame, ActionGenetique
{
    protected long duree;

    public Action(long p_duree)
    {
        this.duree = p_duree;
    }

    public abstract void execute();
    public abstract void mutate();

    public long getDuree()
    {
        return duree;
    }
    
    public void setDuree(long p_duree)
    {
        duree = p_duree;
    }
}
