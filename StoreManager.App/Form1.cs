using Microsoft.Data.SqlClient;
using StoreManager.DTO;
using StoreManager.Repository;
using StoreManager.Service.Interfaces.Repositories;

namespace StoreManager.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server = .; Database = StoreManager.Database; Integrated Security = true; TrustServerCertificate = true";

            IUnitOfWork<SqlConnection> unitOfWork = new UnitOfWork<SqlConnection>(connectionString);
            IEnumerable<Category> categories = unitOfWork.CategoryRepository.Load(c => c.Description == "Description");


            //MessageBox.Show(categories.ToList().ForEach(c => c = c.Name));

            foreach (Category category in categories)
            {
                MessageBox.Show("Fetched rows = " + Convert.ToString(categories.Count()));
            }

            //CategoryRepository repository = new(connectionString);

            //Category category = repository.Get(1);

            //var name = category.Name;

            //Category category1 = new() { CategoryId = 2, Name = "MyCategory", Description = "Description" };

            //repository.Update(category1);

            Console.ReadLine();
        }
    }
}
