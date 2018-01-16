
using System.Diagnostics;

public class Genetique
{
    private Action[] tireur;
    private Action[] eclaireur;

    private float idxCrossover, idxMutation;

    public Action[] getActionsTireur()
    {
        return tireur;
    }

    public Action[] getActionsEclaireur()
    {
        return eclaireur;
    }

    public Genetique(int tailleADN, float indiceCrossover, float indiceMutation)
    {
        tireur = new Action[tailleADN];
        for(int i = 0; i < tailleADN; i ++)
        {
            tireur[i] = Action.actionAleatoire(true);
        }

        eclaireur = new Action[tailleADN];
        for (int i = 0; i < tailleADN; i++)
        {
            eclaireur[i] = Action.actionAleatoire(false);
        }

        idxCrossover = indiceCrossover;
        idxMutation = indiceMutation;
    }

    public void makeNextGeneration()
    {

    }

    public void crossover()
    {

    }

    public void mutate()
    {

    } 
}
