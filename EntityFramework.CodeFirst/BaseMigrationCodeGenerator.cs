﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Model;

namespace HireRight.EntityFramework.CodeFirst
{
    public class BaseMigrationCodeGenerator : MigrationCodeGenerator
    {
        public override ScaffoldedMigration Generate(string migrationId, IEnumerable<MigrationOperation> operations, string sourceModel, string targetModel, string @namespace, string className)
        {
            List<string> columnNamesToSetDefaultSqlValue = new List<string> { "CreatedUtc", "ModifiedUtc" };
            const string guidColumnNameToSetDefaultSqlValue = "Id";

            foreach (MigrationOperation operation in operations)
                if (operation is CreateTableOperation)
                {
                    foreach (ColumnModel column in ((CreateTableOperation)operation).Columns)
                        if ((column.ClrType == typeof(DateTime)) && columnNamesToSetDefaultSqlValue.Contains(column.Name))
                            column.DefaultValueSql = "GETUTCDATE()";
                        else if ((column.ClrType == typeof(Guid)) && (column.Name == guidColumnNameToSetDefaultSqlValue))
                            column.DefaultValueSql = "NEWSEQUENTIALID()";
                }
                else if (operation is AddColumnOperation)
                {
                    ColumnModel column = ((AddColumnOperation)operation).Column;

                    if ((column.ClrType == typeof(DateTime)) && columnNamesToSetDefaultSqlValue.Contains(column.Name))
                        column.DefaultValueSql = "GETUTCDATE()";
                    else if ((column.ClrType == typeof(Guid)) && (column.Name == guidColumnNameToSetDefaultSqlValue))
                        column.DefaultValueSql = "NEWSEQUENTIALID()";
                }

            CSharpMigrationCodeGenerator generator = new CSharpMigrationCodeGenerator();

            return generator.Generate(migrationId, operations, sourceModel, targetModel, @namespace, className);
        }
    }
}