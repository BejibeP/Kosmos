-- =========================================
-- Schema kosmos
-- =========================================

CREATE SCHEMA kosmos;


-- =========================================
-- Table 1 : Planets (exemple pédagique et amusant)
-- =========================================
CREATE TABLE kosmos.planets (
    planet_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    size_km FLOAT,          -- diamètre approximatif
    distance_au FLOAT,      -- distance au soleil en UA
    has_life BIT DEFAULT 0,
    discovered_date DATE DEFAULT GETDATE()
);


-- Quelques données exemples
INSERT INTO kosmos.planets (name, size_km, distance_au, has_life)
VALUES 
('Mercury', 4879, 0.39, 0),
('Venus', 12104, 0.72, 0),
('Earth', 12742, 1.0, 1),
('Mars', 6779, 1.52, 0),
('Jupiter', 139820, 5.20, 0),
('Saturn', 116460, 9.58, 0);


-- =========================================
-- Table 2 : Users
-- =========================================
CREATE TABLE kosmos.users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL UNIQUE,
    email NVARCHAR(100) NOT NULL UNIQUE,
    profile_id INT NULL, -- profil par défaut
    created_at DATETIME DEFAULT GETDATE()
);


-- =========================================
-- Table 3 : Profiles
-- =========================================
CREATE TABLE kosmos.profiles (
    profile_id INT IDENTITY(1,1) PRIMARY KEY,
    profile_name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(255) NULL
);


-- =========================================
-- Table 4 : Rights
-- =========================================
CREATE TABLE kosmos.rights (
    right_id INT IDENTITY(1,1) PRIMARY KEY,
    label NVARCHAR(100) NOT NULL,
    type NVARCHAR(50) NOT NULL,        -- 'field', 'service', 'menu', etc.
    description NVARCHAR(255) NULL
);


-- =========================================
-- Table 5 : Profile_Rights (liens N:N)
-- =========================================
CREATE TABLE kosmos.profile_rights (
    profile_id INT NOT NULL,
    right_id INT NOT NULL,
    PRIMARY KEY (profile_id, right_id),
    CONSTRAINT fk_profile FOREIGN KEY (profile_id) REFERENCES kosmos.profiles(profile_id),
    CONSTRAINT fk_right FOREIGN KEY (right_id) REFERENCES kosmos.rights(right_id)
);


-- =========================================
-- Exemple de données pour profils
-- =========================================
INSERT INTO kosmos.profiles (profile_name, description)
VALUES
('Admin', 'Accès total à toutes les fonctions'),
('Astronomer', 'Peut lire et écrire des données planétaires'),
('Guest', 'Accès lecture seule aux planètes');

-- Exemple de droits
INSERT INTO kosmos.rights (label, type, description)
VALUES
('ReadPlanets', 'field', 'Lecture des informations des planètes'),
('EditPlanets', 'field', 'Modification des informations des planètes'),
('DeletePlanets', 'field', 'Suppression de planètes'),
('ManageUsers', 'service', 'Créer / modifier / supprimer des utilisateurs'),
('ViewProfiles', 'service', 'Visualiser les profils existants');

-- Exemple d’affectation de droits aux profils
-- Admin → tous droits
INSERT INTO kosmos.profile_rights (profile_id, right_id)
SELECT 1, right_id FROM kosmos.rights;

-- Astronomer → lecture + édition planètes
INSERT INTO kosmos.profile_rights (profile_id, right_id)
SELECT 2, right_id FROM kosmos.rights WHERE label IN ('ReadPlanets','EditPlanets');

-- Guest → lecture seule
INSERT INTO kosmos.profile_rights (profile_id, right_id)
SELECT 3, right_id FROM kosmos.rights WHERE label='ReadPlanets';


-- =========================================
-- Exemple d’utilisateur
-- =========================================
INSERT INTO kosmos.users (username, email, profile_id)
VALUES
('cecilia', 'cecilia@kosmos.local', 1),
('leone', 'leone@kosmos.local', 2),
('bob', 'bob@kosmos.local', 3);

