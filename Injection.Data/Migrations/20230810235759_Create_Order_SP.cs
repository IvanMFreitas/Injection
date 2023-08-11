using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Injection.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Order_SP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    CREATE PROCEDURE CreateOrder
                                    @PersonId UNIQUEIDENTIFIER,
                                    @ProductId UNIQUEIDENTIFIER,
                                    @Qty INT
                                    AS
                                    BEGIN
                                        DECLARE @Price DECIMAL(5, 2)
                                        DECLARE @NewGuid UNIQUEIDENTIFIER
                                        DECLARE @CurrentDateTime DATETIME2(7)

                                        -- Get Price from Product table
                                        SELECT @Price = Price
                                        FROM Product
                                        WHERE Id = @ProductId

                                        -- Calculate Total
                                        DECLARE @Total DECIMAL(5, 2)
                                        SET @Total = @Price * @Qty

                                        -- Generate new Guid and current DateTime
                                        SET @NewGuid = NEWID()
                                        SET @CurrentDateTime = SYSDATETIME()

                                        -- Insert into Order table
                                        INSERT INTO [Order] (Id, PersonId, ProductId, Qty, Total, CreatedAt)
                                        VALUES (@NewGuid, @PersonId, @ProductId, @Qty, @Total, @CurrentDateTime)                                       
                                    END
                                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
