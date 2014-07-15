using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using Autofac;
using ShareMarket.BusinessLogic.Libs;
using ShareMarket.Core;
using ShareMarket.Core.Enums;
using ShareMarket.DataAccess;

using ShareMarket.Utility.Utilities;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;

namespace ShareMarket.Web
{
    public static class DatabaseConfig
    {
        public static void ConfigureDataBase()
        {
            var ctx1 = new ShareMarketDbContext();
            ctx1.Database.Initialize(true);

          
            if (ctx1.Database.Exists())
            {
                WebSecurity.InitializeDatabaseConnection("dbConnection", "UserProfile", "Id", "UserName",
                    autoCreateTables: true);
            }
        
            // Enumerate on roles : add them if they don't exists.
            EnumUtil.GetValues<RoleType>().ForEach((item) =>
            {
                if (! System.Web.Security.Roles.RoleExists(item.ToString()))
                {
                    System.Web.Security.Roles.CreateRole(item.ToString());
                }
            });

            using (HomeLib homeLib = GlobalUtil.Container.Resolve<HomeLib>())
            {
                homeLib.CreateUser();
            }

            //Seed();
        }

        ///// <summary>
        ///// Seed data with raw data in master tables.
        ///// </summary>
        //private static void Seed()
        //{
        //    IContainer container = GlobalUtil.Container;
        //    try
        //    {
        //        string adminEmail = "techadmin@techgrains.com";
        //        IWebSecurity webSecurity = container.Resolve<IWebSecurity>();

        //        using (GoByMobilePortalDbContext goByMobilePortalDbContext = new GoByMobilePortalDbContext())
        //        {

        //            #region "Country Seed"
        //            //Seed Country
        //            goByMobilePortalDbContext.Countries.AddOrUpdate(x => x.Name,
        //                new Country()
        //                  {
        //                      Name = "India",
        //                  });
        //            goByMobilePortalDbContext.SaveChanges();

        //            #endregion

        //            #region "State Seed"


        //            //Seed State
        //            goByMobilePortalDbContext.States.AddOrUpdate(x => x.Name,
        //                new State()
        //                {
        //                    Name = "Gujarat",
        //                    CountryId = 1
        //                });
        //            goByMobilePortalDbContext.SaveChanges();

        //            #endregion

        //            #region "City Seed"

        //            //Seed City
        //            if (goByMobilePortalDbContext.CityMasters.FirstOrDefault(x => x.Name.Equals("Baroda")) == null)
        //            {
        //                goByMobilePortalDbContext.CityMasters.Add(new CityMaster()
        //                    {
        //                        Name = "Baroda",
        //                        StateId = 1,
        //                        Medical = true
        //                    });
        //            }
        //            goByMobilePortalDbContext.SaveChanges();

        //            #endregion

        //            #region "Settings Seed"

        //            //Seed Settings
        //            List<KeyValuePair<string, string>> settings = new List<KeyValuePair<string, string>>()
        //           {
        //               new KeyValuePair<string, string>("GoByMobilePortal.AdminEmail",adminEmail),
        //               new KeyValuePair<string, string>("GoByMobilePortal.SqlScriptsPath","~//SqlScripts"),
        //               new KeyValuePair<string, string>("GoByMobilePortal.UploadedEntityImages","~/UploadedEntityImages"),
        //               new KeyValuePair<string, string>("Hospital.ImageSize","1000"),
        //               new KeyValuePair<string, string>("Entity.ProfileImageSize","1000"),
        //               new KeyValuePair<string, string>("Entity.ProfileImageDimension","100*100"),
        //               new KeyValuePair<string, string>("Hospital.ImageDimension","100*100")
        //           };
        //            settings.ForEach(s =>
        //            {
        //                if (goByMobilePortalDbContext.Settings.FirstOrDefault(x => x.Name.Equals(s.Key)) == null)
        //                {
        //                    goByMobilePortalDbContext.Settings.Add(new Setting()
        //                    {
        //                        Name = s.Key,
        //                        Value = s.Value
        //                    });
        //                }
        //            });

        //            goByMobilePortalDbContext.SaveChanges();


        //            #endregion

        //            #region "Entity Attribute Master Seed"

        //            List<EntityAttributesMaster> attributes = new List<EntityAttributesMaster>()
        //            {
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 1,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.AwardsAndRecognitions,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 12,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 2,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Education,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 13,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 34,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Buffet,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 18,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 41,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.FixedMeal,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 19,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 3,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Experience,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 14,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 4,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Memberships,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 15,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 5,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Registrations,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 16,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 6,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Specialization,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 11,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[\"Orthopaedic\",\"General Physician\",\"Paediatrician\"]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 39,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Facilities,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 16,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[\"Air Conditioner\",\"Dine In /  Out Door \",\"Live Music\",\"Sodexo / Ticket Meal Accepted\",\"Credit / Debit Card Accepted\",\"Wifi\",\"Lounge\",\"Catering Services\"]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 40,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Cuisine,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 17,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[\"Awadhi\",\"Punjabi\",\"Gujarati\",\"Gujarati Thali\",\"Chinese\",\"Fast Food\"]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 7,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Services,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 17,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },

        //                 new EntityAttributesMaster()
        //                 {
        //                     Id = 8,
        //                     AttributeType = Attribute.EntityDetail,
        //                     AttributeName = AttributeNameConstants.MetaTitle,
        //                     DataType = InputType.Text,
        //                     DisplayOrder = 3,
        //                     Type = EntityType.Doctor,
        //                     ListValues = "[]"
        //                 },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 9,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.MetaKeyword,
        //                    DataType = InputType.Text,
        //                    DisplayOrder = 4,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 10,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.MetaDescription,
        //                    DataType = InputType.MultilineText,
        //                    DisplayOrder = 5,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                 {
        //                     Id = 35,
        //                     AttributeType = Attribute.EntityDetail,
        //                     AttributeName = AttributeNameConstants.MetaTitle,
        //                     DataType = InputType.Text,
        //                     DisplayOrder = 1,
        //                     Type = EntityType.Restaurant,
        //                     ListValues = "[]"
        //                 },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 36,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.MetaKeyword,
        //                    DataType = InputType.Text,
        //                    DisplayOrder = 2,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 37,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.MetaDescription,
        //                    DataType = InputType.MultilineText,
        //                    DisplayOrder = 3,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                 new EntityAttributesMaster()
        //                 {
        //                     Id = 11,
        //                     AttributeType = Attribute.Entity,
        //                     AttributeName = AttributeNameConstants.Title,
        //                     DataType = InputType.Text,
        //                     DisplayOrder = 1,
        //                     Type = EntityType.Doctor,
        //                     ListValues = "[]"
        //                 },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 12,
        //                    AttributeType = Attribute.Entity,
        //                    AttributeName = AttributeNameConstants.SubTitle,
        //                    DataType = InputType.Text,
        //                    DisplayOrder = 2,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 13,
        //                    AttributeType = Attribute.Entity,
        //                    AttributeName = AttributeNameConstants.EstablishedSince,
        //                    DataType = InputType.Year,
        //                    DisplayOrder = 7,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 14,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Services,
        //                    DataType = InputType.List,
        //                    DisplayOrder = 5,
        //                    Type = EntityType.Hospital,
        //                    ListValues = "[]"
        //                },
        //                 new EntityAttributesMaster()
        //                 {
        //                     Id = 15,
        //                     AttributeType = Attribute.EntityDetail,
        //                     AttributeName = AttributeNameConstants.MetaTitle,
        //                     DataType = InputType.Text,
        //                     DisplayOrder = 1,
        //                     Type = EntityType.Hospital,
        //                     ListValues = "[]"
        //                 },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 16,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.MetaKeyword,
        //                    DataType = InputType.Text,
        //                    DisplayOrder = 2,
        //                    Type = EntityType.Hospital,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 17,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.MetaDescription,
        //                    DataType = InputType.MultilineText,
        //                    DisplayOrder = 3,
        //                    Type = EntityType.Hospital,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 18,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.AboutText,
        //                    DataType = InputType.MultilineText,
        //                    DisplayOrder = 4,
        //                    Type = EntityType.Hospital,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 19,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.AboutText,
        //                    DataType = InputType.MultilineText,
        //                    DisplayOrder = 6,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 38,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.AboutText,
        //                    DataType = InputType.MultilineText,
        //                    DisplayOrder = 7,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                 new EntityAttributesMaster()
        //                {
        //                    Id = 20,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.HomeVisiting,
        //                    DataType = InputType.Checkbox,
        //                    DisplayOrder = 8,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 23,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Veg,
        //                    DataType = InputType.Checkbox,
        //                    DisplayOrder = 9,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 24,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.NonVeg,
        //                    DataType = InputType.Checkbox,
        //                    DisplayOrder = 10,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 25,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.FixedMeal,
        //                    DataType = InputType.Checkbox,
        //                    DisplayOrder = 11,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 26,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Buffet,
        //                    DataType = InputType.Checkbox,
        //                    DisplayOrder = 12,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 27,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.TableBooking,
        //                    DataType = InputType.Checkbox,
        //                    DisplayOrder = 13,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                new EntityAttributesMaster()
        //                {
        //                    Id = 28,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.HomeDelivery,
        //                    DataType = InputType.Checkbox,
        //                    DisplayOrder = 14,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                },
        //                 new EntityAttributesMaster()
        //                {
        //                    Id = 29,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Banner,
        //                    DataType = InputType.Image,
        //                    DisplayOrder = 15,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                    },
        //                 new EntityAttributesMaster()
        //                {
        //                    Id = 21,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.Image,
        //                    DataType = InputType.Text,
        //                    DisplayOrder = 10,
        //                    Type = EntityType.Doctor,
        //                    ListValues = "[]"
        //                    }
        //                    , 
        //                    new EntityAttributesMaster()
        //                {
        //                    Id = 30,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.HomeDeliveryCondition,
        //                    DataType = InputType.Text,
        //                    DisplayOrder = 15,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                    },
        //                     new EntityAttributesMaster()
        //                {
        //                    Id = 31,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.ContactPersonName,
        //                    DataType = InputType.Text,
        //                    DisplayOrder = 4,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                    },
        //                     new EntityAttributesMaster()
        //                {
        //                    Id = 32,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.ContactNo,
        //                    DataType = InputType.Text,
        //                    DisplayOrder = 5,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                    },
        //                     new EntityAttributesMaster()
        //                {
        //                    Id = 33,
        //                    AttributeType = Attribute.EntityDetail,
        //                    AttributeName = AttributeNameConstants.MenuUpdatedDate,
        //                    DataType = InputType.Date,
        //                    DisplayOrder = 6,
        //                    Type = EntityType.Restaurant,
        //                    ListValues = "[]"
        //                    }
        //            };
        //            //Seed EntityAttributesMasters
        //            attributes.ForEach(a =>
        //                {
        //                    if (goByMobilePortalDbContext.EntityAttributesMasters.FirstOrDefault(x => x.Id.Equals(a.Id)) == null)
        //                    {
        //                        goByMobilePortalDbContext.EntityAttributesMasters.Add(a);
        //                    }
        //                });

        //            goByMobilePortalDbContext.SaveChanges();

        //            #endregion
        //        }

        //        if (!webSecurity.UserExists(adminEmail))
        //        {
        //            IRole role = container.Resolve<IRole>();

        //            //Add Admin user
        //            webSecurity.CreateUserAndAccount(adminEmail, "test#123", new
        //            {
        //                Name = "Techadmin",
        //                IsDeleted = false,
        //                CreatedOn = DateUtil.GetCurrentDateTime(),
        //            });

        //            //Add Admin to Role
        //            role.AddUserToRole(adminEmail, Role.TechAdmin.ToString());
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ex.LogError(typeof(DatabaseConfig));
        //    }
        //}

        ///// <summary>
        ///// Create or update sql scripts.
        ///// </summary>
        //public static void CreateOrUpdateSqlScripts()
        //{
        //    try
        //    {
        //        IAppSettingsHelper appSettingsHelper = GlobalUtil.Container.Resolve<IAppSettingsHelper>();
        //        using (SqlConnection con = new SqlConnection { ConnectionString = ConfigurationManager.ConnectionStrings["GoByMobilePortalDbContext"].ConnectionString })
        //        {
        //            con.Open();
        //            string sqlScriptsPath = HttpContext.Current.Server.MapPath(appSettingsHelper.SqlScriptsPath);
        //            if (Directory.Exists(sqlScriptsPath))
        //            {
        //                foreach (var item in Directory.EnumerateFiles(sqlScriptsPath))
        //                {
        //                    FileInfo file = new FileInfo(item);
        //                    string scriptText = file.OpenText().ReadToEnd();
        //                    string[] scriptsArraySplittedWithGo = scriptText.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
        //                    foreach (var script in scriptsArraySplittedWithGo)
        //                    {
        //                        SqlCommand sqlComm = new SqlCommand(script, con);
        //                        sqlComm.ExecuteNonQuery();
        //                    }

        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.LogError(typeof(DatabaseConfig));
        //    }
        //}
    }
}