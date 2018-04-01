# velib

Extensions :

-Interface graphique client
-Cache



-Lancement du client console:

Ouvrir la solution velib.sln
Definir ClientConsole en tant que projet de démarrage, la console s'ouvre.
Les commandes help, exit, cities, city, stations sont disponibles
help affiche les commandes disponibles
exit permet de quitter 
cities affiche une liste des villes disponibles ainsi que le pays
city permet de selectionner une ville pour laquelle afficher les stations, une fois city entrée
il faut donner un nom de ville.
stations permet alors d'afficher la liste des stations de cette ville, entrer le nom d'une stations affiche alors le nombre de velos disponibles dans cette station.


-Lancement du client graphique:

Ouvrir la solution velib.sln
Definir ClientGUI en tant que projet de démarrage, l'interface se lance.
On peut alors selectionner sa ville avec le menu deroulant, une fois une ville selectionnée une liste des stations s'affiche.
On peut cliquer sur une des stations, des informations sur le station sont alors affichées. On peut aussi faire une recherche dans la liste a partir la textbox située au dessus de la liste,
elle est alors mise a jour.

-Cache:

Pour chaque recherche par ville, une liste des stations de la ville est mise en cache pour une durée definie.



