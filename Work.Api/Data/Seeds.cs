using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Entities;
using Work.Api.Models;

namespace Work.Api.Data
{
    public static class Seeds
    {
        [Obsolete]
        public static void Initialize(WorkDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            //if (context.Companies.Any() && context.Employees.Any() && context.Users.Any() && context.TokenModels.Any())
            //{
            //    return;
            //}

            var companies = new Company[]
            {
                new Company
                {
                    Id = Guid.Parse("620f23d6-6a9c-43b9-a70a-c93cf0096ee5"),
                    Name = "Microsoft",
                    Introduction = "Great Company"
                },
                new Company
                {
                    Id = Guid.Parse("f6ee3f75-8659-4ab1-a1a3-d799b8a8f70c"),
                    Name = "Google",
                    Introduction = "404 Company"
                },
                new Company
                {
                    Id = Guid.Parse("28e2b431-f8b3-4fad-af74-602278ebda00"),
                    Name = "Alipapa",
                    Introduction = "FuBao Company"
                },
                new Company
                {
                    Id = Guid.Parse("aeb13c79-f6d4-4d23-b06b-01d550e2acc6"),
                    Name = "Face Book",
                    Introduction = "Freedown Company"
                },
                new Company
                {
                    Id = Guid.Parse("290d2019-1a83-4afd-b370-7e19f8965c92"),
                    Name = "BaiDu",
                    Introduction = "Ads Company"
                },
                new Company
                {
                    Id = Guid.Parse("8bc932ea-9f54-4da2-a7a1-2e8045a8a5cf"),
                    Name = "Wang Yi",
                    Introduction = "Freedown Company"
                },
                new Company
                {
                    Id = Guid.Parse("6db2af13-fce8-4731-9d8f-92abf87a55b0"),
                    Name = "JingDong",
                    Introduction = "Fast Company"
                },
                new Company
                {
                    Id = Guid.Parse("412300f6-a923-45ee-92c1-581fcf93c21a"),
                    Name = "Xiao Mi",
                    Introduction = "Smart Company"
                },
                new Company
                {
                    Id = Guid.Parse("6b48611b-f96c-4527-aebd-db96e7b02146"),
                    Name = "Bilibili",
                    Introduction = "Acg Company"
                },
                new Company
                {
                    Id = Guid.Parse("f69e131f-1cfb-4e26-8661-79ad9be75ace"),
                    Name = "Zhang Zi Dao",
                    Introduction = "Faker Company"
                }
            };
            foreach(var i in companies)
            {
                context.Companies.Add(i);
            }
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee
                {
                    Id = Guid.Parse("347e2607-04ba-43de-a8df-07154856f824"),
                    CompanyId = Guid.Parse("620f23d6-6a9c-43b9-a70a-c93cf0096ee5"),
                    EmployeeNo = "A001",
                    FirstName = "张",
                    LastName = "一",
                    Gender = Gender.男,
                    BirthOfDate = new DateTime(1990, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("15422d65-3426-4ad0-a815-ce70f4ba5174"),
                    CompanyId = Guid.Parse("620f23d6-6a9c-43b9-a70a-c93cf0096ee5"),
                    EmployeeNo = "A002",
                    FirstName = "张",
                    LastName = "二",
                    Gender = Gender.男,
                    BirthOfDate = new DateTime(1991, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("d454a121-7f98-4641-a996-10f6a02701d8"),
                    CompanyId = Guid.Parse("620f23d6-6a9c-43b9-a70a-c93cf0096ee5"),
                    EmployeeNo = "A003",
                    FirstName = "张",
                    LastName = "三",
                    Gender = Gender.男,
                    BirthOfDate = new DateTime(1992, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("72562ee7-b32b-435b-b91f-c65e487a487d"),
                    CompanyId = Guid.Parse("f6ee3f75-8659-4ab1-a1a3-d799b8a8f70c"),
                    EmployeeNo = "B001",
                    FirstName = "王",
                    LastName = "一",
                    Gender = Gender.男,
                    BirthOfDate = new DateTime(1993, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("0f33e571-69f8-45c4-96d7-734dd775ded4"),
                    CompanyId = Guid.Parse("f6ee3f75-8659-4ab1-a1a3-d799b8a8f70c"),
                    EmployeeNo = "B002",
                    FirstName = "王",
                    LastName = "二",
                    Gender = Gender.男,
                    BirthOfDate = new DateTime(1994, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("9daced10-9473-47aa-bd84-f5f52da7d4a3"),
                    CompanyId = Guid.Parse("f6ee3f75-8659-4ab1-a1a3-d799b8a8f70c"),
                    EmployeeNo = "B003",
                    FirstName = "王",
                    LastName = "三",
                    Gender = Gender.男,
                    BirthOfDate = new DateTime(1995, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("c9ccdadd-325f-4f84-8cec-b668a76047e8"),
                    CompanyId = Guid.Parse("28e2b431-f8b3-4fad-af74-602278ebda00"),
                    EmployeeNo = "C001",
                    FirstName = "李",
                    LastName = "一",
                    Gender = Gender.女,
                    BirthOfDate = new DateTime(1996, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("8b623d8f-3ab9-4941-8625-8fa8405175ef"),
                    CompanyId = Guid.Parse("28e2b431-f8b3-4fad-af74-602278ebda00"),
                    EmployeeNo = "C002",
                    FirstName = "李",
                    LastName = "二",
                    Gender = Gender.女,
                    BirthOfDate = new DateTime(1997, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("3006d592-2fa2-41cf-ba51-9a4f7e705eac"),
                    CompanyId = Guid.Parse("28e2b431-f8b3-4fad-af74-602278ebda00"),
                    EmployeeNo = "C003",
                    FirstName = "李",
                    LastName = "三",
                    Gender = Gender.女,
                    BirthOfDate = new DateTime(1998, 01, 01)
                }
            };
            foreach(var i in employees)
            {
                context.Employees.Add(i);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User
                {
                    Id = 1,
                    UserName = "Admin001",
                    PassWord = "123456"
                },
                new User
                {
                    Id = 2,
                    UserName = "Admin002",
                    PassWord = "abcdef"
                },
                new User
                {
                    Id = 3,
                    UserName = "Leader001",
                    PassWord = "123456abcdef"
                }
            };
            foreach(var i in users)
            {
                context.Users.Add(i);
            }
            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Users ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Users OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }
            //context.SaveChanges();

            var tokenModels = new TokenModel[]
            {
                new TokenModel
                {
                    Uid = 1,
                    Uname = "The Giao",
                    Role = Role.Admin,
                    UserId = 1
                },
                new TokenModel
                {
                    Uid = 2,
                    Uname = "The Shy",
                    Role = Role.Admin,
                    UserId = 2
                },
                new TokenModel
                {
                    Uid = 3,
                    Uname = "The Bug",
                    Role = Role.Leader,
                    UserId = 3
                }
            };
            foreach(var i in tokenModels)
            {
                context.TokenModels.Add(i);
            }
            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.TokenModels ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.TokenModels OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }
            //context.SaveChanges();

            var loginRoles = new LoginRole[]
            {
                new LoginRole
                {
                    Id = 1,
                    RoleName = Role.Admin,
                    Discription = "拥有一切权限"
                },
                new LoginRole
                {
                    Id = 2,
                    RoleName = Role.Leader,
                    Discription = "拥有对Employee的权限"
                }
            };
            foreach(var i in loginRoles)
            {
                context.LoginRoles.Add(i);
            }
            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.LoginRoles ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.LoginRoles OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }

            var urls = new Url[]
            {
                new Url
                {
                    Id = 1,
                    LinkUrl = "/api/companies",
                    RoleId = 1
                },
                new Url
                {
                    Id = 2,
                    LinkUrl = "/api/text",
                    RoleId = 2
                }
            };
            foreach(var i in urls)
            {
                context.Urls.Add(i);
            }
            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Urls ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Urls OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}
