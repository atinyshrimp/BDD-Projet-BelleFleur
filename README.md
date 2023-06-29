# Application de gestion de boutiques
## Introduction
Réalisation d'une application permettant à un fleuriste de gérer ses boutiques.
Grâce à cette application, le fleuriste peut gérer sa boutique, son stock, ses clients, et ses commandes.

### Information sur le projet

**Durée du projet :** 4 mois<br>
**Langage :** C#<br>
**Interface :** Windows Forms<br>
**Base de données :** MySQL<br>

## Information pour lancer le projet
+ Déployer la base de données
+ Exécuter le projet :
  + Ouvrir la solution "ConsoleApp1.sln" sur Visual Studio 2022, et exécuter "WindowsFormsApp1" OU
  + Ouvrir "WindowsFormsApp1\bin\Debug\WindowsFormsApp1.exe"

## Visuel de l'application
***Ce que montre la vidéo :*** *Comment créer un bouquet, remplir son contenu, valider sa création et voir le contenu de ce bouquet.*


![alt text](https://github.com/atinyzen/BDD-Projet-BelleFleur/blob/main/Visual_demo.gif)

## Spécifications techniques
voir *Note_technique_LAPILUS_B.pdf*

## Informations complémentaires
*L'application permet de gérer les stocks d'accessoires, de fleurs et de bouquets (composés des deux derniers) d'une boutique.<br>
Cette gestion passe par 4 modules : Boutiques, Clients, Produits et Commandes.*

### Boutiques
+ Dans ce module, l'admin peut créer, modifier et/ou supprimer une boutique.
+ Une liste des boutiques est disponible pour la visualisation de ces dernières.
+ Une barre de recherche permet d'accéder à une/des boutique(s) par son nom, son adresse et sa ville d'implémentation.

### Clients
+ Dans ce module, l'admin peut créer, modifier et/ou supprimer un client.
+ Une liste des clients est disponible pour la visualisation de ces derniers.
+ Une barre de recherche permet d'accéder à un/des client(s) par son adresse mail, son nom, son prénom, son numéro de téléphone, son adresse de facturation et son numéro de carte bancaire.
+ L'admin peut également filtrer la liste en fonction de la boutique d'appartenance des clients et/ou de leur niveau de fidélité.
+ Des fichiers peuvent également être exportés de l'application :
  + L'exportation en ***JSON*** des clients n'ayant pas commandé depuis plus de six mois.
  + L'exportation en ***XML*** des clients ayant commandé plusieurs fois durant le mois courant.

### Produits
+ Dans ce module, l'admin peut créer, modifier et/ou supprimer un produit.
  + Les bouquets étant des produits particuliers, leur gestion est séparée de celle des accessoires et des fleurs. Pour accéder à la liste, ainsi qu'à la modification et suppression des bouquets, il faut sélectionner     l'option *"Bouquets"* dans le filtre pour les produits
  + ***Modification d'un bouquet :***
    + En modifiant un bouquet, on peut voir les produits qui le composent, *mais* on ne peut pas les modifier ni les supprimer.
    + Si l'admin veut un bouquet avec d'autres quantités/produits, il doit en créer un autre.
    + L'admin peut modifier le stock d'un bouquet (qui n'affecte pas le stock des produits le composant), ce qui n'affectera que celui de la boutique active. Et il peut aussi changer les information du bouquet (nom, catégorie, etc.), ce qui affectera *toutes* les boutiques.
  + ***Création d'un bouquet :***
    + Lors de la création d'un bouquet, le stock de ce dernier est initialisé à zéro pour chacune des boutiques.
    + Lors du choix des produits composant le bouquet, seulement ceux qui correspondent à la période de disponibilité du bouquet sont affichés dans la liste.
+ Une liste des produits est disponible pour la visualisation de ces derniers.
+ Une barre de recherche permet d'accéder à un/des produit(s) par son code produit et son nom.
+ Deux filtres sont à disposition de l'admin :
  + Le premier pour le **type de produits**,
  + Le deuxième pour **la boutique**

### Commandes
+ Dans ce module, l'admin peut créer, modifier et/ou supprimer une commande.
+ Une liste des commandes est disponible pour la visualisation de ces dernières.
+ Une barre de recherche permet d'accéder à une/des boutique(s) par son numéro de commande, son adresse de livraison et l'email de son émetteur.
+ En modifiant une commande, on peut **seulement** voir son contenu.

### Statistiques
#### Module supplémentaire pour avoir une vue globale sur tous les magasins.
*Ce module regroupe les informations importantes par rapport aux données des boutiques sur une période donnée :* <br>
+ *Prix moyen du bouquet acheté*,
+ *Nombre de commandes*,
+ *Chiffre d'affaires total, toutes les boutiques combinées (par défaut)*,
+ *Les ventes totales sur la période donnée*,
+ *Le top 3 des meilleurs clients*,
+ *La boutique avec le meilleur chiffre d'affaires sur la période donnée*,
+ *Le top 5 des produits vendus*

<br>

+ Choix de la période donnée
  + Par défaut, lors de l'ouverture du module, la période est les ***trente derniers jours***
  + D'autres options sont mises à disposition : *sept derniers jours, le mois courant et les six derniers mois*.
  + Si les options ci-dessus ne correspondent pas aux attentes de l'admin, il peut choisir sa période en appuyant sur *Personnalisé*. Il choisit le début et la fin de la période voulue, et il confirme son choix en appuyant sur le ✓
+ Choix de la boutique
  + Les données affichées par l'interface graphique sont celles de **toutes les boutiques** combinées lors de l'ouverture du module.
  + Les flèches directionnelles permettent de changer la boutique sur laquelle sont basées les données ; les boutiques sont listées par ordre alphabétique.
+ Le top 5 des produits
  + Par défaut, lors de l'ouverture du module, le classement ne prend en compte que les accessoires et les fleurs.
  + De la même manière que pour les boutiques, les flèches directionnelles permettent de basculer entre plusieurs types de classement :
    + *Top 5 des accessoires*,
    + *Top 5 des fleurs*,
    + *Top 5 des bouquets*,
    + On peut également revenir au mode par défaut, *Top 5 des produits*.
