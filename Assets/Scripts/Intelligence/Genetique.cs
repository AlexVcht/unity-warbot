
public class Genetique
{
    private Action[] tireur;
    private Action[] eclaireur;

    private float idxCrossover, idxMutation;

    public Genetique(int tailleADN, float indiceCrossover, float indiceMutation)
    {
        tireur = new Action[tailleADN];
        for(int i = 0; i < tailleADN; i ++)
        {
            tireur[i] = Action.actionAleatoire();
        }

        eclaireur = new Action[tailleADN];
        for (int i = 0; i < tailleADN; i++)
        {
            eclaireur[i] = Action.actionAleatoire();
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
