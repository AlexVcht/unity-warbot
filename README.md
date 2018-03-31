# Warbot

## Principe du jeu
C'est un tournoi d'équipes de tanks où le but est de toucher toutes les cibles de la carte le plus rapidement possible. A cela nous avons appliqué un algorithme génétique pour l'apprentissage de l'environnement.

L'environnement est fixe pour tout un tournoi. C'est à dire qu'il est initialisé au lancement à chaque lancement du programme, mais d'une génération à l'autre il ne change pas, sinon les tanks seraient perdus.

## Agents de la simulation
Les agents de la simulation sont
- Tank tireur : détaillé ci-dessous
- Tank éclaireur : détaillé ci-dessous
- Cible : objet que les tanks cherchent à détruire
- Obus : munition tirée par le tank tireur pour détruire une cible, elle explose dès qu'elle touche quelque chose
- Balle de peinture : munition tirée par le tank éclaireur pour marquer les cibles, elle explose dès qu'elle touche quelque chose

### Tank tireur
Ce tank est équipé d'un canon qui fait mal pour détruire les cibles, il a une portée limitée et ne va pas très vite (car il est lourd). Il a accès à la carte d'équipe du terrain où il peut voir les cibles qui ont été découvertes par le tank éclaireur.

### Tank éclaireur
Ce tank est équipé d'un canon marqueur, il a une portée limitée et est plus rapide que le tank tireur (car il a des obus de peinture et pas en métal). Quand il s'approche d'une cible il tire dessus, ce qui la marque sur la carte de l'équipe et la rend accessible au tank tireur.

### Actions possibles des tanks
- Bouger dans une direction aléatoire
- - Les paramètres de l'action sont :
- - - La direction vers laquelle aller
- - - Le temps du déplacement
- Regarder la carte d'équipe et choisir une cible vers laquelle aller (seulement pour le tank tireur)
- - Les paramètres de l'action sont :
- - - L'algorithme de choix de la cible, les choix possibles sont :
- - - - La cible la plus proche
- - - - La cible la plus éloignée

## Algorithme génétique
Nous avons appliqué un algorithme génétique pour faire apprendre les tanks où aller chercher les cibles à attaquer et à jouer en équipe.

L'ADN des tanks est une suite d'actions qu'ils vont réaliser, certaines prennent plus de temps que d'autres ou impliquent une coopération dans l'équipe.

Les actions possibles sont décrites plus haut.

Action est une interface. L'ADN est un tableau d'Action. De cette façon, nous pouvons gérer les actions et la génétique indépendamment de l'action. 
- Pour exécuter l'ADN du bot il suffit d'appeler la méthode execute() sur chaque case de l'ADN. 
- Pour faire évoluer un tank il suffit d'appeler la méthode mutate() sur chaque case de l'ADN.

Ceci permet de pouvoir ajouter des actions possibles (même complexe) au bot sans avoir à modifier l’algorithme de décision et l’algorithme génétique.

### Apprentissage
Une fois la population initialisée, nous faisons jouer chaque équipe (tank tireur et tank éclaireur) sur le terrain, nous enregistrons le temps (limité à 1min30s) qu'ils mettent à détruire toutes les cibles et le nombre de cibles détruites (utile s'ils n'ont pas eu le temps de tout détruire). Nous attribuons ensuite un score à l'équipe, défini par :
```
squad.score = ((maxTimeSimulation - elapsedTime)*1000 / maxTimeSimulation + (NumTargets - NumTargetsAlive)*1000 / NumTargets)
```
où 
- `maxTimeSimulation` est le temps maximum autorisé pour détruire toutes les cibles
- `elapsedTime` est le temps que l'équipe a mis à détruire toutes les cibles (ou `maxTimeSimulation` si ils n'ont pas réussi)
- `NumTargets` est le nombre total de cibles sur le terrain à l'initialisation de la partie
- `NumTargetsAlive` est le nombre de cibles restants à la fin (0 s'ils ont réussi à tout détruire dans le temps imparti)

Quand toutes les équipes ont joué. Nous gardons N/2 équipes (N étant la taille de la population), qui vont ensuite se reproduire pour créer la génération d'après. 

Le brassage génétique se fait en deux étapes :
- La mutation de l'ADN, qui peut se faire sur les paramètres de l'action ou sur le type d'action
- - Exemple : 'Bouger' mute en 'Regarder la carte' ou 'Bouger vers Nord' mute en 'Bouger vers Sud'
- Le crossover entre deux tanks. Deux tanks du même type sont sélectionnés et font un échange d'une part de leur ADN
- - Exemple : [1 2 3 4 5] crossover avec [10 11 12 13 14] pour donner [1 2 3 13 14] et [10 11 12 4 5]

Une fois la génération d'après créée nous recommencons à faire jouer toutes les équipes ...

## Sauvegarde et chargement de la partie
L'apprentissage des tanks peut être long et nous offrons donc la possibilité de sauvegarder la simulation pour la recharger au prochain lancement. A la fin de chaque génération nous sauvegardons donc les données de la simulation (ADN des équipes, placement des cibles, etc). Un bouton Load est ensuite disponible sur le menu du jeu pour charger cette sauvegarde.

## Ouverture sur unity
- Lancer unity
- Importer le projet
- Dans la fenêtre "Project" double clic sur "Warbot.unity"

## Lancement sur unity 
- Dans le game manager préciser
-- le nombre de round à gagner : "Num Rounds To Win"
-- le nombre de cible générés aléatoirement : "Num Targets"
