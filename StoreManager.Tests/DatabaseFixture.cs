using Microsoft.Data.SqlClient;

namespace StoreManager.Tests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly string ConnectionString = "Server = .; Database = StoreManagerTest; Integrated Security = true; TrustServerCertificate = true;";

        public DatabaseFixture()
        {
            Db = new SqlConnection(ConnectionString);
            Db.Open();
            var command = Db.CreateCommand();
            command.CommandText = "Insert into Categories(name) values('Category1')" +
                                  "Insert into Categories(name) values('Category2')" +
                                  "Insert into Categories(name) values('Category3')" +
                                  "Insert into Categories(name) values('Category4')" +
                                  "insert into countries(name) values('Country1')" +
                                  "insert into countries(name) values('Country2')" +
                                  "insert into countries(name) values('Country3')" +
                                  "insert into countries(name) values('Country4')" +
                                  "Insert Into Cities(CountryID, Name) values(1,'City1') " +
                                  "Insert Into Cities(CountryID, Name) values(2,'City2') " +
                                  "Insert Into Cities(CountryID, Name) values(4,'City3') " +
                                  "Insert Into Cities(CountryID, Name) values(3,'City4') " +
                                  "Insert Into EmployeeTypes(Name) values('Type1')" +
                                  "Insert Into EmployeeTypes(Name) values('Type2')" +
                                  "Insert Into EmployeeTypes(Name) values('Type3')" +
                                  "Insert Into EmployeeTypes(Name) values('Type4')" +
                                  "Insert Into Customers(FirstName, LastName, Email, Phone, CityId) values('Name1', 'LastName1','11@Email','--', 1)" +
                                  "Insert Into Customers(FirstName, LastName, Email, Phone, CityId) values('Name2', 'LastName2','22@Email','--', 2)" +
                                  "Insert Into Customers(FirstName, LastName, Email, Phone, CityId) values('Name3', 'LastName3','33@Email','--', 3)" +
                                  "Insert Into Customers(FirstName, LastName, Email, Phone, CityId) values('Name4', 'LastName4','44@Email','--', 4)" +
                                  "Insert Into Employees(EmployeeTypeId, FirstName, LastName, IdentityNumber, Email, Phone, Cityid) values(1,'Name1','LastName1', '123', '11@Email', '-', 1)" +
                                  "Insert Into Employees(EmployeeTypeId, FirstName, LastName, IdentityNumber, Email, Phone, Cityid) values(2,'Name2','LastName2', '134', '22@Email', '-', 2)" +
                                  "Insert Into Employees(EmployeeTypeId, FirstName, LastName, IdentityNumber, Email, Phone, Cityid) values(3,'Name3','LastName3', '131', '33@Email', '-', 3)" +
                                  "Insert Into Employees(EmployeeTypeId, FirstName, LastName, IdentityNumber, Email, Phone, Cityid) values(4,'Name4','LastName4', '223', '44@Email', '-', 4)" +
                                  "Insert Into Suppliers(Name, TaxCode, Email, Phone, CityId) values('Supplier1', 'Code1', '11@Email', 'Phone1', 1)" +
                                  "Insert Into Suppliers(Name, TaxCode, Email, Phone, CityId) values('Supplier2', 'Code2', '22@Email', 'Phone2', 2)" +
                                  "Insert Into Suppliers(Name, TaxCode, Email, Phone, CityId) values('Supplier3', 'Code3', '33@Email', 'Phone3', 3)" +
                                  "Insert Into Suppliers(Name, TaxCode, Email, Phone, CityId) values('Supplier4', 'Code4', '44@Email', 'Phone4', 4)" +
                                  "Insert into Purchases(EmployeeID, SupplierID, Status) values(1, 1, 0)" +
                                  "Insert into Purchases(EmployeeID, SupplierID, Status) values(2, 2, 0)" +
                                  "Insert into Purchases(EmployeeID, SupplierID, Status) values(2, 3, 0)" +
                                  "Insert into Purchases(EmployeeID, SupplierID, Status) values(2, 4, 0)" +
                                  "Insert into Sales(EmployeeID, CustomerID, Status) values(2, 1, 0)" +
                                  "Insert into Sales(EmployeeID, CustomerID, Status) values(2, 3, 0)" +
                                  "Insert into Sales(EmployeeID, CustomerID, Status) values(2, 2, 0)" +
                                  "Insert into Sales(EmployeeID, CustomerID, Status) values(2, 4, 0)" +
                                  "Insert Into Roles(RoleName, RoleCode) values('Moderator', 'ADM')" +
                                  "Insert Into Roles(RoleName, RoleCode) values('Console', 'CON')" +
                                  "Insert Into Roles(RoleName, RoleCode) values('Administrator', 'ADM')" +
                                  "Insert into Users(EmployeeId, UserName, Password) Values(1, 'UserName1', Convert(varbinary, 'Password1'))" +
                                  "Insert into Users(EmployeeId, UserName, Password) Values(2, 'UserName2', Convert(varbinary, 'Password2'))" +
                                  "Insert into Users(EmployeeId, UserName, Password) Values(3, 'UserName3', Convert(varbinary, 'Password3'))" +
                                  "insert into UserRoles(RoleID, EmployeeID) values(1,1);" +
                                  "insert into UserRoles(RoleID, EmployeeID) values(1,2);" +
                                  "insert into UserRoles(RoleID, EmployeeID) values(1,3);";
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            var command = Db.CreateCommand();

            command.CommandText = "Delete UserRoles;" +
                        "Delete Users;" +
                        "Delete Roles; DBCC CHECKIDENT('Roles', RESEED, 0)" +
                        "Delete Sales;  DBCC CHECKIDENT('Sales', RESEED, 0)" +
                        "Delete Purchases;  DBCC CHECKIDENT('Purchases', RESEED, 0)" +
                        "Delete Suppliers;  DBCC CHECKIDENT('Suppliers', RESEED, 0)" +
                        "Delete Employees;  DBCC CHECKIDENT('Employees', RESEED, 0)" +
                        "Delete Customers;  DBCC CHECKIDENT('Customers', RESEED, 0)" +
                        "Delete EmployeeTypes;  DBCC CHECKIDENT('EmployeeTypes', RESEED, 0)" +
                        "Delete Cities; DBCC CHECKIDENT ('Cities', RESEED, 0)" +
                        "Delete Countries; DBCC CHECKIDENT ('Countries', RESEED, 0)" +
                        "Delete categories; DBCC CHECKIDENT ('categories', RESEED, 0)";
            command.ExecuteNonQuery();
            Db.Close();
            Db.Dispose();
        }

        public SqlConnection Db { get; private set; }
    }
}
