# DevOps - Dev Stack

Cette section contient un docker compose all-in-one pour lancer un environnement de developpement et tester les fonctionnalité de vos astres Kosmos.

Remplissez juste le fichier .env avec les mots de passe souhaiités

Cette stack Docker Compose fournit un environnement complet pour le développement d’applications .NET incluant :

- **MSSQL Server 2025**
- **Redis 8.2**
- **Keycloak**
- **RabbitMQ (avec management UI)**
- **HashiCorp Vault**
- **Mailpit (SMTP + API HTTP + UI)**

Tous les services utilisent **des volumes nommés** pour un stockage persistant compatible Portainer (aucune dépendance aux dossiers hôtes).

---

## 📦 Contenu

## Services

| Service     | Port local | Description |
|-------------|------------|-------------|
| MSSQL       | 1433       | Base de données SQL Server 2025 |
| Redis       | 6379       | Cache / PubSub |
| Keycloak    | 8080       | Identity & Access Management |
| RabbitMQ    | 15672 / 5672 | Broker de messages |
| Vault       | 8200       | Secret manager |
| Mailpit     | 8025 / 1025 | SMTP fake + API HTTP pour tests d’email |

---

## 🚀 Lancer la stack

Placez un fichier `.env` (fourni dans ce repo) puis lancez :

```bash
docker compose up -d
```

Ou via Portainer → Stacks → Add → Upload docker-compose.yml.

🔐 Configuration des services
MSSQL

Login :

user : sa

password : défini dans .env (par défaut : YourStr0ngDevPass!)

Redis

Accessible sur :

localhost:6379

Keycloak

UI disponible sur :

👉 http://localhost:8080

Credentials :

User : admin

Password : admin

RabbitMQ

Management UI :
👉 http://localhost:15672

Default login : guest / guest

Vault

UI Web :
👉 http://localhost:8200

Au premier lancement :

Cliquez sur Initialize

Sélectionnez :

Secret shares: 1

Secret threshold: 1

Sauvegardez le Unseal Key et le Root Token

Cliquez sur Unseal

Vault restera utilisable tant que le conteneur ne redémarre pas.

Mailpit (SMTP + API HTTP)
UI Web :

👉 http://localhost:8025

SMTP (pour votre app .NET)
host: localhost
port: 1025
SSL: false

API HTTP (FOSS alternative à SendGrid/Mailgun)

Envoyer un email :

POST http://localhost:8025/api/v1/messages
Content-Type: application/json

{
  "from": "test@example.com",
  "to": ["demo@example.com"],
  "subject": "Hello",
  "text": "Message via HTTP API"
}

🛠 Fichier .env

Le fichier .env fourni contient des valeurs par défaut fonctionnelles pour le développement.

Vous pouvez ajuster les ports et mots de passe sans modifier le docker-compose.yml.

📁 Volumes persistants

Tous les services utilisent des volumes nommés :

mssql-data
redis-data
keycloak-data
rabbitmq-data
vault-data
mailpit-data

Pour les supprimer totalement :

docker compose down -v

🧩 Notes

Vault est en mode file storage, adapté pour le développement.

L’unseal automatique n’est volontairement pas inclus (unseal manuel via l’UI).

Mailpit remplace MailHog : API HTTP + UI moderne + FOSS.

Aucun secret de production ne doit être utilisé dans ce stack.
