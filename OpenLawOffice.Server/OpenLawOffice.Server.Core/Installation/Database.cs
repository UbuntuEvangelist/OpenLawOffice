﻿using System;
using System.Data;
using ServiceStack.OrmLite;

namespace OpenLawOffice.Server.Core.Installation
{
    public class Database
    {
        public void Run()
        {
            using (IDbConnection conn = Core.Database.Instance.OpenConnection())
            {
                conn.CreateTableIfNotExists<DBOs.Security.User>();
                conn.CreateTableIfNotExists<DBOs.Security.Area>();
                conn.CreateTableIfNotExists<DBOs.Security.AreaAcl>();
                conn.CreateTableIfNotExists<DBOs.Security.SecuredResource>();
                conn.CreateTableIfNotExists<DBOs.Security.SecuredResourceAcl>();
                conn.CreateTableIfNotExists<DBOs.Tagging.TagCategory>();
                conn.CreateTableIfNotExists<DBOs.Matters.Matter>();
                conn.CreateTableIfNotExists<DBOs.Matters.MatterTag>();
                conn.CreateTableIfNotExists<DBOs.Matters.ResponsibleUser>();
                conn.CreateTableIfNotExists<DBOs.Contacts.Contact>();
                conn.CreateTableIfNotExists<DBOs.Matters.MatterContact>();

                DBOs.Security.User dbUser = conn.QuerySingle<DBOs.Security.User>(new { Username = "TestUser" });
                // == "a" on before client hash
                string pw = Services.Security.Authentication.ServerHashPassword("1F40FC92DA241694750979EE6CF582F2D5D7D28E18335DE05ABC54D0560E0F5302860C652BF08D560252AA5E74210546F369FBBBCE8C12CFC7957B2652FE9A75", "0123456789");
                
                if (dbUser == null)
                {
                    dbUser = new DBOs.Security.User()
                    {
                        Username = "TestUser",
                        Password = pw,
                        PasswordSalt = "0123456789",
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.User>(dbUser);
                    dbUser.Id = (int)conn.GetLastInsertId();
                }

                #region Areas

                DBOs.Security.Area dbSecurity = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Security" });
                if (dbSecurity == null)
                {
                    dbSecurity = new DBOs.Security.Area()
                    {
                        Name = "Security",
                        Description = "Security Group",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbSecurity);
                    dbSecurity.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaUser = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Security.User" });
                if (dbAreaUser == null)
                {
                    dbAreaUser = new DBOs.Security.Area()
                    {
                        ParentId = dbSecurity.Id,
                        Name = "Security.User",
                        Description = "System users",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaUser);
                    dbAreaUser.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaArea = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Security.Area" });
                if (dbAreaArea == null)
                {
                    dbAreaArea = new DBOs.Security.Area()
                    {
                        ParentId = dbSecurity.Id,
                        Name = "Security.Area",
                        Description = "System areas",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaArea);
                    dbAreaArea.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaAreaAcl = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Security.AreaAcl" });
                if (dbAreaAreaAcl == null)
                {
                    dbAreaAreaAcl = new DBOs.Security.Area()
                    {
                        ParentId = dbSecurity.Id,
                        Name = "Security.AreaAcl",
                        Description = "Acls for system areas",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaAreaAcl);
                    dbAreaAreaAcl.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaSecuredResource = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Security.SecuredResource" });
                if (dbAreaSecuredResource == null)
                {
                    dbAreaSecuredResource = new DBOs.Security.Area()
                    {
                        ParentId = dbSecurity.Id,
                        Name = "Security.SecuredResource",
                        Description = "System secured resources",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaSecuredResource);
                    dbAreaSecuredResource.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaSecuredResourceAcl = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Security.SecuredResourceAcl" });
                if (dbAreaSecuredResourceAcl == null)
                {
                    dbAreaSecuredResourceAcl = new DBOs.Security.Area()
                    {
                        ParentId = dbSecurity.Id,
                        Name = "Security.SecuredResourceAcl",
                        Description = "Acls for secured system resources",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaSecuredResourceAcl);
                    dbAreaSecuredResourceAcl.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaTagging = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Tagging" });
                if (dbAreaTagging == null)
                {
                    dbAreaTagging = new DBOs.Security.Area()
                    {
                        Name = "Tagging",
                        Description = "Tagging",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaTagging);
                    dbAreaTagging.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaTaggingTagCategory = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Tagging.TagCategory" });
                if (dbAreaTaggingTagCategory == null)
                {
                    dbAreaTaggingTagCategory = new DBOs.Security.Area()
                    {
                        ParentId = dbAreaTagging.Id,
                        Name = "Tagging.TagCategory",
                        Description = "Categories for tags",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaTaggingTagCategory);
                    dbAreaTaggingTagCategory.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaMatters = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Matters" });
                if (dbAreaMatters == null)
                {
                    dbAreaMatters = new DBOs.Security.Area()
                    {
                        Name = "Matters",
                        Description = "Matters",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaMatters);
                    dbAreaMatters.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaMattersMatter = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Matters.Matter" });
                if (dbAreaMattersMatter == null)
                {
                    dbAreaMattersMatter = new DBOs.Security.Area()
                    {
                        ParentId = dbAreaMatters.Id,
                        Name = "Matters.Matter",
                        Description = "System matters",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaMattersMatter);
                    dbAreaMattersMatter.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaMattersTag = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Matters.MatterTag" });
                if (dbAreaMattersTag == null)
                {
                    dbAreaMattersTag = new DBOs.Security.Area()
                    {
                        ParentId = dbAreaMatters.Id,
                        Name = "Matters.MatterTag",
                        Description = "Tags for matters",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaMattersTag);
                    dbAreaMattersTag.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaMattersResponsibleUser = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Matters.ResponsibleUser" });
                if (dbAreaMattersResponsibleUser == null)
                {
                    dbAreaMattersResponsibleUser = new DBOs.Security.Area()
                    {
                        ParentId = dbAreaMatters.Id,
                        Name = "Matters.ResponsibleUser",
                        Description = "User responsibilities for matters",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaMattersResponsibleUser);
                    dbAreaMattersResponsibleUser.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaContacts = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Contacts" });
                if (dbAreaContacts == null)
                {
                    dbAreaContacts = new DBOs.Security.Area()
                    {
                        Name = "Contacts",
                        Description = "Contacts",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaContacts);
                    dbAreaContacts.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaContactsContact = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Contacts.Contact" });
                if (dbAreaContactsContact == null)
                {
                    dbAreaContactsContact = new DBOs.Security.Area()
                    {
                        ParentId = dbAreaContacts.Id,
                        Name = "Contacts.Contact",
                        Description = "System contacts",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaContactsContact);
                    dbAreaContactsContact.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.Area dbAreaMattersMatterContact = conn.QuerySingle<DBOs.Security.Area>(new { Name = "Matters.MatterContact" });
                if (dbAreaMattersMatterContact == null)
                {
                    dbAreaMattersMatterContact = new DBOs.Security.Area()
                    {
                        ParentId = dbAreaContacts.Id,
                        Name = "Matters.MatterContact",
                        Description = "Contacts for a specific matter",
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.Area>(dbAreaMattersMatterContact);
                    dbAreaMattersMatterContact.Id = (int)conn.GetLastInsertId();
                }

                #endregion

                #region Area Acls

                DBOs.Security.AreaAcl dbAAclSecurity = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbSecurity.Id, UserId = dbUser.Id });
                if (dbAAclSecurity == null)
                {
                    dbAAclSecurity = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbSecurity.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclSecurity);
                    dbAAclSecurity.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.AreaAcl dbAAclUser = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaUser.Id, UserId = dbUser.Id });
                if (dbAAclUser == null)
                {
                    dbAAclUser = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaUser.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclUser);
                    dbAAclUser.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.AreaAcl dbAAclArea = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaArea.Id, UserId = dbUser.Id });
                if (dbAAclArea == null)
                {
                    dbAAclArea = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaArea.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclArea);
                    dbAAclArea.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.AreaAcl dbAAclAreaAcl = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaAreaAcl.Id, UserId = dbUser.Id });
                if (dbAAclAreaAcl == null)
                {
                    dbAAclAreaAcl = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaAreaAcl.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclAreaAcl);
                    dbAAclAreaAcl.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.AreaAcl dbAAclSecuredResource = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaSecuredResource.Id, UserId = dbUser.Id });
                if (dbAAclSecuredResource == null)
                {
                    dbAAclSecuredResource = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaSecuredResource.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclSecuredResource);
                    dbAAclSecuredResource.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Security.AreaAcl dbAAclSecuredResourceAcl = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaSecuredResourceAcl.Id, UserId = dbUser.Id });
                if (dbAAclSecuredResourceAcl == null)
                {
                    dbAAclSecuredResourceAcl = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaSecuredResourceAcl.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclSecuredResourceAcl);
                    dbAAclSecuredResourceAcl.Id = (int)conn.GetLastInsertId();
                }

                // Matters
                DBOs.Security.AreaAcl dbAAclMatters = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaMatters.Id, UserId = dbUser.Id });
                if (dbAAclMatters == null)
                {
                    dbAAclMatters = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaMatters.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclMatters);
                    dbAAclMatters.Id = (int)conn.GetLastInsertId();
                }

                // Tagging.TagCategory
                DBOs.Security.AreaAcl dbAAclTaggingTagCategory = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaTaggingTagCategory.Id, UserId = dbUser.Id });
                if (dbAAclTaggingTagCategory == null)
                {
                    dbAAclTaggingTagCategory = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaTaggingTagCategory.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclTaggingTagCategory);
                    dbAAclTaggingTagCategory.Id = (int)conn.GetLastInsertId();
                }

                // Matters.Matter
                DBOs.Security.AreaAcl dbAAclMattersMatter = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaMattersMatter.Id, UserId = dbUser.Id });
                if (dbAAclMattersMatter == null)
                {
                    dbAAclMattersMatter = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaMattersMatter.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclMattersMatter);
                    dbAAclMattersMatter.Id = (int)conn.GetLastInsertId();
                }

                // Matters.MatterTag
                DBOs.Security.AreaAcl dbAAclMattersMatterTag = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaMattersTag.Id, UserId = dbUser.Id });
                if (dbAAclMattersMatterTag == null)
                {
                    dbAAclMattersMatterTag = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaMattersTag.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclMattersMatterTag);
                    dbAAclMattersMatterTag.Id = (int)conn.GetLastInsertId();
                }

                // Matters.ResponsibleUser
                DBOs.Security.AreaAcl dbAAclMattersResponsibleUser = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaMattersResponsibleUser.Id, UserId = dbUser.Id });
                if (dbAAclMattersResponsibleUser == null)
                {
                    dbAAclMattersResponsibleUser = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaMattersResponsibleUser.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclMattersResponsibleUser);
                    dbAAclMattersResponsibleUser.Id = (int)conn.GetLastInsertId();
                }

                // Contacts
                DBOs.Security.AreaAcl dbAAclContacts = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaContacts.Id, UserId = dbUser.Id });
                if (dbAAclContacts == null)
                {
                    dbAAclContacts = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaContacts.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclContacts);
                    dbAAclContacts.Id = (int)conn.GetLastInsertId();
                }

                // Contacts.Contact
                DBOs.Security.AreaAcl dbAAclContactsContact = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaContactsContact.Id, UserId = dbUser.Id });
                if (dbAAclContactsContact == null)
                {
                    dbAAclContactsContact = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaContactsContact.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclContactsContact);
                    dbAAclContactsContact.Id = (int)conn.GetLastInsertId();
                }

                // Matters.MatterContact
                DBOs.Security.AreaAcl dbAAclMattersMatterContact = conn.QuerySingle<DBOs.Security.AreaAcl>(new { SecurityAreaId = dbAreaMattersMatterContact.Id, UserId = dbUser.Id });
                if (dbAAclMattersMatterContact == null)
                {
                    dbAAclMattersMatterContact = new DBOs.Security.AreaAcl()
                    {
                        SecurityAreaId = dbAreaMattersMatterContact.Id,
                        UserId = dbUser.Id,
                        AllowFlags = (int)(Common.Models.PermissionType.AllAdmin | Common.Models.PermissionType.AllWrite | Common.Models.PermissionType.AllRead),
                        DenyFlags = 0,
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now
                    };
                    conn.Insert<DBOs.Security.AreaAcl>(dbAAclMattersMatterContact);
                    dbAAclMattersMatterContact.Id = (int)conn.GetLastInsertId();
                }

                #endregion
                
                #region Contacts

                DBOs.Contacts.Contact dbContact = conn.QuerySingle<DBOs.Contacts.Contact>(new { DisplayName = "Lucas Nodine" });
                if (dbContact == null)
                {
                    dbContact = new DBOs.Contacts.Contact()
                    {
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now,
                        IsOrganization = false,
                        Nickname = "Luke",
                        DisplayNamePrefix = "Mr.",
                        Surname = "Nodine",
                        MiddleName = "James",
                        GivenName = "Lucas",
                        Initials = "LJN",
                        DisplayName = "Lucas Nodine",
                        Email1DisplayName = "Fake Prevent Spambots",
                        Email1EmailAddress = "no@one.com",
                        Fax1DisplayName = "Fake Fax",
                        Fax1FaxNumber = "1-555-555-5555",
                        Address1DisplayName = "Nodine Legal PO Box",
                        Address1AddressCity = "Parsons",
                        Address1AddressStateOrProvince = "Kansas",
                        Address1AddressPostalCode = "67357",
                        Address1AddressCountry = "USA",
                        Address1AddressCountryCode = "1",
                        Address1AddressPostOfficeBox = "1125",
                        Telephone1DisplayName = "Virtual Office",
                        Telephone1TelephoneNumber = "620-577-4271",
                        Birthday = new DateTime(1982, 3, 25),
                        Wedding = new DateTime(2012, 5, 12),
                        Title = "Managing Member",
                        CompanyName = "Nodine Legal, LLC",
                        Profession = "Attorney",
                        Language = "English (American)",
                        BusinessHomePage = "www.nodinelegal.com",
                        Gender = "Male",
                        ReferredByName = "Bob"
                    };
                    conn.Insert<DBOs.Contacts.Contact>(dbContact);
                    dbContact.Id = (int)conn.GetLastInsertId();
                }

                #endregion

                #region Matters
                
                DBOs.Tagging.TagCategory dbTagCategory1 = conn.QuerySingle<DBOs.Tagging.TagCategory>(new { Name = "Status" });
                if (dbTagCategory1 == null)
                {
                    dbTagCategory1 = new DBOs.Tagging.TagCategory()
                    {
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now,
                        Name = "Status"
                    };
                    conn.Insert<DBOs.Tagging.TagCategory>(dbTagCategory1);
                    dbTagCategory1.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Tagging.TagCategory tagCategory2 = conn.QuerySingle<DBOs.Tagging.TagCategory>(new { Name = "Jurisdiction" });
                if (tagCategory2 == null)
                {
                    tagCategory2 = new DBOs.Tagging.TagCategory()
                    {
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now, 
                        Name = "Jurisdiction"
                    };
                    conn.Insert<DBOs.Tagging.TagCategory>(tagCategory2);
                    tagCategory2.Id = (int)conn.GetLastInsertId();
                }

                DBOs.Matters.Matter matter1 = conn.QuerySingle<DBOs.Matters.Matter>(new { Title = "Test Matter 1" });
                if (matter1 == null)
                {
                    matter1 = new DBOs.Matters.Matter()
                    {
                        Id = Guid.NewGuid(),
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now,
                        Title = "Test Matter 1",
                        Synopsis = "This is the synopsis for test matter 1"
                    };
                    conn.Insert<DBOs.Matters.Matter>(matter1);
                }

                DBOs.Matters.MatterTag matterTag1 = conn.QuerySingle<DBOs.Matters.MatterTag>(new { Tag = "Active" });
                if (matterTag1 == null)
                {
                    matterTag1 = new DBOs.Matters.MatterTag()
                    {
                        Id = Guid.NewGuid(),
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now,
                        MatterId = matter1.Id,
                        TagCategoryId = 1,
                        Tag = "Active"
                    };
                    conn.Insert<DBOs.Matters.MatterTag>(matterTag1);
                }

                DBOs.Matters.MatterTag matterTag2 = conn.QuerySingle<DBOs.Matters.MatterTag>(new { Tag = "Labette County, KS" });
                if (matterTag2 == null)
                {
                    matterTag2 = new DBOs.Matters.MatterTag()
                    {
                        Id = Guid.NewGuid(),
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now,
                        MatterId = matter1.Id,
                        TagCategoryId = 2,
                        Tag = "Labette County, KS"
                    };
                    conn.Insert<DBOs.Matters.MatterTag>(matterTag2);
                }

                DBOs.Matters.ResponsibleUser respUser1 = conn.QuerySingle<DBOs.Matters.ResponsibleUser>(new { MatterId = matter1.Id, UserId = dbUser.Id });
                if (respUser1 == null)
                {
                    respUser1 = new DBOs.Matters.ResponsibleUser()
                    {
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now,
                        MatterId = matter1.Id,
                        UserId = dbUser.Id,
                        Responsibility = "Attorney"
                    };
                }

                DBOs.Matters.MatterContact matterContact1 = conn.QuerySingle<DBOs.Matters.MatterContact>(new { MatterId = matter1.Id, ContactId = dbContact.Id });
                if (matterContact1 == null)
                {
                    matterContact1 = new DBOs.Matters.MatterContact()
                    {
                        CreatedByUserId = dbUser.Id,
                        ModifiedByUserId = dbUser.Id,
                        UtcCreated = DateTime.Now,
                        UtcModified = DateTime.Now,
                        MatterId = matter1.Id,
                        ContactId = dbContact.Id
                    };
                }

                #endregion

            }
        }
    }
}
