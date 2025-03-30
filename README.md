🎟 Etkinlik Biletleri Alım Satım API

Bu API, etkinlik biletlerinin alım satım işlemlerini gerçekleştirmek için geliştirilmiştir. Kullanıcılar biletleri görüntüleyebilir, satın alabilir ve admin yetkileriyle biletleri silebilir. JWT tabanlı kimlik doğrulama ve rol bazlı yetkilendirme ile güvenli erişim sağlanmaktadır.

🚀 Teknolojiler

ASP.NET Core 8.0

Katmanlı Mimari

Entity Framework Core

Entity Framework Identity

Authentication ve Role-Based Authorization

JWT (JSON Web Token) Bearer Token

Microsoft SQL Server

📌 API Endpoints

Aşağıda API'de kullanılan endpoint'ler ve HTTP istek metotları listelenmiştir:

HTTP Verbs

Endpoint

Açıklama

POST

/api/auth/register

Yeni bir kullanıcı hesabı oluşturur.

POST

/api/auth/login

Var olan bir kullanıcı hesabıyla giriş yapar.

GET

/api/tickets/all?page=1

Platformdaki biletleri sayfalı olarak getirir.

GET

/api/tickets/search?Query=sinema&Date=2024-12-01&Price=1000&page=1

Belirtilen kriterlere göre biletleri arar.

DELETE

/api/admin/deleteticket/{id}

Bir bileti admin yetkisiyle siler.

🔐 API Yanıt Örnekleri

1️⃣ Kimlik Doğrulama Yanıtı

{
    "data": {
        "token": "{Token}",
        "expireDate": "{Token expire date}"
    }
}

2️⃣ Bilet Arama Yanıtı

{
    "data": [
        {
            "name": "Mercan Dede Konseri",
            "eventDate": "2026-01-12 21:00:10",
            "address": "Harbiye",
            "city": "İstanbul",
            "activity": "Konser",
            "imageUrl": "ticketDefault.jpg",
            "price": 1000
        },
        {
            "name": "Kaos Kafe Tiyatro Oyunu",
            "eventDate": "2026-02-20 23:10:10",
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
}

📌 Kurulum ve Çalıştırma

Projeyi Klonlayın:

git clone https://github.com/gurhanbatmaca/event-ticket-api.git
cd event-ticket-api

Bağımlılıkları Yükleyin:

dotnet restore

Veritabanı Göçlerini Çalıştırın:

dotnet ef database update

Uygulamayı Başlatın:

dotnet run

🛠 Katkıda Bulunma

Her türlü katkıya açığız! Fork alıp geliştirme yapabilir ve pull request gönderebilirsiniz. Sorularınız için Gürhan Batmaca ile iletişime geçebilirsiniz.

🚀 İyi kodlamalar!
