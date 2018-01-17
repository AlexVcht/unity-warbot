
using System;

public class Squad : ActionGenetique
{
    public Action[] tireur;
    public Action[] eclaireur;
    public long score;

    public Squad(int tailleADN): this(tailleADN, new Action[0], new Action[0])
    {
        for (int a = 0; a < tailleADN; a++)
        {
            tireur[a] = Action.actionAleatoire(true);
            eclaireur[a] = Action.actionAleatoire(false);
        }
    }

    public Squad(int tailleADN, Action[] adnTireur, Action[] adnEclaireur)
    {
        score = 60 * 60 * 1000; // 60min * 60s * 1000ms => 60min en ms
        tireur = new Action[tailleADN];
        eclaireur = new Action[tailleADN];

        Array.Copy(adnTireur, tireur, adnTireur.Length);
        Array.Copy(adnEclaireur, eclaireur, adnEclaireur.Length);
    }

    public void mutate(float iMutation)
    {
        mutateADN(tireur, true, iMutation);
        mutateADN(eclaireur, false, iMutation);
    }

    public void mutateADN(Action[] adn, bool isTank, float iMutate)
    {
        for (int i = 0; i < adn.Length; i++)
        {
            float r = UnityEngine.Random.Range(0, 1);
            if (r < Math.Sqrt(iMutate))
            {
                adn[i] = Action.actionAleatoire(isTank);
            }
            if (r < iMutate)
            {
                adn[i].mutate(iMutate);
            }
        }
    }

    public static Squad crossover(Squad male, Squad female)
    {
        Action[] tireurChild = crossoverOneAgent(male.tireur, female.tireur);
        Action[] eclaireurChild = crossoverOneAgent(male.eclaireur, female.eclaireur);
        return new Squad(male.eclaireur.Length, tireurChild, eclaireurChild);
    }

    public static Action[] crossoverOneAgent(Action[] male, Action[] female)
    {
        int tailleADN = male.Length;

        int iCrossover = UnityEngine.Random.Range(0, tailleADN - 1);
        Action[] adnChild = new Action[tailleADN];
        Array.Copy(male, 0, adnChild, 0, iCrossover);
        Array.Copy(female, iCrossover, adnChild, iCrossover, tailleADN - iCrossover);
        return adnChild;
    }
}