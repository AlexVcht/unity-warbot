
public interface ActionGame
{
    void execute(Connaissances connaissances, int x, int y);
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

    public abstract void execute(Connaissances connaissances, int x, int y);
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
