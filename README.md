# 🌐 Dotel Project

**Dotel** is a multifunctional web management system built on **ASP.NET Core Razor Pages**. The project offers comprehensive features such as user management, real-time messaging, membership subscriptions, email notification service, and role-based access control. It's suitable for platforms involving content management, rental services, ticket booking, or customer service.


Demo: dotel-bvf5gwccf4dgf4f3.southeastasia-01.azurewebsites.net
---

## ⚙️ Technologies Used

- **Language:** C#, ASP.NET Core Razor Pages
- **Front-end:** HTML, CSS, JavaScript, Bootstrap 5, jQuery
- **Database:** SQL Server
- **Authentication & Authorization:** Cookie-based authentication with role-based access control from the database
- **Email Service:** Mailgun (SMTP API)
- **Deployment:** Azure App Service + GitHub Actions CI/CD pipeline
- **Payment Integration:** VNPay or MoMo (in development)
- **Google Maps:** For rental location display
- **Google Analytics:** For traffic and usage analysis

---

## 🔐 User Roles

Dotel implements role-based access with the following user types:

- **Guest (Unauthenticated):** Limited access; prompted to register when trying to access restricted features  
- **Regular User:** Can manage personal profile, access basic features, and communicate with support staff  
- **Member:** Gains premium privileges such as posting content or accessing exclusive features  
- **Admin:** Full system access to manage users, content, and analytics  

---

## 💬 Key Features

### 🔍 Smart Rental Search with Auto-Suggestions
- Real-time suggestions while typing
- Enhanced user experience during property searches

### 💬 Real-Time Chat
- SignalR-powered Messenger-style chat
- Conversations grouped and displayed by user

### ✉️ Email Notification System
- Integrated with Mailgun via SMTP
- Sends registration confirmation, system alerts, and contact messages

### ☁️ CI/CD Deployment with Azure
- Automatically builds and deploys latest code from GitHub using GitHub Actions
- Supports environment-specific settings and secret management

### 🗺️ Google Maps Integration
- Visualize rental property locations directly on the map

### 📊 Google Analytics Integration
- Analyze user behavior and track site performance

---

> ⚠️ **Note:** Payment feature under active development. Contributions and feedback are welcome!
