using System.Data;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data;

public static class ModelBuilderExtention
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>().HasData(
            new Activity {Id=1,Name="Konser",Url="konser"},
            new Activity {Id=2,Name="Söyleşi",Url="soylesi"},
            new Activity {Id=3,Name="Konuşma",Url="konusma"}
        );

        modelBuilder.Entity<Address>().HasData(
            new Address {Id=1,Title="Harbiye",City="İstanbul",Country="Türkiye"},
            new Address {Id=2,Title="Galata Port",City="İstanbul",Country="Türkiye"},
            new Address {Id=3,Title="Fuar Kültürpark",City="İzmir",Country="Türkiye"},
            new Address {Id=4,Title="Rockefeller Center",City="New York",Country="USA"}
        );

        modelBuilder.Entity<Artor>().HasData(
            new Artor {Id=1,Name="Mercan Dede",Url="mercan-dede"},
            new Artor {Id=2,Name="Serra Yılmaz",Url="serra-yilmaz"},
            new Artor {Id=3,Name="Sezen Aksu",Url="sezen-aksu"},
            new Artor {Id=4,Name="Ajda Pekkan",Url="ajda-pekkan"},
            new Artor {Id=5,Name="Carole King",Url="carole-king"},
            new Artor {Id=6,Name="Betül Mardin",Url="betul-mardin"}
        );

        modelBuilder.Entity<Category>().HasData(
            new Category {Id=1,Name="Rock",Url="rock"},
            new Category {Id=2,Name="Jazz",Url="jazz"},
            new Category {Id=3,Name="Pop",Url="pop"},
            new Category {Id=4,Name="Klasik",Url="klasik"},
            new Category {Id=5,Name="Rap",Url="rap"},
            new Category {Id=6,Name="Kalkla İlişkiler",Url="halkla-iliskiler"}
        );

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket {Id=1,SeatNumber=1,Limit=100,Name="Mercan Dede Konseri",Url="mercan-dede-konseri",Price=1000,EventDate=new DateTime(2026, 12, 01, 21, 00, 10),AddressId=1,ActivityId=1},

            new Ticket {Id=2,SeatNumber=2,Limit=100,Name="Sezen Aksu Konseri",Url="sezen-aksu-konseri",Price=1500,EventDate=new DateTime(2026, 10, 01, 21, 00, 10),AddressId=1,ActivityId=1},

            new Ticket {Id=3,SeatNumber=3,Limit=100,Name="Ajda Pekkan Konseri",Url="ajda-pekkan-konseri",Price=1500,EventDate=new DateTime(2026, 06, 01, 21, 00, 10),AddressId=2,ActivityId=1},

            new Ticket {Id=4,SeatNumber=4,Limit=100,Name="Ajda Pekkan Konseri",Url="ajda-pekkan-konseri",
            Price=1500,EventDate=new DateTime(2026, 05, 01, 21, 00, 10),AddressId=2,ActivityId=1},

            new Ticket {Id=5,SeatNumber=5,Limit=100,Name="Betül Mardin Konuşma",Url="betul-mardin-konusma",
            Price=1200,EventDate=new DateTime(2026, 04, 01, 21, 00, 10),AddressId=3,ActivityId=3},

            new Ticket {Id=6,SeatNumber=1,Limit=100,Name="Serra Yılmaz Konseri",Url="serra-yilmaz-konseri",Price=800,EventDate=new DateTime(2026, 03, 23, 23, 10, 10),AddressId=1,ActivityId=1}
        );

        modelBuilder.Entity<Work>().HasData(
            new Work {Id=1,Name="Sufi Dreams",Url="sufi-dreams",Description="Sufi Dreams"},
            new Work {Id=2,Name="Nar",Url="nar",Description="Nar"},

            new Work {Id=3,Name="Gülümse",Url="gulumse",Description="Gülümse"},
            new Work {Id=4,Name="Firuze",Url="firuze",Description="Firuze"},

            new Work {Id=5,Name="Palavra Palavra",Url="palavra",Description="Palavra Palavra"},
            new Work {Id=6,Name="Neler Edeceğim",Url="neler-edecegim",Description="Neler Edeceğim"},

            new Work {Id=7,Name="Discover Turkey",Url="discover-turkey",Description="Discover Turkey"},
            new Work {Id=8,Name="Değerli Dostum",Url="degerli-dostum",Description="Değerli Dostum"}
        );

        modelBuilder.Entity<ActivityCategory>().HasData(
            new ActivityCategory {ActivityId=1,CategoryId=1},
            new ActivityCategory {ActivityId=1,CategoryId=2},
            new ActivityCategory {ActivityId=1,CategoryId=3},
            new ActivityCategory {ActivityId=2,CategoryId=6},
            new ActivityCategory {ActivityId=3,CategoryId=6}
        );

        modelBuilder.Entity<ArtorWork>().HasData(
            new ArtorWork {ArtorId=1,WorkId=1},
            new ArtorWork {ArtorId=1,WorkId=2},

            new ArtorWork {ArtorId=3,WorkId=3},
            new ArtorWork {ArtorId=3,WorkId=4},

            new ArtorWork {ArtorId=4,WorkId=3},
            new ArtorWork {ArtorId=4,WorkId=4},

            new ArtorWork {ArtorId=6,WorkId=5},
            new ArtorWork {ArtorId=6,WorkId=6}
        );

        modelBuilder.Entity<TicketArtor>().HasData(
            new TicketArtor {TicketId=1,ArtorId=1},
            new TicketArtor {TicketId=2,ArtorId=3},
            new TicketArtor {TicketId=3,ArtorId=4},
            new TicketArtor {TicketId=4,ArtorId=4},
            new TicketArtor {TicketId=5,ArtorId=6}
        );
    }
}
