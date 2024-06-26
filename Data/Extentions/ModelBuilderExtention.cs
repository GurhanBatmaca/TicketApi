﻿using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data;

public static class ModelBuilderExtention
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>().HasData(
            new Activity {Id=1,Name="Konser",Url="konser",ImageUrl="activityDefault.jpg"},
            new Activity {Id=2,Name="Söyleşi",Url="soylesi",ImageUrl="activityDefault.jpg"},
            new Activity {Id=3,Name="Konuşma",Url="konusma",ImageUrl="activityDefault.jpg"},
            new Activity {Id=4,Name="Tiyatro",Url="tiyatro",ImageUrl="activityDefault.jpg"},
            new Activity {Id=5,Name="Sinema",Url="sinema",ImageUrl="activityDefault.jpg"}
        );

        modelBuilder.Entity<Address>().HasData(
            new Address {Id=1,Title="Harbiye",CityId=34,Url="harbiye",ImageUrl="addressDefault.jpg"},
            new Address {Id=2,Title="Galata Port",CityId=34,Url="galata-port",ImageUrl="addressDefault"},
            new Address {Id=3,Title="Fuar Kültürpark",CityId=35,Url="fuar-kulturpark",ImageUrl="addressDefault"},
            new Address {Id=4,Title="Ankara Kültürpark",CityId=6,Url="ankara-kulturpark",ImageUrl="addressDefault"},

            new Address {Id=5,Title="İstanbul Devlet Tiyatrosu",CityId=34,ImageUrl="addressDefault"},
            new Address {Id=6,Title="Ankara Sanat Tiyatrosu",CityId=6,ImageUrl="addressDefault"},
            new Address {Id=7,Title="Cinetime Sinemaları",CityId=34,ImageUrl="addressDefault"}
        );

        modelBuilder.Entity<Artor>().HasData(
            new Artor {Id=1,Name="Mercan Dede",Url="mercan-dede",ImageUrl="artorDefault.jpg"},
            new Artor {Id=2,Name="Serra Yılmaz",Url="serra-yilmaz",ImageUrl="artorDefault"},
            new Artor {Id=3,Name="Sezen Aksu",Url="sezen-aksu",ImageUrl="artorDefault"},
            new Artor {Id=4,Name="Ajda Pekkan",Url="ajda-pekkan",ImageUrl="artorDefault"},
            new Artor {Id=5,Name="Carole King",Url="carole-king",ImageUrl="artorDefault"},
            new Artor {Id=6,Name="Betül Mardin",Url="betul-mardin",ImageUrl="artorDefault"},

            new Artor {Id=7,Name="Haluk Bilginer",Url="haluk-bilginer",ImageUrl="artorDefault"},
            new Artor {Id=8,Name="Demet Akbağ",Url="demek-akbag",ImageUrl="artorDefault"},
            new Artor {Id=9,Name="Altan Erkekli",Url="altan-erkekli",ImageUrl="artorDefault"},

            new Artor {Id=10,Name="Cem Yılmaz",Url="cem-yilmaz",ImageUrl="artorDefault"},
            new Artor {Id=11,Name="Gülse Birsel",Url="gulse-birsel",ImageUrl="artorDefault"},
            new Artor {Id=12,Name="Aras Bulut İylemli",Url="aras-bulut-iylemli",ImageUrl="artorDefault"}
        );

        modelBuilder.Entity<Category>().HasData(
            new Category {Id=1,Name="Rock",Url="rock",ImageUrl="categoryDefault.jpg"},
            new Category {Id=2,Name="Jazz",Url="jazz",ImageUrl="categoryDefault.jpg"},
            new Category {Id=3,Name="Pop",Url="pop",ImageUrl="categoryDefault.jpg"},
            new Category {Id=4,Name="Klasik",Url="klasik",ImageUrl="categoryDefault.jpg"},
            new Category {Id=5,Name="Rap",Url="rap",ImageUrl="categoryDefault.jpg"},
            new Category {Id=6,Name="Halkla İlişkiler",Url="halkla-iliskiler",ImageUrl="categoryDefault.jpg"},

            new Category {Id=7,Name="Komedi",Url="komedi",ImageUrl="categoryDefault.jpg"},
            new Category {Id=8,Name="Macera",Url="macera",ImageUrl="categoryDefault.jpg"},
            new Category {Id=9,Name="Drama",Url="drama",ImageUrl="categoryDefault.jpg"},
            new Category {Id=10,Name="Tarih",Url="tarih",ImageUrl="categoryDefault.jpg"}
        );

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket {Id=1,Limit=1000,Name="Mercan Dede Konseri",Url="mercan-dede-konseri",Price=1000,EventDate=new DateTime(2026, 12, 01, 21, 00, 10),AddressId=1,ActivityId=1,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=2,Limit=1100,Name="Sezen Aksu Konseri",Url="sezen-aksu-konseri",Price=1500,EventDate=new DateTime(2026, 10, 01, 21, 00, 10),AddressId=1,ActivityId=1,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=3,Limit=800,Name="Ajda Pekkan Konseri",Url="ajda-pekkan-konseri",Price=1500,EventDate=new DateTime(2026, 06, 01, 21, 00, 10),AddressId=2,ActivityId=1,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=4,Limit=1000,Name="Ajda Pekkan Konseri",Url="ajda-pekkan-konseri",
            Price=1500,EventDate=new DateTime(2026, 05, 01, 21, 00, 10),AddressId=2,ActivityId=1,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=5,Limit=1000,Name="Betül Mardin Konuşma",Url="betul-mardin-konusma",
            Price=1200,EventDate=new DateTime(2026, 04, 01, 21, 00, 10),AddressId=3,ActivityId=3,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=6,Limit=1000,Name="Serra Yılmaz Konseri",Url="serra-yilmaz-konseri",Price=800,EventDate=new DateTime(2026, 03, 23, 23, 10, 10),AddressId=1,ActivityId=1,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=7,Limit=1000,Name="Gülmeyen Gülümseme Tiyatro Oyunu",Url="gulmeyen-gulumseme-tiyatro-oyunu",Price=800,EventDate=new DateTime(2026, 02, 23, 23, 10, 10),AddressId=5,ActivityId=4,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=8,Limit=1000,Name="Kaos Kafe Tiyatro Oyunu",Url="kaos-kafe-tiyatro-oyunu",Price=500,EventDate=new DateTime(2026, 02, 20, 23, 10, 10),AddressId=6,ActivityId=4,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=9,Limit=1000,Name="Rüya Takipçileri Sinama Filmi",Url="ruya-takipcileri-sinama-filmi",Price=500,EventDate=new DateTime(2026, 01, 23, 23, 10, 10),AddressId=7,ActivityId=5,ImageUrl="ticketDefault.jpg"},

            new Ticket {Id=10,Limit=1000,Name="Kuantum Kediler Sinama Filmi",Url="kuantum-kediler-sinama-filmi",Price=500,EventDate=new DateTime(2026, 01, 20, 23, 10, 10),AddressId=7,ActivityId=5,ImageUrl="ticketDefault.jpg"}
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

        modelBuilder.Entity<City>().HasData(
            new City {Id = 1 ,PlateNumber = 1,Name="Adana",Url="adana",ImageUrl="cityDefault.jpg"},
            new City {Id = 6 ,PlateNumber = 6,Name="Ankara",Url="ankara",ImageUrl="cityDefault.jpg"},
            new City {Id = 7 ,PlateNumber = 7,Name="Antalya",Url="antalya",ImageUrl="cityDefault.jpg"},
            new City {Id = 34 ,PlateNumber = 34,Name="İstanbul",Url="istanbul",ImageUrl="cityDefault.jpg"},
            new City {Id = 35 ,PlateNumber = 35,Name="İzmir",Url="izmir",ImageUrl="cityDefault.jpg"}
        );

        modelBuilder.Entity<SeatInfo>().HasData(
            new SeatInfo {Id=1,FrontView=100,MiddleView=300,BackView=600,TicketId=1},
            new SeatInfo {Id=2,FrontView=150,MiddleView=250,BackView=700,TicketId=2},
            new SeatInfo {Id=3,FrontView=100,MiddleView=200,BackView=500,TicketId=3},
            new SeatInfo {Id=4,FrontView=100,MiddleView=300,BackView=600,TicketId=4},
            new SeatInfo {Id=5,FrontView=100,MiddleView=300,BackView=600,TicketId=5},
            new SeatInfo {Id=6,FrontView=100,MiddleView=300,BackView=600,TicketId=6},
            new SeatInfo {Id=7,FrontView=100,MiddleView=300,BackView=600,TicketId=7},
            new SeatInfo {Id=8,FrontView=100,MiddleView=300,BackView=600,TicketId=8},
            new SeatInfo {Id=9,FrontView=100,MiddleView=300,BackView=600,TicketId=9},
            new SeatInfo {Id=10,FrontView=100,MiddleView=300,BackView=600,TicketId=10}
        );

        // modelBuilder.Entity<ReservedSeat>().HasData(
        //     new ReservedSeat {Id=1,SeatNumber=1,TicketId=1},
        //     new ReservedSeat {Id=2,SeatNumber=2,TicketId=1},
        //     new ReservedSeat {Id=3,SeatNumber=3,TicketId=1},
        //     new ReservedSeat {Id=4,SeatNumber=4,TicketId=1},

        //     new ReservedSeat {Id=5,SeatNumber=5,TicketId=1},
        //     new ReservedSeat {Id=6,SeatNumber=6,TicketId=1},
        //     new ReservedSeat {Id=7,SeatNumber=7,TicketId=1}
        // );
    }
}