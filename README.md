
# Etkinlik biletleri alım satımı 

Çevrimiçi ürün satışı yapılan platform.

  
## Özellikler

* Asp.Net Core Api 8.0
* Katmanlı Mimari
* Entity Framework
* Entity Framework Identity
* Authentication ve Role-Based Authorization
* JWT barier token
* Microsoft Sql Veritaban

### API Endpoints
| HTTP Verbs | Endpoints | Action |
| --- | --- | --- |
| POST | /api/auth/register | To sign up a new user account |
| POST | /api/auth/login | To login an existing user account |
| GET | /api/tickets/all?page=1   To retrieve as many tickets as the number of pages from the platform|  |
| GET | /api/tickets/search?Query=sinema&Date=2024-12-01&Price=1000&page=1 | To retrieve details of a single cause |
| DELETE | /api/admin/deleteticket/{id} | To delete a single cause |
