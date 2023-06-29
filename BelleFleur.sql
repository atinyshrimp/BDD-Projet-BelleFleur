DROP DATABASE IF EXISTS fleurs;
CREATE DATABASE fleurs;
USE fleurs;

-- --------------------------------------------------------

--
-- Structure de la table `boutique`
--

DROP TABLE IF EXISTS boutique;
CREATE TABLE boutique
(id_boutique INT NOT NULL PRIMARY KEY,
nom_boutique VARCHAR(30) NOT NULL,
adresse_boutique VARCHAR(60) NOT NULL,
ville_boutique VARCHAR(30) NOT NULL);

--
-- Peuplement de la table `boutique`
--

INSERT INTO boutique (`id_boutique`,`nom_boutique`,`adresse_boutique`,`ville_boutique`)VALUES
(1, 'Belle Fleur', '2 Rue de Marseille', 'Paris'),
(2, 'Garden Grace', '45 Boulevard Yoyo', 'Strasbourg'),
(3, 'Lily Lane', '27 Avenue Janvier', 'Lille'),
(4, 'Floral Fiesta', '5 Rue KQ', 'Toulouse');

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

DROP TABLE IF EXISTS client;
CREATE TABLE client
(courriel VARCHAR(60) NOT NULL PRIMARY KEY, nom VARCHAR(60), prenom VARCHAR(60),
no_tel VARCHAR(10), mdp VARCHAR(20) NOT NULL, adresse_facture VARCHAR(60), no_carte VARCHAR(16),
fidelite ENUM('Or', 'Bronze', 'None'),
id_boutique INT, CONSTRAINT FK_client_id_boutique FOREIGN KEY (id_boutique) REFERENCES boutique(id_boutique));

--
-- Peuplement de la table `client`
--

INSERT INTO client (`courriel`,`nom`,`prenom`,`no_tel`,`mdp`,`adresse_facture`,`no_carte`,`fidelite`,`id_boutique`) VALUES
('abc@gmail.com', 'Kim', 'Hongjoong', '0612345679', 'motdepasse', '1 Rue de Paris', '5555222233334440', 'None', 1),
('def@gmail.com', 'Park', 'Seonghwa', '0612345680', 'motdepasse', '2 Rue de Paris', '5555222233334440', 'None', 1),
('ghi@gmail.com', 'Jung', 'Yunho', '0612345681', 'motdepasse', '3 Rue de Paris', '5555222233334440', 'None', 2),
('jkl@gmail.com', 'Kang', 'Yeosang', '0612345682', 'motdepasse', '4 Rue de Paris', '5555222233334440', 'None', 3),
('mno@gmail.com', 'Choi', 'San', '0612345683', 'motdepasse', '5 Rue de Paris', '5555222233334440', 'None', 1),
('pqr@gmail.com', 'Song', 'Mingi', '0612345684', 'motdepasse', '6 Rue de Paris', '5555222233334440', 'None', 2),
('stu@gmail.com', 'Jung', 'Wooyoung', '0612345685', 'motdepasse', '7 Rue de Paris', '5555222233334440', 'None', 2),
('vwx@gmail.com', 'Choi', 'Jongho', '0612345686', 'motdepasse', '8 Rue de Paris', '5555222233334440', 'None', 3),
('yz@yahoo.fr', 'Choi', 'Yeonjun', '0612345687', 'motdepasse', '9 Rue de Paris', '5555222233334440', 'None', 3),
('contact@sment.com', 'Jung', 'Jaehyun', '0612345684', 'motdepasse', '6 Rue de Paris', '5555222233334440', 'None', 2);

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

DROP TABLE IF EXISTS commande;
CREATE TABLE commande
(no_commande VARCHAR(16) PRIMARY KEY,
adresse VARCHAR(60),
type_commande ENUM('standard', 'personnalisé'),
message VARCHAR(140), date_commande DATE, date_livraison DATE,
etat ENUM('VINV', 'CC', 'CPAV', 'CAL', 'CL'),
prix_sans_reduc DECIMAL(5,2),
prix_commande DECIMAL(5,2),
courriel VARCHAR(60), CONSTRAINT FK_commande_courriel FOREIGN KEY (courriel) REFERENCES client(courriel) ON DELETE CASCADE ON UPDATE CASCADE);

--
-- Peuplement de la table `commande`
--

INSERT INTO commande (`no_commande`, `adresse`, `type_commande`, `message`, `date_commande`, `date_livraison`, `etat`, `prix_sans_reduc`, `prix_commande`, `courriel`) VALUES
('CMD-20230501-005', 'KQ ENTERTAINMENT', 'standard', 'Pour E', '2023-05-01', '2023-05-05', 'CL', 45, 45, 'def@gmail.com'),
('CMD-20230501-004', 'KQ ENTERTAINMENT', 'standard', 'Pour D', '2023-05-01', '2023-05-05', 'CL', 45, 45, 'def@gmail.com'),
('CMD-20230501-003', 'KQ ENTERTAINMENT', 'standard', 'Pour C', '2023-05-01', '2023-05-05', 'CL', 45, 45, 'def@gmail.com'),
('CMD-20230501-002', 'KQ ENTERTAINMENT', 'standard', 'Pour B', '2023-05-01', '2023-05-05', 'CL', 45, 45, 'def@gmail.com'),
('CMD-20230501-001', 'KQ ENTERTAINMENT', 'standard', 'Pour A', '2023-05-01', '2023-05-05', 'CL', 45, 45, 'def@gmail.com'),
('CMD-20230412-001', 'Paris', 'personnalisé', '', '2023-04-12', '2023-04-16', 'CL', 5.6, 5.6, 'yz@yahoo.fr'),
('CMD-20230307-001', 'Paris', 'personnalisé', '', '2023-03-07', '2023-03-13', 'CL', 57, 57, 'ghi@gmail.com'),
('CMD-20230208-001', 'KQ ENTERTAINMENT', 'standard', '', '2023-02-08', '2023-02-14', 'CL', 120, 120, 'def@gmail.com'),
('CMD-20230121-001', '2 Rue de Paris', 'standard', '', '2023-01-21', '2023-01-25', 'CL', 65, 65, 'def@gmail.com');

-- --------------------------------------------------------

--
-- Structure de la table `produit`
--

DROP TABLE IF EXISTS produit;
CREATE TABLE produit 
(id_produit VARCHAR(4) NOT NULL PRIMARY KEY, nom_produit VARCHAR(30), occasion VARCHAR(30), 
prix_produit DECIMAL(5,2),
dispo_debut INT, dispo_fin INT, seuil_produit INT);

--
-- Structure de la table `produit`
--

INSERT INTO produit (`id_produit`, `nom_produit`, `occasion`,  `prix_produit`, `dispo_debut`, `dispo_fin`, `seuil_produit`) VALUES
('A001', 'Vase en argile', 'Toute occasion', 2.6, 1, 12, 10),
('A002', 'Ruban rose', 'Toute occasion', 1.5, 1, 12, 10),
('A003', 'Boite', 'Toute occasion', 5, 1, 12, 10),
('A004', 'Vase en verre', 'Toute occasion', 8, 1, 12, 10),
('F001', 'Gerbera', 'Toute occasion', 5, 1, 12, 5),
('F002', 'Ginger', 'Toute occasion', 4, 1, 12, 5),
('F003', 'Glaieul', 'Toute occasion', 1, 5, 11, 5),
('F004', 'Marguerite', 'Toute occasion', 2.25, 1, 12, 5),
('F005', 'Rose rouge', 'Toute occasion', 2.5, 1, 12, 5),
('F006', 'Rose blanche', 'Toute occasion', 2.5, 1, 12, 5),
('F007', 'Oiseau de paradis', 'Toute occasion', 3.5, 1, 12, 5),
('F008', 'Genet', 'Toute occasion', 3.5, 1, 4, 5),
('F009', 'Lys', 'Toute occasion', 3.5, 3, 6, 5),
('F010', 'Alstromeria', 'Toute occasion', 2, 1, 12, 5),
('F011', 'Orchidees', 'Toute occasion', 2.75, 1, 12, 5),
('B001', 'Gros Merci', 'Toute occasion', 45, 1, 12, 5),
('B002', 'L\'amoureux', 'St Valentin', 65, 1, 12, 5),
('B003', 'L\'exotique', 'Toute occasion', 40, 1, 12, 5),
('B004', 'Maman', 'Fête des Mères', 80, 1, 12, 5),
('B005', 'Vive la mariée', 'Mariage', 120, 1, 12, 5);

-- --------------------------------------------------------

--
-- Structure de la table `compose`
--

DROP TABLE IF EXISTS compose;
CREATE TABLE compose
(id_bouquet VARCHAR(4) NOT NULL, CONSTRAINT FK_compose_id_bouquet FOREIGN KEY (id_bouquet) REFERENCES produit(id_produit) ON DELETE CASCADE ON UPDATE CASCADE,
id_composant VARCHAR(50), CONSTRAINT FK_compose_id_composant FOREIGN KEY (id_composant) REFERENCES produit(id_produit) ON DELETE CASCADE ON UPDATE CASCADE,
qte INT,
PRIMARY KEY(id_bouquet, id_composant));

--
-- Peuplement de la table `compose`
--

INSERT INTO compose (`id_bouquet`, `id_composant`, `qte`) VALUES
('B001', 'F004', 19),
('B002', 'F005', 13),
('B002', 'F006', 12),
('B003', 'F002', 3),
('B003', 'F005', 2),
('B003', 'F006', 2),
('B003', 'F007', 3),
('B003', 'F008', 1),
('B004', 'F001', 7),
('B004', 'F006', 5),
('B004', 'F009', 5),
('B004', 'F010', 6),
('B005', 'F009', 19),
('B005', 'F011', 18);

-- --------------------------------------------------------

--
-- Structure de la table `inclut`
--

DROP TABLE IF EXISTS inclut;
CREATE TABLE inclut
(no_commande VARCHAR(60), CONSTRAINT FK_inclut_no_commande FOREIGN KEY (no_commande) REFERENCES commande(no_commande) ON DELETE CASCADE ON UPDATE CASCADE,
id_produit VARCHAR(4), CONSTRAINT FK_inclut_id_produit FOREIGN KEY (id_produit) REFERENCES produit(id_produit) ON DELETE CASCADE ON UPDATE CASCADE,
nb_produit INT NOT NULL,
PRIMARY KEY(no_commande, id_produit));

--
-- Peuplement de la table `inclut`
--

INSERT INTO inclut (`no_commande`, `id_produit`, `nb_produit`) VALUES
('CMD-20230501-005', 'B001', 1),
('CMD-20230501-004', 'B001', 1),
('CMD-20230501-003', 'B001', 1),
('CMD-20230501-002', 'B001', 1),
('CMD-20230501-001', 'B001', 1),
('CMD-20230412-001', 'A001', 1),
('CMD-20230412-001', 'A002', 2),
('CMD-20230307-001', 'A003', 1),
('CMD-20230307-001', 'F002', 3),
('CMD-20230307-001', 'F009', 4),
('CMD-20230307-001', 'F010', 2),
('CMD-20230307-001', 'F011', 8),
('CMD-20230208-001', 'B002', 1),
('CMD-20230121-001', 'B003', 1);

-- --------------------------------------------------------

--
-- Structure de la table `stocke`
--

DROP TABLE IF EXISTS stocke;
CREATE TABLE stocke
(id_boutique INT NOT NULL, CONSTRAINT FK_stocke_id_boutique FOREIGN KEY (id_boutique) REFERENCES boutique(id_boutique) ON DELETE CASCADE ON UPDATE CASCADE,
id_produit VARCHAR(4) NOT NULL, CONSTRAINT FK_stocke_id_produit FOREIGN KEY (id_produit) REFERENCES produit(id_produit) ON DELETE CASCADE ON UPDATE CASCADE,
qte_inv INT );

--
-- Déclencheur de la table `stocke` ; gestion d'une rupture de stock
--

delimiter //
CREATE TRIGGER check_inventory
BEFORE UPDATE ON stocke
FOR EACH ROW
BEGIN
	IF NEW.qte_inv < 0 THEN
		SIGNAL SQLSTATE "45000" SET MESSAGE_TEXT = "Pas assez de stock";
	END IF;
END; //
delimiter ;

--
-- Peuplement de la table `stocke`
--

LOAD DATA INFILE "C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/Projet/stocke.csv"
INTO TABLE stocke
FIELDS TERMINATED BY ';'
LINES STARTING BY ''
TERMINATED BY '\r\n';
-- j'ai laissé cette manière de peupler la table car il y a beaucoup trop de données pour les écrire à la main

-- --------------------------------------------------------

--
-- Création de l'utilisateur 'bozo'
--
 
CREATE USER IF NOT EXISTS 'bozo'@'localhost' IDENTIFIED BY 'bozo';
GRANT SELECT ON fleurs.* TO 'bozo'@'localhost';
SHOW GRANTS FOR 'bozo'@'localhost';
SHOW GRANTS FOR 'root'@'localhost';
REVOKE USAGE ON *.* FROM 'bozo'@'localhost';

--
-- Fin du script
--

-- --------------------------------------------------------