using System;

public class Genetique
{
    private Action[] tireur;
    private Action[] eclaireur;

    private float iMutateSoft;
    private double iMutateHard;

    public Action[] getActionsTireur()
    {
        return tireur;
    }

    public Action[] getActionsEclaireur()
    {
        return eclaireur;
    }

    public Genetique(int tailleADN, float indiceMutation)
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

        iMutateSoft = indiceMutation;
        iMutateHard = Math.Sqrt(iMutateSoft);
    }

    public void makeNextGeneration()
    {
        
    }

    public void crossover(Action[] adn1, Action[] adn2)
    {
        int iCrossover = (int)UnityEngine.Random.Range(0, adn1.Length-1);
        Action[] adnTMP = new Action[adn2.Length - iCrossover];
        Array.Copy(adn2, adn2.Length - iCrossover - 1, adnTMP, 0, adn2.Length - iCrossover);
        Array.Copy(adn1, iCrossover + 1, adn2, iCrossover + 1, adn2.Length - iCrossover);
        Array.Copy(adnTMP, 0, adn1, iCrossover + 1, adn2.Length - iCrossover);
    }

    public void mutate(Action[] adn, bool isTank)
    {
        for (int i = 0; i<adn.Length; i++)
        {
            float r = UnityEngine.Random.Range(0, 1);
            if(r < iMutateHard)
            {
                adn[i] = Action.actionAleatoire(isTank);
            }
            if (r < iMutateSoft)
            {
                adn[i].mutate(iMutateSoft);
            }
        }
    } 
}
