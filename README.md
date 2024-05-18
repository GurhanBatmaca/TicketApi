# Etkinlik biletleri alım satımı için Api

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
| GET | /api/tickets/all?page=1 | To retrieve as many tickets as the number of pages from the platform  |
| GET | /api/tickets/search?Query=sinema&Date=2024-12-01&Price=1000&page=1 | To retrieve tickets from the platform according to search criteria |
| DELETE | /api/admin/deleteticket/{id} | To delete a single ticket |

### API Response

```
"data": {
            "token": "{Token}",
            "expireDate": "{Token expire date}"
        }
```

```

"data": [
    {
        "name": "Mercan Dede Konseri",
        "eventDate": "2026.01.12 21:00:10",
        "address": "Harbiye",
        "city": "İstanbul",
        "activity": "Konser",
        "imageUrl": "ticketDefault.jpg",
        "price": 1000
    },
    {
        "name": "Kaos Kafe Tiyatro Oyunu",
        "eventDate": "2026.20.02 23:10:10",
        "address": "Ankara Sanat Tiyatrosu",
        "city": "Ankara",
        "activity": "Tiyatro",
        "imageUrl": "ticketDefault.jpg",
        "price": 500
    }
],
"pageInfo": {
                "totalItems": 200,
                "itemPerPage": 2,
                "currentPage": 1,
                "totalPages": 100
            }
```
