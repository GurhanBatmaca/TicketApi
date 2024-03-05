using System.Data;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data;

public static class ModelBuilderExtention
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>().HasData(
            new Activity {Id=1,Name="Konser",Url="konser",ImageUrl=""},
            new Activity {Id=2,Name="Söyleşi",Url="soylesi",ImageUrl=""},
            new Activity {Id=3,Name="Konuşma",Url="konusma",ImageUrl=""},
            new Activity {Id=4,Name="Tiyatro",Url="tiyatro",ImageUrl=""},
            new Activity {Id=5,Name="Sinema",Url="sinema",ImageUrl=""}
        );

        modelBuilder.Entity<Address>().HasData(
            new Address {Id=1,Title="Harbiye",City="İstanbul",Country="Türkiye",ImageUrl=""},
            new Address {Id=2,Title="Galata Port",City="İstanbul",Country="Türkiye",ImageUrl=""},
            new Address {Id=3,Title="Fuar Kültürpark",City="İzmir",Country="Türkiye",ImageUrl=""},
            new Address {Id=4,Title="Rockefeller Center",City="New York",Country="USA",ImageUrl=""},

            new Address {Id=5,Title="İstanbul Devlet Tiyatrosu",City="İstanbul",Country="Türkiye",ImageUrl=""},
            new Address {Id=6,Title="Ankara Sanat Tiyatrosu",City="Ankara",Country="Türkiye",ImageUrl=""},
            new Address {Id=7,Title="Cinetime Sinemaları",City="İstanbul",Country="Türkiye",ImageUrl=""}
        );

        modelBuilder.Entity<Artor>().HasData(
            new Artor {Id=1,Name="Mercan Dede",Url="mercan-dede",ImageUrl=""},
            new Artor {Id=2,Name="Serra Yılmaz",Url="serra-yilmaz",ImageUrl=""},
            new Artor {Id=3,Name="Sezen Aksu",Url="sezen-aksu",ImageUrl=""},
            new Artor {Id=4,Name="Ajda Pekkan",Url="ajda-pekkan",ImageUrl=""},
            new Artor {Id=5,Name="Carole King",Url="carole-king",ImageUrl=""},
            new Artor {Id=6,Name="Betül Mardin",Url="betul-mardin",ImageUrl=""},

            new Artor {Id=7,Name="Haluk Bilginer",Url="haluk-bilginer",ImageUrl=""},
            new Artor {Id=8,Name="Demet Akbağ",Url="demek-akbag",ImageUrl=""},
            new Artor {Id=9,Name="Altan Erkekli",Url="altan-erkekli",ImageUrl=""},

            new Artor {Id=10,Name="Cem Yılmaz",Url="cem-yilmaz",ImageUrl=""},
            new Artor {Id=11,Name="Gülse Birsel",Url="gulse-birsel",ImageUrl=""},
            new Artor {Id=12,Name="Aras Bulut İylemli",Url="aras-bulut-iylemli",ImageUrl=""}
        );

        modelBuilder.Entity<Category>().HasData(
            new Category {Id=1,Name="Rock",Url="rock",ImageUrl=""},
            new Category {Id=2,Name="Jazz",Url="jazz",ImageUrl=""},
            new Category {Id=3,Name="Pop",Url="pop",ImageUrl=""},
            new Category {Id=4,Name="Klasik",Url="klasik",ImageUrl=""},
            new Category {Id=5,Name="Rap",Url="rap",ImageUrl=""},
            new Category {Id=6,Name="Halkla İlişkiler",Url="halkla-iliskiler",ImageUrl=""},

            new Category {Id=7,Name="Komedi",Url="komedi",ImageUrl=""},
            new Category {Id=8,Name="Macera",Url="macera",ImageUrl=""},
            new Category {Id=9,Name="Drama",Url="drama",ImageUrl=""},
            new Category {Id=10,Name="Tarih",Url="tarih",ImageUrl=""}
        );

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket {Id=1,SeatNumber=1,Limit=100,Name="Mercan Dede Konseri",Url="mercan-dede-konseri",Price=1000,EventDate=new DateTime(2026, 12, 01, 21, 00, 10),AddressId=1,ActivityId=1,ImageUrl=""},

            new Ticket {Id=2,SeatNumber=2,Limit=100,Name="Sezen Aksu Konseri",Url="sezen-aksu-konseri",Price=1500,EventDate=new DateTime(2026, 10, 01, 21, 00, 10),AddressId=1,ActivityId=1,ImageUrl=""},

            new Ticket {Id=3,SeatNumber=3,Limit=100,Name="Ajda Pekkan Konseri",Url="ajda-pekkan-konseri",Price=1500,EventDate=new DateTime(2026, 06, 01, 21, 00, 10),AddressId=2,ActivityId=1,ImageUrl=""},

            new Ticket {Id=4,SeatNumber=4,Limit=100,Name="Ajda Pekkan Konseri",Url="ajda-pekkan-konseri",
            Price=1500,EventDate=new DateTime(2026, 05, 01, 21, 00, 10),AddressId=2,ActivityId=1,ImageUrl=""},

            new Ticket {Id=5,SeatNumber=5,Limit=100,Name="Betül Mardin Konuşma",Url="betul-mardin-konusma",
            Price=1200,EventDate=new DateTime(2026, 04, 01, 21, 00, 10),AddressId=3,ActivityId=3,ImageUrl=""},

            new Ticket {Id=6,SeatNumber=1,Limit=100,Name="Serra Yılmaz Konseri",Url="serra-yilmaz-konseri",Price=800,EventDate=new DateTime(2026, 03, 23, 23, 10, 10),AddressId=1,ActivityId=1,ImageUrl=""},

            new Ticket {Id=7,SeatNumber=1,Limit=100,Name="Gülmeyen Gülümseme Tiyatro Oyunu",Url="gulmeyen-gulumseme-tiyatro-oyunu",Price=800,EventDate=new DateTime(2026, 02, 23, 23, 10, 10),AddressId=5,ActivityId=4,ImageUrl=""},

            new Ticket {Id=8,SeatNumber=1,Limit=100,Name="Kaos Kafe Tiyatro Oyunu",Url="kaos-kafe-tiyatro-oyunu",Price=500,EventDate=new DateTime(2026, 02, 20, 23, 10, 10),AddressId=6,ActivityId=4,ImageUrl=""},

            new Ticket {Id=9,SeatNumber=1,Limit=100,Name="Rüya Takipçileri Sinama Filmi",Url="ruya-takipcileri-sinama-filmi",Price=500,EventDate=new DateTime(2026, 01, 23, 23, 10, 10),AddressId=7,ActivityId=5,ImageUrl=""},

            new Ticket {Id=10,SeatNumber=1,Limit=100,Name="Kuantum Kediler Sinama Filmi",Url="kuantum-kediler-sinama-filmi",Price=500,EventDate=new DateTime(2026, 01, 20, 23, 10, 10),AddressId=7,ActivityId=5,ImageUrl=""}
        );

        modelBuilder.Entity<Work>().HasData(
            new Work {Id=1,Name="Sufi Dreams",Url="sufi-dreams",Description="Sufi Dreams"},
            new Work {Id=2,Name="Nar",Url="nar",Description="Nar"},

            new Work {Id=3,Name="Gülümse",Url="gulumse",Description="Gülümse"},
            new Work {Id=4,Name="Firuze",Url="firuze",Description="Firuze"},

            new Work {Id=5,Name="Palavra Palavra",Url="palavra",Description="Palavra Palavra"},
            new Work {Id=6,Name="Neler Edeceğim",Url="neler-edecegim",Description="Neler Edeceğim"},

            new Work {Id=7,Name="Discover Turkey",Url="discover-turkey",Description="Discover Turkey"},
            new Work {Id=8,Name="Değerli Dostum",Url="degerli-dostum",Description="Değerli Dostum"},

            new Work {Id=9,Name="Gora",Url="gora",Description="Gora 1"},
            new Work {Id=10,Name="Tatlı Hayat",Url="tatli-hayat",Description="Hatlı Hayat Dizisi"},
            new Work {Id=11,Name="Bir Demet tiyatro",Url="bir-demet-tiyatro",Description="Bir Demek Tiyatro Oyunu"},
            new Work {Id=12,Name="Avrupa Yakası",Url="avrupa-yakasi",Description="Avrupa Yakası Dizisi"},
            new Work {Id=13,Name="Pek Yakında",Url="pek-yakinda",Description="Pek Yaında Filmi"}
        );

        modelBuilder.Entity<ActivityCategory>().HasData(
            new ActivityCategory {ActivityId=1,CategoryId=1},
            new ActivityCategory {ActivityId=1,CategoryId=2},
            new ActivityCategory {ActivityId=1,CategoryId=3},
            new ActivityCategory {ActivityId=2,CategoryId=6},
            new ActivityCategory {ActivityId=3,CategoryId=6},

            new ActivityCategory {ActivityId=4,CategoryId=7},
            new ActivityCategory {ActivityId=4,CategoryId=8},
            new ActivityCategory {ActivityId=5,CategoryId=9},
            new ActivityCategory {ActivityId=5,CategoryId=10}
        );

        modelBuilder.Entity<ArtorWork>().HasData(
            new ArtorWork {ArtorId=1,WorkId=1},
            new ArtorWork {ArtorId=1,WorkId=2},

            new ArtorWork {ArtorId=3,WorkId=3},
            new ArtorWork {ArtorId=3,WorkId=4},

            new ArtorWork {ArtorId=4,WorkId=3},
            new ArtorWork {ArtorId=4,WorkId=4},

            new ArtorWork {ArtorId=6,WorkId=5},
            new ArtorWork {ArtorId=6,WorkId=6},

            new ArtorWork {ArtorId=7,WorkId=10},
            new ArtorWork {ArtorId=7,WorkId=8},

            new ArtorWork {ArtorId=8,WorkId=10},
            new ArtorWork {ArtorId=8,WorkId=11},

            new ArtorWork {ArtorId=9,WorkId=13},

            new ArtorWork {ArtorId=10,WorkId=9},
            new ArtorWork {ArtorId=10,WorkId=12},

            new ArtorWork {ArtorId=11,WorkId=12},
            new ArtorWork {ArtorId=11,WorkId=13},
            new ArtorWork {ArtorId=12,WorkId=13}
        );

        modelBuilder.Entity<TicketArtor>().HasData(
            new TicketArtor {TicketId=1,ArtorId=1},
            new TicketArtor {TicketId=2,ArtorId=3},
            new TicketArtor {TicketId=3,ArtorId=4},
            new TicketArtor {TicketId=4,ArtorId=4},
            new TicketArtor {TicketId=5,ArtorId=6},
            new TicketArtor {TicketId=6,ArtorId=2},

            new TicketArtor {TicketId=7,ArtorId=7},
            new TicketArtor {TicketId=7,ArtorId=8},
            new TicketArtor {TicketId=8,ArtorId=10},
            new TicketArtor {TicketId=8,ArtorId=11},

            new TicketArtor {TicketId=9,ArtorId=7},
            new TicketArtor {TicketId=9,ArtorId=11},

            new TicketArtor {TicketId=10,ArtorId=12},
            new TicketArtor {TicketId=10,ArtorId=11}
        );
    }
}