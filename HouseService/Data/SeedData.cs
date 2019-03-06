using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HouseService.Models.Entities;

namespace HouseService.Data
{
    public class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<AppUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            //context.Database.EnsureCreated();
            string[] roleNames = { "Admin", "Manager", "Dispatcher" };
            context.Database.Migrate();
            context.Database.EnsureCreated();
            foreach (var roleName in roleNames)
            {
                var roleExist = await rolemanager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    await rolemanager.CreateAsync(new IdentityRole(roleName));
                }
            }

            /* -- Админ -- */
            var admin = await usermanager.FindByNameAsync("admin");
            if (admin == null)
            {
                var _admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@hcs.com",
                    FirstName = "admin",
                    LastName = ""
                };
                var result = await usermanager.CreateAsync(_admin, "1234567890");
                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(_admin, "Admin");
                    await usermanager.AddToRoleAsync(_admin, "Manager");
                }
            }
            else
            {
                if (!await usermanager.IsInRoleAsync(admin, "Admin"))
                {
                    await usermanager.AddToRoleAsync(admin, "Admin");
                }
                if (!await usermanager.IsInRoleAsync(admin, "Manager"))
                {
                    await usermanager.AddToRoleAsync(admin, "Manager");
                }
            }

            /* -- Менеджер -- */
            var manager = await usermanager.FindByNameAsync("manager");
            if (manager == null)
            {
                var _manager = new AppUser
                {
                    UserName = "manager",
                    Email = "manager@hcs.com",
                    FirstName = "manager",
                    LastName = ""
                };
                var result = await usermanager.CreateAsync(_manager, "1234567891");
                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(_manager, "Manager");
                }
            }
            else
            {
                if (!await usermanager.IsInRoleAsync(manager, "Manager"))
                {
                    await usermanager.AddToRoleAsync(manager, "Manager");
                }
            }

            /* -- Диспетчер -- */
            var dispatcher = await usermanager.FindByNameAsync("dispatcher");
            if (dispatcher == null)
            {
                var _dispatcher = new AppUser
                {
                    UserName = "dispatcher",
                    Email = "dispatcher@hcs.com",
                    FirstName = "dispatcher",
                    LastName = ""
                };
                var result = await usermanager.CreateAsync(_dispatcher, "1234567892");
                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(_dispatcher, "Dispatcher");
                }
            }
            else
            {
                if (!await usermanager.IsInRoleAsync(dispatcher, "Dispatcher"))
                {
                    await usermanager.AddToRoleAsync(dispatcher, "Dispatcher");
                }
            }

            if (!context.Companies.Any())
            {
                /* -- Компании -- */
                context.Companies.Add(new CompanyModel
                {
                    Name = "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ 'Управляющая компания 'ПОЛЁТ''",
                    ShortName = "Управляющая компания 'ПОЛЁТ'",
                    Phone = "8 (903) 147-35-97",
                    LegalAddress = "142407, Московская область, город Ногинск, улица Аэроклубная, д.15",
                    ActualAddress = "142407, Московская область, город Ногинск, улица Дмитрия Михайлова, дом 1 ",
                    MailingAddress = "142407, Московская область, город Ногинск, улица Дмитрия Михайлова, дом 1 ",
                    OGRN = "1155031003269",
                    INN = "5031116124",
                    KPP = "503101001",
                    Description = "Тест 1",
                });
                context.Companies.Add(new CompanyModel
                {
                    Name = "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ 'ТЕПЛОВОДОСЕРВИС'",
                    ShortName = "ООО 'ТВС'",
                    Phone = "8 (496) 514-14-94",
                    LegalAddress = "142400, Россия, МО, г.Ногинск, ул.Рабочая, д.142",
                    ActualAddress = "142400, Россия, МО, г.Ногинск, ул.Рабочая, д.142",
                    MailingAddress = "142400, Россия, МО, г.Ногинск, ул.Рабочая, д.142",
                    OGRN = "1055005902115",
                    INN = "5031060930",
                    KPP = "503101001",
                    Description = "Тест 2",
                });

                context.SaveChanges();

            }


            /* -- Регионы -- */
            Region region1 = null;
            Region region2 = null;
            if (!context.Regions.Any())
            {
                var r1 = context.Regions.Add(new Region { Name = "Ногинский район" });
                var r2 = context.Regions.Add(new Region { Name = "Раменкий район" });

                context.SaveChanges();

                region1 = r1.Entity;
                region2 = r2.Entity;
            }
            /* -- Города -- */
            City city1 = null;
            City city2 = null;
            City city3 = null;
            City city4 = null;
            if (!context.Cities.Any())
            {
                var c1 = context.Cities.Add(new City { Name = "Ногинск", RegionId = region1.Id });
                var c2 = context.Cities.Add(new City { Name = "Старая Купавна", RegionId = region1.Id });
                var c3 = context.Cities.Add(new City { Name = "Электроугли", RegionId = region1.Id });
                var c4 = context.Cities.Add(new City { Name = "Кратово", RegionId = region2.Id });

                context.SaveChanges();

                city1 = c1.Entity;
                city2 = c2.Entity;
                city3 = c3.Entity;
                city4 = c4.Entity;
            }
            /* -- Улицы -- */
            Street street1 = null;
            Street street2 = null;
            Street street3 = null;
            Street street4 = null;
            Street street5 = null;
            Street street6 = null;
            if (!context.Streets.Any())
            {
                var s1 = context.Streets.Add(new Street { Name = "Дмитрия Михайлова", CityId = city1.Id });
                var s2 = context.Streets.Add(new Street { Name = "Климова", CityId = city1.Id });
                var s3 = context.Streets.Add(new Street { Name = "Тестилей", CityId = city1.Id });
                var s4 = context.Streets.Add(new Street { Name = "Советской конституции", CityId = city1.Id });
                var s5 = context.Streets.Add(new Street { Name = "Матросова", CityId = city2.Id });
                var s6 = context.Streets.Add(new Street { Name = "Школьная", CityId = city3.Id });

                context.SaveChanges();

                street1 = s1.Entity;
                street2 = s2.Entity;
                street3 = s3.Entity;
                street4 = s4.Entity;
                street5 = s5.Entity;
                street6 = s6.Entity;
            }
            /* -- Дома -- */
            House house1 = null;
            House house2 = null;
            House house3 = null;
            House house4 = null;
            House house5 = null;
            House house6 = null;
            House house7 = null;
            House house8 = null;
            if (!context.Houses.Any())
            {
                var h1 = context.Houses.Add(new House { Name = "1", StreetId = street1.Id });
                var h2 = context.Houses.Add(new House { Name = "2", StreetId = street1.Id });
                var h3 = context.Houses.Add(new House { Name = "3", StreetId = street1.Id });
                var h4 = context.Houses.Add(new House { Name = "4", StreetId = street1.Id });
                var h5 = context.Houses.Add(new House { Name = "1", StreetId = street2.Id });
                var h6 = context.Houses.Add(new House { Name = "2", StreetId = street2.Id });
                var h7 = context.Houses.Add(new House { Name = "5", StreetId = street4.Id });
                var h8 = context.Houses.Add(new House { Name = "45", StreetId = street6.Id });

                context.SaveChanges();

                house1 = h1.Entity;
                house2 = h2.Entity;
                house3 = h3.Entity;
                house4 = h4.Entity;
                house5 = h5.Entity;
                house6 = h6.Entity;
                house7 = h7.Entity;
                house8 = h8.Entity;
            }
            /* -- Квартиры -- */
            Room room1 = null;
            Room room2 = null;
            Room room3 = null;
            Room room4 = null;
            Room room5 = null;
            Room room6 = null;
            Room room7 = null;
            Room room8 = null;
            Room room9 = null;
            Room room10 = null;
            if (!context.Rooms.Any())
            {
                var r1 = context.Rooms.Add(new Room { Name = "1", HouseId = house1.Id });
                var r2 = context.Rooms.Add(new Room { Name = "2", HouseId = house1.Id });
                var r3 = context.Rooms.Add(new Room { Name = "3", HouseId = house1.Id });
                var r4 = context.Rooms.Add(new Room { Name = "4", HouseId = house1.Id });
                var r5 = context.Rooms.Add(new Room { Name = "5", HouseId = house1.Id });
                var r6 = context.Rooms.Add(new Room { Name = "6", HouseId = house1.Id });
                var r7 = context.Rooms.Add(new Room { Name = "1", HouseId = house2.Id });
                var r8 = context.Rooms.Add(new Room { Name = "2", HouseId = house2.Id });
                var r9 = context.Rooms.Add(new Room { Name = "3", HouseId = house5.Id });
                var r10 = context.Rooms.Add(new Room { Name = "4", HouseId = house8.Id });

                context.SaveChanges();

                room1 = r1.Entity;
                room2 = r2.Entity;
                room3 = r3.Entity;
                room4 = r4.Entity;
                room5 = r5.Entity;
                room6 = r6.Entity;
                room7 = r7.Entity;
                room8 = r8.Entity;
                room9 = r9.Entity;
                room10 = r10.Entity;
            }

            /* -- Адреса -- */
            AdressModel adress1 = null;
            AdressModel adress2 = null;
            AdressModel adress3 = null;
            AdressModel adress4 = null;
            AdressModel adress5 = null;
            if (!context.Adresses.Any())
            {
                var a1 = context.Adresses.Add(new AdressModel
                {
                    CompanyId = context.Companies.Where(c => c.Name == "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ 'Управляющая компания 'ПОЛЁТ''").SingleOrDefault().Id,
                    RegionId = region1.Id,
                    CityId = city1.Id,
                    StreetId = street1.Id,
                    HouseId = house1.Id,
                    RoomId = room1.Id
                });
                var a2 = context.Adresses.Add(new AdressModel
                {
                    CompanyId = context.Companies.Where(c => c.Name == "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ 'Управляющая компания 'ПОЛЁТ''").SingleOrDefault().Id,
                    RegionId = region1.Id,
                    CityId = city1.Id,
                    StreetId = street1.Id,
                    HouseId = house1.Id,
                    RoomId = room2.Id
                });
                var a3 = context.Adresses.Add(new AdressModel
                {
                    CompanyId = context.Companies.Where(c => c.Name == "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ 'Управляющая компания 'ПОЛЁТ''").SingleOrDefault().Id,
                    RegionId = region1.Id,
                    CityId = city1.Id,
                    StreetId = street1.Id,
                    HouseId = house1.Id,
                    RoomId = room3.Id
                });
                var a4 = context.Adresses.Add(new AdressModel
                {
                    CompanyId = context.Companies.Where(c => c.Name == "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ 'ТЕПЛОВОДОСЕРВИС'").SingleOrDefault().Id,
                    RegionId = region1.Id,
                    CityId = city3.Id,
                    StreetId = street6.Id,
                    HouseId = house8.Id,
                    RoomId = room10.Id
                });
                var a5 = context.Adresses.Add(new AdressModel
                {
                    CompanyId = context.Companies.Where(c => c.Name == "ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ 'ТЕПЛОВОДОСЕРВИС'").SingleOrDefault().Id,
                    RegionId = region1.Id,
                    CityId = city1.Id,
                    StreetId = street2.Id,
                    HouseId = house5.Id,
                    RoomId = room9.Id
                });


                context.SaveChanges();

                adress1 = a1.Entity;
                adress2 = a2.Entity;
                adress3 = a3.Entity;
                adress4 = a4.Entity;
                adress5 = a5.Entity;
            }


            if (!context.RequestStatuses.Any())
            {

                context.RequestStatuses.Add(new RequestStatusModel { Name = "Создана" });
                context.RequestStatuses.Add(new RequestStatusModel { Name = "В работе" });
                context.RequestStatuses.Add(new RequestStatusModel { Name = "Закрыта" });

                context.SaveChanges();
            }

            /* -- Типы заявок -- */
            if (!context.RequestTypes.Any())
            {

                context.RequestTypes.Add(new RequestTypeModel { Name = "Электричество" });
                context.RequestTypes.Add(new RequestTypeModel { Name = "Вода" });
                context.RequestTypes.Add(new RequestTypeModel { Name = "Газ" });
                context.RequestTypes.Add(new RequestTypeModel { Name = "Уличные работы" });

                context.SaveChanges();
            }
            //
            Contact contact1 = null;
            if (!context.Contacts.Any())
            {
                var c1 = context.Contacts.Add(new Contact
                {
                    FirstName="Марья", LastName="Иванова", MiddleName="Ивановна", ContactType= Models.ContactType.Resident, Phone="+79553451122"
                });
                context.SaveChanges();
                contact1 = c1.Entity;
            }
            /* -- Заявки -- */
            if (!context.Requests.Any())
            {
                var _manager = await usermanager.FindByNameAsync("manager");
                var managerId = _manager.Id;


                var request = new Request
                {
                    RequestTypeId = context.RequestTypes.Where(c => c.Name == "Вода").SingleOrDefault().Id,
                    RequestStatusId = context.RequestStatuses.Where(c => c.Name == "Создана").SingleOrDefault().Id,
                    StartDate = DateTime.Now,
                    //Contact = "Дмитрий Владимирович 8(888)728-29-22",
                    Description = "Прорвало трубу в квартире",
                    AdressId = adress1.Id,
                    UserId = managerId,
                    ContactId = contact1.Id
                };
                context.Requests.Add(request);
                context.SaveChanges();

            }

        }
    }
}
