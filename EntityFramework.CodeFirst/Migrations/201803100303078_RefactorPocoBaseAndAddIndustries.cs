using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;

namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorPocoBaseAndAddIndustries : DbMigration
    {
        public override void Up()
        {
            //source (made into func instead of extension): https://stackoverflow.com/a/33942989
            Action<IDbMigration, string, string> dropDefault =
                (migration, table, col) =>
                {
                    var sql =
                        new SqlOperation(String.Format("DECLARE @SQL varchar(1000) "
                                                       + "SET @SQL='ALTER TABLE {0} DROP CONSTRAINT "
                                                       + "['+(SELECT name FROM sys.default_constraints "
                                                       + "WHERE parent_object_id = object_id('{0}') "
                                                       + "AND col_name(parent_object_id, parent_column_id) = '{1}')+']'; "
                                                       + "PRINT @SQL; EXEC(@SQL);",
                                                       table, col));
                    migration.AddOperation(sql);
                };
            Sql(@"DELETE FROM dbo.ScaleCategory
                  DELETE FROM dbo.Company
                  DELETE FROM dbo.Contact
                  DELETE FROM dbo.CompanyLocation
                  DELETE FROM dbo.[Order]
                  DELETE FROM dbo.Product
                  DELETE FROM dbo.Discount");
            dropDefault(this, "dbo.ScaleCategory", "Id");
            dropDefault(this, "dbo.Company", "Id");
            dropDefault(this, "dbo.Contact", "Id");
            dropDefault(this, "dbo.CompanyLocation", "Id");
            dropDefault(this, "dbo.[Order]", "Id");
            dropDefault(this, "dbo.Product", "Id");
            dropDefault(this, "dbo.Discount", "Id");

            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Contact", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Order", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropIndex("dbo.Contact", new[] { "CompanyId" });
            DropIndex("dbo.CompanyLocation", new[] { "CompanyId" });
            DropIndex("dbo.Order", new[] { "CompanyId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropIndex("dbo.Discount", new[] { "ProductId" });
            
            AddColumn("dbo.ScaleCategory", "RowGuid", c => c.Guid(nullable: false, defaultValueSql: "NEWID()"));
            AddColumn("dbo.Company", "RowGuid", c => c.Guid(nullable: false, defaultValueSql: "NEWID()"));
            AddColumn("dbo.Contact", "RowGuid", c => c.Guid(nullable: false, defaultValueSql: "NEWID()"));
            AddColumn("dbo.CompanyLocation", "RowGuid", c => c.Guid(nullable: false, defaultValueSql: "NEWID()"));
            AddColumn("dbo.Order", "RowGuid", c => c.Guid(nullable: false, defaultValueSql: "NEWID()"));
            AddColumn("dbo.Product", "RowGuid", c => c.Guid(nullable: false, defaultValueSql: "NEWID()"));
            AddColumn("dbo.Discount", "RowGuid", c => c.Guid(nullable: false, defaultValueSql: "NEWID()"));
            AddColumn("dbo.Discount", "Product_Id", c => c.Long());

            DropPrimaryKey("dbo.ScaleCategory");
            DropPrimaryKey("dbo.Company");
            DropPrimaryKey("dbo.Contact");
            DropPrimaryKey("dbo.CompanyLocation");
            DropPrimaryKey("dbo.Order");
            DropPrimaryKey("dbo.Product");
            DropPrimaryKey("dbo.Discount");

            DropColumn("dbo.Company", "Id");
            AddColumn("dbo.Company", "Id", c => c.Long(nullable: false, identity: true));

            DropColumn("dbo.Contact", "Id");
            AddColumn("dbo.Contact", "Id", c => c.Long(nullable: false, identity: true));

            DropColumn("dbo.Contact", "CompanyId");
            AddColumn("dbo.Contact", "CompanyId", c => c.Long(nullable: false));

            DropColumn("dbo.CompanyLocation", "Id");
            AddColumn("dbo.CompanyLocation", "Id", c => c.Long(nullable: false, identity: true));

            DropColumn("dbo.CompanyLocation", "CompanyId");
            AddColumn("dbo.CompanyLocation", "CompanyId", c => c.Long(nullable: false));

            DropColumn("dbo.Order", "Id");
            AddColumn("dbo.Order", "Id", c => c.Long(nullable: false, identity: true));

            DropColumn("dbo.Order", "CompanyId");
            AddColumn("dbo.Order", "CompanyId", c => c.Long(nullable: false));

            DropColumn("dbo.Order", "ProductId");
            AddColumn("dbo.Order", "ProductId", c => c.Long(nullable: false));

            DropColumn("dbo.Product", "Id");
            AddColumn("dbo.Product", "Id", c => c.Long(nullable: false, identity: true));

            DropColumn("dbo.Discount", "Id");
            AddColumn("dbo.Discount", "Id", c => c.Long(nullable: false, identity: true));

            DropColumn("dbo.ScaleCategory", "Id");
            AddColumn("dbo.ScaleCategory", "Id", c => c.Long(nullable: false, identity: true));

            AddPrimaryKey("dbo.ScaleCategory", "Id");
            AddPrimaryKey("dbo.Company", "Id");
            AddPrimaryKey("dbo.Contact", "Id");
            AddPrimaryKey("dbo.CompanyLocation", "Id");
            AddPrimaryKey("dbo.Order", "Id");
            AddPrimaryKey("dbo.Product", "Id");
            AddPrimaryKey("dbo.Discount", "Id");

            CreateIndex("dbo.Contact", "CompanyId");
            CreateIndex("dbo.CompanyLocation", "CompanyId");
            CreateIndex("dbo.Order", "CompanyId");
            CreateIndex("dbo.Order", "ProductId");
            CreateIndex("dbo.Discount", "Product_Id");

            AddForeignKey("dbo.Discount", "Product_Id", "dbo.Product", "Id");
            AddForeignKey("dbo.Contact", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.Order", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "Id");

            CreateTable("dbo.Industry",
                        c => new
                             {
                                 Id = c.Long(nullable: false),
                                 Name = c.String(nullable: false, maxLength: 100),
                                 CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                                 TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                                 RowGuid = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                             })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable("dbo.IndustryScaleCategory",
                        c => new
                             {
                                 Id = c.Long(nullable: false, identity: true),
                                 IndustryId = c.Long(nullable: false),
                                 CategoryId = c.Long(nullable: false),
                                 CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                                 TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                                 RowGuid = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                             })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScaleCategory", t => t.CategoryId)
                .ForeignKey("dbo.Industry", t => t.IndustryId)
                .Index(t => t.IndustryId)
                .Index(t => t.CategoryId);
        }
        
        public override void Down()
        {
            //DropIndex("dbo.IndustryScaleCategory", new[] { "CategoryId" });
            //DropIndex("dbo.IndustryScaleCategory", new[] { "IndustryId" });
            //DropIndex("dbo.Industry", new[] { "Name" });
            //DropForeignKey("dbo.IndustryScaleCategory", "IndustryId", "dbo.Industry");
            //DropForeignKey("dbo.IndustryScaleCategory", "CategoryId", "dbo.ScaleCategory");
            //DropTable("dbo.IndustryScaleCategory");
            //DropTable("dbo.Industry");

            //DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            //DropForeignKey("dbo.Order", "CompanyId", "dbo.Company");
            //DropForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company");
            //DropForeignKey("dbo.Contact", "CompanyId", "dbo.Company");
            //DropForeignKey("dbo.Discount", "Product_Id", "dbo.Product");

            //DropIndex("dbo.Discount", new[] { "Product_Id" });
            //DropIndex("dbo.Order", new[] { "ProductId" });
            //DropIndex("dbo.Order", new[] { "CompanyId" });
            //DropIndex("dbo.CompanyLocation", new[] { "CompanyId" });
            //DropIndex("dbo.Contact", new[] { "CompanyId" });

            //DropPrimaryKey("dbo.Discount");
            //DropPrimaryKey("dbo.Product");
            //DropPrimaryKey("dbo.Order");
            //DropPrimaryKey("dbo.CompanyLocation");
            //DropPrimaryKey("dbo.Contact");
            //DropPrimaryKey("dbo.Company");
            //DropPrimaryKey("dbo.ScaleCategory");

            //DropColumn("dbo.Company", "Id");
            //AddColumn("dbo.Company", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));

            //DropColumn("dbo.Contact", "Id");
            //AddColumn("dbo.Contact", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
            
            //AlterColumn("dbo.Contact", "CompanyId", c => c.Guid(nullable: false));

            //DropColumn("dbo.CompanyLocation", "Id");
            //AddColumn("dbo.CompanyLocation", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
            
            //AlterColumn("dbo.CompanyLocation", "CompanyId", c => c.Guid(nullable: false));

            //DropColumn("dbo.Order", "Id");
            //AddColumn("dbo.Order", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
            
            //AlterColumn("dbo.Order", "CompanyId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Order", "ProductId", c => c.Guid(nullable: false));

            //DropColumn("dbo.Product", "Id");
            //AddColumn("dbo.Product", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));

            //DropColumn("dbo.Discount", "Id");
            //AddColumn("dbo.Discount", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));

            //DropColumn("dbo.ScaleCategory", "Id");
            //AddColumn("dbo.ScaleCategory", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));

            //AddPrimaryKey("dbo.Discount", "Id");
            //AddPrimaryKey("dbo.Product", "Id");
            //AddPrimaryKey("dbo.Order", "Id");
            //AddPrimaryKey("dbo.CompanyLocation", "Id");
            //AddPrimaryKey("dbo.Contact", "Id");
            //AddPrimaryKey("dbo.Company", "Id");
            //AddPrimaryKey("dbo.ScaleCategory", "Id");

            //DropColumn("dbo.Discount", "Product_Id");
            //DropColumn("dbo.Discount", "RowGuid");
            //DropColumn("dbo.Product", "RowGuid");
            //DropColumn("dbo.Order", "RowGuid");
            //DropColumn("dbo.CompanyLocation", "RowGuid");
            //DropColumn("dbo.Contact", "RowGuid");
            //DropColumn("dbo.Company", "RowGuid");
            //DropColumn("dbo.ScaleCategory", "RowGuid");

            //CreateIndex("dbo.Discount", "ProductId");
            //CreateIndex("dbo.Order", "ProductId");
            //CreateIndex("dbo.Order", "CompanyId");
            //CreateIndex("dbo.CompanyLocation", "CompanyId");
            //CreateIndex("dbo.Contact", "CompanyId");

            //AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "Id");
            //AddForeignKey("dbo.Order", "CompanyId", "dbo.Company", "Id");
            //AddForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company", "Id");
            //AddForeignKey("dbo.Contact", "CompanyId", "dbo.Company", "Id");
            ////remove any orphans from the id type change
            //Sql(@"DELETE FROM dbo.Discount");
            
            //AddForeignKey("dbo.Discount", "ProductId", "dbo.Product", "Id");
            //TODO: at some point, it would be nice to be able to figure out what broke between the above and the working script below...
            Sql(@"IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_CategoryId' AND object_id = object_id(N'[dbo].[IndustryScaleCategory]', N'U'))
    DROP INDEX [IX_CategoryId] ON [dbo].[IndustryScaleCategory]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_IndustryId' AND object_id = object_id(N'[dbo].[IndustryScaleCategory]', N'U'))
    DROP INDEX [IX_IndustryId] ON [dbo].[IndustryScaleCategory]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_Name' AND object_id = object_id(N'[dbo].[Industry]', N'U'))
    DROP INDEX [IX_Name] ON [dbo].[Industry]
IF object_id(N'[dbo].[FK_dbo.IndustryScaleCategory_dbo.Industry_IndustryId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[IndustryScaleCategory] DROP CONSTRAINT [FK_dbo.IndustryScaleCategory_dbo.Industry_IndustryId]
IF object_id(N'[dbo].[FK_dbo.IndustryScaleCategory_dbo.ScaleCategory_CategoryId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[IndustryScaleCategory] DROP CONSTRAINT [FK_dbo.IndustryScaleCategory_dbo.ScaleCategory_CategoryId]
DROP TABLE [dbo].[IndustryScaleCategory]
DROP TABLE [dbo].[Industry]
IF object_id(N'[dbo].[FK_dbo.Order_dbo.Product_ProductId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_dbo.Order_dbo.Product_ProductId]
IF object_id(N'[dbo].[FK_dbo.Order_dbo.Company_CompanyId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_dbo.Order_dbo.Company_CompanyId]
IF object_id(N'[dbo].[FK_dbo.CompanyLocation_dbo.Company_CompanyId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyLocation] DROP CONSTRAINT [FK_dbo.CompanyLocation_dbo.Company_CompanyId]
IF object_id(N'[dbo].[FK_dbo.Contact_dbo.Company_CompanyId]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [FK_dbo.Contact_dbo.Company_CompanyId]
IF object_id(N'[dbo].[FK_dbo.Discount_dbo.Product_Product_Id]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[Discount] DROP CONSTRAINT [FK_dbo.Discount_dbo.Product_Product_Id]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_Product_Id' AND object_id = object_id(N'[dbo].[Discount]', N'U'))
    DROP INDEX [IX_Product_Id] ON [dbo].[Discount]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_ProductId' AND object_id = object_id(N'[dbo].[Order]', N'U'))
    DROP INDEX [IX_ProductId] ON [dbo].[Order]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_CompanyId' AND object_id = object_id(N'[dbo].[Order]', N'U'))
    DROP INDEX [IX_CompanyId] ON [dbo].[Order]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_CompanyId' AND object_id = object_id(N'[dbo].[CompanyLocation]', N'U'))
    DROP INDEX [IX_CompanyId] ON [dbo].[CompanyLocation]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_CompanyId' AND object_id = object_id(N'[dbo].[Contact]', N'U'))
    DROP INDEX [IX_CompanyId] ON [dbo].[Contact]
ALTER TABLE [dbo].[Discount] DROP CONSTRAINT [PK_dbo.Discount]
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [PK_dbo.Product]
ALTER TABLE [dbo].[Order] DROP CONSTRAINT [PK_dbo.Order]
ALTER TABLE [dbo].[CompanyLocation] DROP CONSTRAINT [PK_dbo.CompanyLocation]
ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [PK_dbo.Contact]
ALTER TABLE [dbo].[Company] DROP CONSTRAINT [PK_dbo.Company]
ALTER TABLE [dbo].[ScaleCategory] DROP CONSTRAINT [PK_dbo.ScaleCategory]
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Company')
AND col_name(parent_object_id, parent_column_id) = 'Id';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Company] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Company] DROP COLUMN [Id]
ALTER TABLE [dbo].[Company] ADD [Id] [uniqueidentifier] NOT NULL DEFAULT NEWSEQUENTIALID()
DECLARE @var1 nvarchar(128)
SELECT @var1 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Contact')
AND col_name(parent_object_id, parent_column_id) = 'Id';
IF @var1 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [' + @var1 + ']')
ALTER TABLE [dbo].[Contact] DROP COLUMN [Id]
ALTER TABLE [dbo].[Contact] ADD [Id] [uniqueidentifier] NOT NULL DEFAULT NEWSEQUENTIALID()
DECLARE @var2 nvarchar(128)
SELECT @var2 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Contact')
AND col_name(parent_object_id, parent_column_id) = 'CompanyId';
IF @var2 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [' + @var2 + ']')
ALTER TABLE [dbo].[Contact] DROP COLUMN [CompanyId]
ALTER TABLE [dbo].[Contact] ADD [CompanyId] [uniqueidentifier] NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
DECLARE @var3 nvarchar(128)
SELECT @var3 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CompanyLocation')
AND col_name(parent_object_id, parent_column_id) = 'Id';
IF @var3 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CompanyLocation] DROP CONSTRAINT [' + @var3 + ']')
ALTER TABLE [dbo].[CompanyLocation] DROP COLUMN [Id]
ALTER TABLE [dbo].[CompanyLocation] ADD [Id] [uniqueidentifier] NOT NULL DEFAULT NEWSEQUENTIALID()
DECLARE @var4 nvarchar(128)
SELECT @var4 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CompanyLocation')
AND col_name(parent_object_id, parent_column_id) = 'CompanyId';
IF @var4 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CompanyLocation] DROP CONSTRAINT [' + @var4 + ']')
ALTER TABLE [dbo].[CompanyLocation] DROP COLUMN [CompanyId]
ALTER TABLE [dbo].[CompanyLocation] ADD [CompanyId] [uniqueidentifier] NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
DECLARE @var5 nvarchar(128)
SELECT @var5 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Order')
AND col_name(parent_object_id, parent_column_id) = 'Id';
IF @var5 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Order] DROP CONSTRAINT [' + @var5 + ']')
ALTER TABLE [dbo].[Order] DROP COLUMN [Id]
ALTER TABLE [dbo].[Order] ADD [Id] [uniqueidentifier] NOT NULL DEFAULT NEWSEQUENTIALID()
DECLARE @var6 nvarchar(128)
SELECT @var6 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Order')
AND col_name(parent_object_id, parent_column_id) = 'CompanyId';
IF @var6 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Order] DROP CONSTRAINT [' + @var6 + ']')
ALTER TABLE [dbo].[Order] DROP COLUMN [CompanyId]
ALTER TABLE [dbo].[Order] ADD [CompanyId] [uniqueidentifier] NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
DECLARE @var7 nvarchar(128)
SELECT @var7 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Order')
AND col_name(parent_object_id, parent_column_id) = 'ProductId';
IF @var7 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Order] DROP CONSTRAINT [' + @var7 + ']')
ALTER TABLE [dbo].[Order] DROP COLUMN [ProductId]
ALTER TABLE [dbo].[Order] ADD [ProductId] [uniqueidentifier] NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'
DECLARE @var8 nvarchar(128)
SELECT @var8 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Product')
AND col_name(parent_object_id, parent_column_id) = 'Id';
IF @var8 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Product] DROP CONSTRAINT [' + @var8 + ']')
ALTER TABLE [dbo].[Product] DROP COLUMN [Id]
ALTER TABLE [dbo].[Product] ADD [Id] [uniqueidentifier] NOT NULL DEFAULT NEWSEQUENTIALID()
DECLARE @var9 nvarchar(128)
SELECT @var9 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Discount')
AND col_name(parent_object_id, parent_column_id) = 'Id';
IF @var9 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Discount] DROP CONSTRAINT [' + @var9 + ']')
ALTER TABLE [dbo].[Discount] DROP COLUMN [Id]
ALTER TABLE [dbo].[Discount] ADD [Id] [uniqueidentifier] NOT NULL DEFAULT NEWSEQUENTIALID()
DECLARE @var10 nvarchar(128)
SELECT @var10 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.ScaleCategory')
AND col_name(parent_object_id, parent_column_id) = 'Id';
IF @var10 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[ScaleCategory] DROP CONSTRAINT [' + @var10 + ']')
ALTER TABLE [dbo].[ScaleCategory] DROP COLUMN [Id]
ALTER TABLE [dbo].[ScaleCategory] ADD [Id] [uniqueidentifier] NOT NULL DEFAULT NEWSEQUENTIALID()
ALTER TABLE [dbo].[Discount] ADD CONSTRAINT [PK_dbo.Discount] PRIMARY KEY ([Id])
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [PK_dbo.Product] PRIMARY KEY ([Id])
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [PK_dbo.Order] PRIMARY KEY ([Id])
ALTER TABLE [dbo].[CompanyLocation] ADD CONSTRAINT [PK_dbo.CompanyLocation] PRIMARY KEY ([Id])
ALTER TABLE [dbo].[Contact] ADD CONSTRAINT [PK_dbo.Contact] PRIMARY KEY ([Id])
ALTER TABLE [dbo].[Company] ADD CONSTRAINT [PK_dbo.Company] PRIMARY KEY ([Id])
ALTER TABLE [dbo].[ScaleCategory] ADD CONSTRAINT [PK_dbo.ScaleCategory] PRIMARY KEY ([Id])
DECLARE @var11 nvarchar(128)
SELECT @var11 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Discount')
AND col_name(parent_object_id, parent_column_id) = 'Product_Id';
IF @var11 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Discount] DROP CONSTRAINT [' + @var11 + ']')
ALTER TABLE [dbo].[Discount] DROP COLUMN [Product_Id]
DECLARE @var12 nvarchar(128)
SELECT @var12 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Discount')
AND col_name(parent_object_id, parent_column_id) = 'RowGuid';
IF @var12 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Discount] DROP CONSTRAINT [' + @var12 + ']')
ALTER TABLE [dbo].[Discount] DROP COLUMN [RowGuid]
DECLARE @var13 nvarchar(128)
SELECT @var13 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Product')
AND col_name(parent_object_id, parent_column_id) = 'RowGuid';
IF @var13 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Product] DROP CONSTRAINT [' + @var13 + ']')
ALTER TABLE [dbo].[Product] DROP COLUMN [RowGuid]
DECLARE @var14 nvarchar(128)
SELECT @var14 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Order')
AND col_name(parent_object_id, parent_column_id) = 'RowGuid';
IF @var14 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Order] DROP CONSTRAINT [' + @var14 + ']')
ALTER TABLE [dbo].[Order] DROP COLUMN [RowGuid]
DECLARE @var15 nvarchar(128)
SELECT @var15 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.CompanyLocation')
AND col_name(parent_object_id, parent_column_id) = 'RowGuid';
IF @var15 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[CompanyLocation] DROP CONSTRAINT [' + @var15 + ']')
ALTER TABLE [dbo].[CompanyLocation] DROP COLUMN [RowGuid]
DECLARE @var16 nvarchar(128)
SELECT @var16 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Contact')
AND col_name(parent_object_id, parent_column_id) = 'RowGuid';
IF @var16 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [' + @var16 + ']')
ALTER TABLE [dbo].[Contact] DROP COLUMN [RowGuid]
DECLARE @var17 nvarchar(128)
SELECT @var17 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Company')
AND col_name(parent_object_id, parent_column_id) = 'RowGuid';
IF @var17 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Company] DROP CONSTRAINT [' + @var17 + ']')
ALTER TABLE [dbo].[Company] DROP COLUMN [RowGuid]
DECLARE @var18 nvarchar(128)
SELECT @var18 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.ScaleCategory')
AND col_name(parent_object_id, parent_column_id) = 'RowGuid';
IF @var18 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[ScaleCategory] DROP CONSTRAINT [' + @var18 + ']')
ALTER TABLE [dbo].[ScaleCategory] DROP COLUMN [RowGuid]
CREATE INDEX [IX_ProductId] ON [dbo].[Discount]([ProductId])
CREATE INDEX [IX_ProductId] ON [dbo].[Order]([ProductId])
CREATE INDEX [IX_CompanyId] ON [dbo].[Order]([CompanyId])
CREATE INDEX [IX_CompanyId] ON [dbo].[CompanyLocation]([CompanyId])
CREATE INDEX [IX_CompanyId] ON [dbo].[Contact]([CompanyId])
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_dbo.Order_dbo.Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_dbo.Order_dbo.Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
ALTER TABLE [dbo].[CompanyLocation] ADD CONSTRAINT [FK_dbo.CompanyLocation_dbo.Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
ALTER TABLE [dbo].[Contact] ADD CONSTRAINT [FK_dbo.Contact_dbo.Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])

DELETE FROM [dbo].[Discount]
ALTER TABLE [dbo].[Discount] ADD CONSTRAINT [FK_dbo.Discount_dbo.Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])");
        }
    }
}
