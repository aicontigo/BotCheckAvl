# BotCheckAvl

BotCheckAvl is a Telegram bot designed to monitor the availability and health of web services by their URLs. The bot provides real-time notifications about the status of each registered service, helping you stay informed about outages, slow responses, and recoveries.

## Features

- **Service Monitoring:**
  - Periodically checks the status of registered services by their URLs.
  - Notifies users if a service becomes unavailable (does not respond).
  - Notifies users when a service recovers and becomes available again.
  - Detects and notifies if a service is slow to respond (timeout and retry count are configurable via the bot menu).

- **Service Management:**
  - List all registered services and their current statuses via a bot command.
  - Add new services to monitor through the bot menu.
  - Remove services from monitoring via the menu.
  - Temporarily disable monitoring for specific services.

## Configuration

- All configuration (timeouts, retry counts, etc.) can be managed via the bot's interactive menu.
- Service URLs and their statuses are stored and managed by the bot.
- The `appsettings.json` file now includes a `TgBot` section. Set your Telegram bot token in `TgBot:Token` and optionally a `WebhookUrl`.

## Use Cases

- Get instant alerts in Telegram if any of your critical services go down or become slow.
- Easily manage the list of monitored services without leaving the Telegram interface.
- Suitable for DevOps, SREs, and anyone who needs simple uptime monitoring with instant notifications.

## Getting Started

1. Clone the repository and edit `appsettings.json` to provide your bot token in the `TgBot` section.
2. Run the application using .NET 9.0.
3. Interact with the bot in Telegram to add, remove, or manage services.

## Database Migrations

Entity Framework Core with SQLite is used for data storage. After changing the
model, create and apply migrations:

```bash
dotnet tool install --global dotnet-ef   # once
dotnet ef migrations add InitialCreate
dotnet ef database update
```

The default SQLite database file is `botcheckavl.db` as configured in
`appsettings.json`.

---

**Note:** This project is under active development. Contributions and feedback are welcome! 