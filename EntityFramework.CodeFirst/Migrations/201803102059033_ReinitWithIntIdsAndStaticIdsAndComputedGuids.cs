using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;

namespace HireRight.Persistence.Migrations
{
    public partial class ReinitWithIntIdsAndStaticIdsAndComputedGuids : DbMigration
    {
        private void DropDefault(IDbMigration migration, string table, string col)
        {
            //source (made into func instead of extension): https://stackoverflow.com/a/33942989
            var sql = new SqlOperation($"DECLARE @SQL varchar(1000) SET @SQL=\'ALTER TABLE {table} DROP CONSTRAINT [\'+(SELECT name FROM sys.default_constraints WHERE parent_object_id = object_id(\'{table.Replace("[", "").Replace("]", "")}\') AND col_name(parent_object_id, parent_column_id) = \'{col}\')+\']\'; PRINT @SQL; EXEC(@SQL);");
            migration.AddOperation(sql);
        }

        //private void CacheIdInRowGuidColumn(string table)
        //{
        //    //For each table whose ID type is switching, grab the existing GUID Id and store that in the new RowGuid column
        //    AddColumn(table.Replace("[", "").Replace("]", ""), "RowGuid", c => c.Guid(nullable: false, defaultValueSql: "NEWID()"));
        //    Sql($"Update {table} Set RowGuid = Id");
        //}

        private void RemoveRowGuidColumnAndUseAsId(string table)
        {
            DropColumn(table.Replace("[", "").Replace("]", ""), "Id");
            RenameColumn(table.Replace("[", "").Replace("]", ""), "RowGuid", "Id");
            AlterColumn(table.Replace("[", "").Replace("]", ""), "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "NEWSEQUENTIALID()"));
        }

        private void CreateRowGuidColumnAndNewIdColumn(string table)
        {
            AlterColumn(table.Replace("[", "").Replace("]", ""), "Id", c => c.Guid(nullable: false, identity: false));
            RenameColumn(table.Replace("[", "").Replace("]", ""), "Id", "RowGuid");
            AddColumn(table.Replace("[", "").Replace("]", ""), "Id", c => c.Long(nullable: false, identity: true));
        }

        //private void ChangeIdentityColumnFromGuidToLong(string table)
        //{
        //    //remove the default constraint or dropping the column will not be allowed.
        //    DropDefault(this, table, "Id");

        //    //Then, alter the table to have a Long ID column
        //    DropColumn(table.Replace("[", "").Replace("]", ""), "Id");
        //    AddColumn(table.Replace("[", "").Replace("]", ""), "Id", c => c.Long(nullable: false, identity: true));
        //}

        private void UpdateForeignKeyColumnToLong(string table, string column, string referenceTable)
        {
            //Then, create a new column on all tables which reference the modified table of type Guid to cache the original foreign key relationship
            AddColumn(table.Replace("[", "").Replace("]", ""), $"Foreign_Key_Guid_Cache_{referenceTable}", c => c.Guid(nullable: false));

            //Then, store the existing value in the cache column for all foreign keys
            Sql($"Update {table} SET Foreign_Key_Guid_Cache_{referenceTable} = {column}");

            //Then, replace the foreign keys to that table with Long columns
            DropColumn(table.Replace("[", "").Replace("]", ""), column);
            AddColumn(table.Replace("[", "").Replace("]", ""), column, c => c.Long(nullable: false));

            //Then, update the values for the foreign keys to use the cached Guid to find the originally referenced object, and get its new Long ID
            Sql($"Update {table} SET {column} = (SELECT r.Id FROM dbo.{referenceTable} r WHERE r.RowGuid = Foreign_Key_Guid_Cache_{referenceTable})");
            DropColumn(table.Replace("[", "").Replace("]", ""), $"Foreign_Key_Guid_Cache_{referenceTable}");
        }

        private void UpdateForeignKeyColumnToGuid(string table, string column, string referenceTable)
        {
            //Then, create a new column on all tables which reference the modified table of type Guid to cache the original foreign key relationship
            AddColumn(table.Replace("[", "").Replace("]", ""), $"Foreign_Key_Guid_Cache_{referenceTable}", c => c.Guid(nullable: false));

            //Then, store the existing value in the cache column for all foreign keys
            Sql($"Update {table} SET Foreign_Key_Guid_Cache_{referenceTable} = (SELECT r.RowGuid FROM dbo.{referenceTable} r WHERE r.Id = {column})");

            //Then, replace the foreign keys to that table with Guid columns
            DropColumn(table.Replace("[", "").Replace("]", ""), column);
            AddColumn(table.Replace("[", "").Replace("]", ""), column, c => c.Guid(nullable: false));

            //Then, update the values for the foreign keys to use the cached Guid to find the originally referenced object, and get its new Long ID
            Sql($"Update {table} SET {column} = Foreign_Key_Guid_Cache_{referenceTable}");
            DropColumn(table.Replace("[", "").Replace("]", ""), $"Foreign_Key_Guid_Cache_{referenceTable}");
        }

        public override void Up()
        {
            DropForeignKey("dbo.Contact", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Order", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropIndex("dbo.Contact", new[] { "CompanyId" });
            DropIndex("dbo.CompanyLocation", new[] { "CompanyId" });
            DropIndex("dbo.Order", new[] { "CompanyId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropIndex("dbo.Discount", new[] { "ProductId" });

            DropPrimaryKey("dbo.Company");
            CreateRowGuidColumnAndNewIdColumn("dbo.Company");
            UpdateForeignKeyColumnToLong("dbo.Contact", "CompanyId", "Company");
            UpdateForeignKeyColumnToLong("dbo.CompanyLocation", "CompanyId", "Company");
            UpdateForeignKeyColumnToLong("dbo.[Order]", "CompanyId", "Company");
            AddPrimaryKey("dbo.Company", "Id");

            DropPrimaryKey("dbo.Product");
            CreateRowGuidColumnAndNewIdColumn("dbo.Product");
            UpdateForeignKeyColumnToLong("dbo.Discount", "ProductId", "Product");
            UpdateForeignKeyColumnToLong("dbo.[Order]", "ProductId", "Product");
            AddPrimaryKey("dbo.Product", "Id");

            DropPrimaryKey("dbo.Contact");
            CreateRowGuidColumnAndNewIdColumn("dbo.Contact");
            AddPrimaryKey("dbo.Contact", "Id");

            DropPrimaryKey("dbo.CompanyLocation");
            CreateRowGuidColumnAndNewIdColumn("dbo.CompanyLocation");
            AddPrimaryKey("dbo.CompanyLocation", "Id");

            DropPrimaryKey("dbo.Order");
            CreateRowGuidColumnAndNewIdColumn("dbo.[Order]");
            AddPrimaryKey("dbo.Order", "Id");

            DropPrimaryKey("dbo.Discount");
            CreateRowGuidColumnAndNewIdColumn("dbo.Discount");
            AddPrimaryKey("dbo.Discount", "Id");

            DropPrimaryKey("dbo.ScaleCategory");
            CreateRowGuidColumnAndNewIdColumn("dbo.ScaleCategory");
            AddPrimaryKey("dbo.ScaleCategory", "Id");

            CreateIndex("dbo.Contact", "CompanyId");
            CreateIndex("dbo.CompanyLocation", "CompanyId");
            CreateIndex("dbo.Order", "CompanyId");
            CreateIndex("dbo.Order", "ProductId");
            CreateIndex("dbo.Discount", "ProductId");

            AddForeignKey("dbo.Contact", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.Order", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.Discount", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "Id");

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

            CreateTable("dbo.Industry",
                        c => new
                        {
                            Id = c.Long(nullable: false, identity: true),
                            Name = c.String(nullable: false, maxLength: 100),
                            StaticId = c.Long(nullable: false),
                            CreatedUtc = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                            TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                            RowGuid = c.Guid(nullable: false, defaultValueSql: "NEWID()"),
                        })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.StaticId, unique: true);

            AddColumn("dbo.ScaleCategory", "StaticId", c => c.Long(nullable: false));
            Sql("Update dbo.ScaleCategory Set StaticId = Id");
            CreateIndex("dbo.ScaleCategory", "StaticId", unique: true);

            AddColumn("dbo.Product", "StaticId", c => c.Long(nullable: false));
            Sql("Update dbo.Product Set StaticId = Id");
            CreateIndex("dbo.Product", "StaticId", unique: true);
        }

        public override void Down()
        {
            DropIndex("dbo.Industry", new[] { "StaticId" });
            DropIndex("dbo.Industry", new[] { "Name" });
            DropForeignKey("dbo.IndustryScaleCategory", "IndustryId", "dbo.Industry");
            DropForeignKey("dbo.IndustryScaleCategory", "CategoryId", "dbo.ScaleCategory");
            DropIndex("dbo.IndustryScaleCategory", new[] { "CategoryId" });
            DropIndex("dbo.IndustryScaleCategory", new[] { "IndustryId" });
            DropTable("dbo.Industry");
            DropTable("dbo.IndustryScaleCategory");

            DropIndex("dbo.ScaleCategory", new[] { "StaticId" });
            DropIndex("dbo.Product", new[] { "StaticId" });
            DropColumn("dbo.ScaleCategory", "StaticId");
            DropColumn("dbo.Product", "StaticId");

            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Order", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Contact", "CompanyId", "dbo.Company");
            DropIndex("dbo.Discount", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "CompanyId" });
            DropIndex("dbo.CompanyLocation", new[] { "CompanyId" });
            DropIndex("dbo.Contact", new[] { "CompanyId" });

            DropPrimaryKey("dbo.Company");
            UpdateForeignKeyColumnToGuid("dbo.Contact", "CompanyId", "Company");
            UpdateForeignKeyColumnToGuid("dbo.CompanyLocation", "CompanyId", "Company");
            UpdateForeignKeyColumnToGuid("dbo.[Order]", "CompanyId", "Company");
            RemoveRowGuidColumnAndUseAsId("dbo.Company");
            AddPrimaryKey("dbo.Company", "Id");

            DropPrimaryKey("dbo.Product");
            UpdateForeignKeyColumnToGuid("dbo.Discount", "ProductId", "Product");
            UpdateForeignKeyColumnToGuid("dbo.[Order]", "ProductId", "Product");
            RemoveRowGuidColumnAndUseAsId("dbo.Product");
            AddPrimaryKey("dbo.Product", "Id");

            DropPrimaryKey("dbo.Contact");
            RemoveRowGuidColumnAndUseAsId("dbo.Contact");
            AddPrimaryKey("dbo.Contact", "Id");

            DropPrimaryKey("dbo.CompanyLocation");
            RemoveRowGuidColumnAndUseAsId("dbo.CompanyLocation");
            AddPrimaryKey("dbo.CompanyLocation", "Id");

            DropPrimaryKey("dbo.Order");
            RemoveRowGuidColumnAndUseAsId("dbo.[Order]");
            AddPrimaryKey("dbo.Order", "Id");

            DropPrimaryKey("dbo.Discount");
            RemoveRowGuidColumnAndUseAsId("dbo.Discount");
            AddPrimaryKey("dbo.Discount", "Id");

            DropPrimaryKey("dbo.ScaleCategory");
            RemoveRowGuidColumnAndUseAsId("dbo.ScaleCategory");
            AddPrimaryKey("dbo.ScaleCategory", "Id");

            CreateIndex("dbo.Discount", "ProductId");
            CreateIndex("dbo.Order", "ProductId");
            CreateIndex("dbo.Order", "CompanyId");
            CreateIndex("dbo.CompanyLocation", "CompanyId");
            CreateIndex("dbo.Contact", "CompanyId");
            AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.Discount", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.Order", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.CompanyLocation", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.Contact", "CompanyId", "dbo.Company", "Id");
        }
    }
}
